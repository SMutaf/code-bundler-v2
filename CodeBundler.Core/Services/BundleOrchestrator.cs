using CodeBundler.Core.Abstractions;
using CodeBundler.Core.Models;
using CodeBundler.Core.Options;

namespace CodeBundler.Core.Services;

public sealed class BundleOrchestrator(
    IProjectScanner projectScanner,
    IBundleExportService bundleExportService) : IBundleOrchestrator
{
    public async Task CreateBundleAsync(
        BundleRequest request,
        IProgress<BundleProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.OutputPath))
        {
            throw new ArgumentException("Output path is required.", nameof(request));
        }

        var filePaths = await projectScanner.GetFilesAsync(
            new ProjectScanOptions
            {
                IncludedExtensions = request.IncludedExtensions
            },
            request.RootPath,
            cancellationToken);

        await bundleExportService.WriteAsync(
            request.OutputPath,
            request.RootPath,
            filePaths,
            request.MinifyJson,
            progress,
            cancellationToken);
    }
}
