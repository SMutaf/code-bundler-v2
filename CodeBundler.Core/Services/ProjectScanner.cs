using CodeBundler.Core.Abstractions;
using CodeBundler.Core.Models;
using CodeBundler.Core.Options;

namespace CodeBundler.Core.Services;

public sealed class ProjectScanner(IProjectTraversalPolicy traversalPolicy) : IProjectScanner
{
    public Task<IReadOnlyList<ExtensionSummary>> GetExtensionsAsync(
        string rootPath,
        CancellationToken cancellationToken = default) =>
        Task.Run<IReadOnlyList<ExtensionSummary>>(() =>
        {
            ValidateRootPath(rootPath);

            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var filePath in EnumerateFiles(rootPath, cancellationToken))
            {
                var extension = DefaultProjectTraversalPolicy.NormalizeExtension(Path.GetExtension(filePath));
                if (string.IsNullOrEmpty(extension))
                {
                    continue;
                }

                counts[extension] = counts.TryGetValue(extension, out var count) ? count + 1 : 1;
            }

            return counts
                .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
                .Select(pair => new ExtensionSummary(pair.Key, pair.Value))
                .ToList();
        }, cancellationToken);

    public async Task<BundleDocument> BuildBundleAsync(
        ProjectScanOptions options,
        string rootPath,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(options);
        ValidateRootPath(rootPath);

        var files = new List<BundleFile>();
        foreach (var filePath in await GetFilesAsync(options, rootPath, cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var content = await File.ReadAllTextAsync(filePath, cancellationToken);
            var relativePath = Path.GetRelativePath(rootPath, filePath);
            var extension = DefaultProjectTraversalPolicy.NormalizeExtension(Path.GetExtension(filePath));
            files.Add(new BundleFile(relativePath, extension, content));
        }

        files.Sort(static (left, right) => StringComparer.OrdinalIgnoreCase.Compare(left.Path, right.Path));
        return new BundleDocument(rootPath, files);
    }

    public Task<IReadOnlyList<string>> GetFilesAsync(
        ProjectScanOptions options,
        string rootPath,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(options);

        return Task.Run<IReadOnlyList<string>>(() =>
        {
            ValidateRootPath(rootPath);

            var normalizedExtensions = options.IncludedExtensions?
                .Select(DefaultProjectTraversalPolicy.NormalizeExtension)
                .Where(static extension => !string.IsNullOrWhiteSpace(extension))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return EnumerateFiles(rootPath, cancellationToken, normalizedExtensions)
                .OrderBy(static path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }, cancellationToken);
    }

    private IEnumerable<string> EnumerateFiles(
        string rootPath,
        CancellationToken cancellationToken,
        IReadOnlySet<string>? includedExtensions = null)
    {
        var pending = new Stack<string>();
        pending.Push(rootPath);

        while (pending.Count > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var currentDirectory = pending.Pop();

            foreach (var directory in Directory.EnumerateDirectories(currentDirectory))
            {
                var directoryName = Path.GetFileName(directory);
                if (traversalPolicy.ShouldSkipDirectory(directoryName))
                {
                    continue;
                }

                pending.Push(directory);
            }

            foreach (var file in Directory.EnumerateFiles(currentDirectory))
            {
                if (traversalPolicy.ShouldIncludeFile(file, includedExtensions))
                {
                    yield return file;
                }
            }
        }
    }

    private static void ValidateRootPath(string rootPath)
    {
        if (string.IsNullOrWhiteSpace(rootPath))
        {
            throw new ArgumentException("Root path is required.", nameof(rootPath));
        }

        if (!Directory.Exists(rootPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {rootPath}");
        }
    }
}
