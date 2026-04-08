using CodeBundler.Core.Models;
using CodeBundler.Core.Options;

namespace CodeBundler.Core.Abstractions;

public interface IBundleOrchestrator
{
    Task CreateBundleAsync(
        BundleRequest request,
        IProgress<BundleProgress>? progress = null,
        CancellationToken cancellationToken = default);
}
