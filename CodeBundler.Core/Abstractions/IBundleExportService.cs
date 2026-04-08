using CodeBundler.Core.Models;

namespace CodeBundler.Core.Abstractions;

public interface IBundleExportService
{
    Task WriteAsync(
        string outputPath,
        string rootPath,
        IReadOnlyList<string> filePaths,
        bool minify,
        IProgress<BundleProgress>? progress = null,
        CancellationToken cancellationToken = default);
}
