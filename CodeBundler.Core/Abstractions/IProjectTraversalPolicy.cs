namespace CodeBundler.Core.Abstractions;

public interface IProjectTraversalPolicy
{
    bool ShouldSkipDirectory(string directoryName);
    bool ShouldIncludeFile(string filePath, IReadOnlySet<string>? includedExtensions);
}
