namespace CodeBundler.Core.Models;

public sealed record BundleProgress(int ProcessedFileCount, int TotalFileCount, string CurrentPath);
