using CodeBundler.Core.Models;
using CodeBundler.Core.Options;

namespace CodeBundler.Core.Abstractions;

public interface IProjectScanner
{
    Task<IReadOnlyList<ExtensionSummary>> GetExtensionsAsync(
        string rootPath,
        CancellationToken cancellationToken = default);

    Task<BundleDocument> BuildBundleAsync(
        ProjectScanOptions options,
        string rootPath,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> GetFilesAsync(
        ProjectScanOptions options,
        string rootPath,
        CancellationToken cancellationToken = default);
}
