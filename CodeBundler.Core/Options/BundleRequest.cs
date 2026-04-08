namespace CodeBundler.Core.Options;

public sealed class BundleRequest
{
    public required string RootPath { get; init; }
    public required string OutputPath { get; init; }
    public required IReadOnlyCollection<string> IncludedExtensions { get; init; }
    public bool MinifyJson { get; init; }
}
