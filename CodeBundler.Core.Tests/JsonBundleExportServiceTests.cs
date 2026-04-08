using System.Text.Json;
using CodeBundler.Core.Services;

namespace CodeBundler.Core.Tests;

public sealed class JsonBundleExportServiceTests : IDisposable
{
    private readonly string _rootPath;
    private readonly string _outputPath;

    public JsonBundleExportServiceTests()
    {
        _rootPath = Path.Combine(Path.GetTempPath(), "CodeBundlerExportTests", Guid.NewGuid().ToString("N"));
        _outputPath = Path.Combine(_rootPath, "bundle.json");
        Directory.CreateDirectory(_rootPath);
    }

    [Fact]
    public async Task WriteAsync_PrettyJson_WritesValidJsonDocument()
    {
        var service = new JsonBundleExportService();
        var filePath = Path.Combine(_rootPath, "src", "app.cs");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        await File.WriteAllTextAsync(filePath, "Console.WriteLine(\"Hello\");");

        await service.WriteAsync(_outputPath, _rootPath, [filePath], minify: false);
        var json = await File.ReadAllTextAsync(_outputPath);

        using var document = JsonDocument.Parse(json);
        Assert.Equal(_rootPath, document.RootElement.GetProperty("rootPath").GetString());
        Assert.Equal("src\\app.cs", document.RootElement.GetProperty("files")[0].GetProperty("path").GetString());
        Assert.Contains(Environment.NewLine, json);
    }

    public void Dispose()
    {
        if (Directory.Exists(_rootPath))
        {
            Directory.Delete(_rootPath, recursive: true);
        }
    }
}
