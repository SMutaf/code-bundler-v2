using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using CodeBundler.Core.Abstractions;
using CodeBundler.Core.Models;

namespace CodeBundler.Core.Services;

public sealed class JsonBundleExportService : IBundleExportService
{
    private static readonly JsonWriterOptions MinifiedOptions = new()
    {
        Indented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonWriterOptions PrettyOptions = new()
    {
        Indented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task WriteAsync(
        string outputPath,
        string rootPath,
        IReadOnlyList<string> filePaths,
        bool minify,
        IProgress<BundleProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        ArgumentNullException.ThrowIfNull(filePaths);

        var outputDirectory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrWhiteSpace(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        await using var stream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 65536,
            useAsync: true);
        await using var writer = new Utf8JsonWriter(stream, minify ? MinifiedOptions : PrettyOptions);

        writer.WriteStartObject();
        writer.WriteString("rootPath", rootPath);
        writer.WriteStartArray("files");

        for (var index = 0; index < filePaths.Count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filePath = filePaths[index];
            var content = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);
            var relativePath = Path.GetRelativePath(rootPath, filePath);
            var extension = DefaultProjectTraversalPolicy.NormalizeExtension(Path.GetExtension(filePath));

            writer.WriteStartObject();
            writer.WriteString("path", relativePath);
            writer.WriteString("extension", extension);
            writer.WriteString("content", content);
            writer.WriteEndObject();

            if ((index + 1) % 25 == 0)
            {
                await writer.FlushAsync(cancellationToken);
            }

            progress?.Report(new BundleProgress(index + 1, filePaths.Count, relativePath));
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
        await writer.FlushAsync(cancellationToken);
    }
}
