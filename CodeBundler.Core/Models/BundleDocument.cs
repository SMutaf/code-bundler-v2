namespace CodeBundler.Core.Models;

public sealed record BundleDocument(string RootPath, IReadOnlyList<BundleFile> Files);
