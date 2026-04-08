using CodeBundler.Core.Options;
using CodeBundler.Core.Services;

namespace CodeBundler.Core.Tests;

public sealed class ProjectScannerTests : IDisposable
{
    private readonly string _rootPath;

    public ProjectScannerTests()
    {
        _rootPath = Path.Combine(Path.GetTempPath(), "CodeBundlerTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_rootPath);
    }

    [Fact]
    public async Task GetExtensionsAsync_IgnoresKnownDirectoriesAndLockFiles()
    {
        WriteFile("src/app.py", "print('hello')");
        WriteFile("src/config.json", "{ }");
        WriteFile("node_modules/pkg/index.js", "ignored");
        WriteFile("obj/generated.cs", "ignored");
        WriteFile("venv/Lib/site-packages/aiohttp/streams.py", "ignored");
        WriteFile("package-lock.json", "{}");

        var scanner = CreateScanner();

        var result = await scanner.GetExtensionsAsync(_rootPath);

        Assert.Collection(
            result,
            item => Assert.Equal((".json", 1), (item.Extension, item.Count)),
            item => Assert.Equal((".py", 1), (item.Extension, item.Count)));
    }

    [Fact]
    public async Task BuildBundleAsync_IncludesOnlySelectedExtensionsAndUsesRelativePaths()
    {
        WriteFile("src/app.py", "print('hello')");
        WriteFile("src/notes.txt", "skip");
        WriteFile("nested/api/main.py", "print('api')");

        var scanner = CreateScanner();

        var document = await scanner.BuildBundleAsync(
            new ProjectScanOptions
            {
                IncludedExtensions = [".py"]
            },
            _rootPath);

        Assert.Equal(_rootPath, document.RootPath);
        Assert.Equal(2, document.Files.Count);
        Assert.All(document.Files, file => Assert.Equal(".py", file.Extension));
        Assert.Contains(document.Files, file => file.Path == Path.Combine("nested", "api", "main.py"));
        Assert.Contains(document.Files, file => file.Path == Path.Combine("src", "app.py"));
    }

    public void Dispose()
    {
        if (Directory.Exists(_rootPath))
        {
            Directory.Delete(_rootPath, recursive: true);
        }
    }

    private ProjectScanner CreateScanner() => new(new DefaultProjectTraversalPolicy());

    private void WriteFile(string relativePath, string content)
    {
        var fullPath = Path.Combine(_rootPath, relativePath);
        var directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(fullPath, content);
    }
}
