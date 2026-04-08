using CodeBundler.Core.Abstractions;

namespace CodeBundler.Core.Services;

public sealed class DefaultProjectTraversalPolicy : IProjectTraversalPolicy
{
    private static readonly HashSet<string> IgnoredDirectories = new(StringComparer.OrdinalIgnoreCase)
    {
        ".git",
        ".vs",
        ".idea",
        ".vscode",
        ".venv",
        "venv",
        "env",
        ".env",
        "__pypackages__",
        "site-packages",
        "Scripts",
        "bin",
        "obj",
        "node_modules",
        "packages",
        "vendor",
        "dist",
        "build",
        "out",
        "coverage",
        "TestResults",
        ".next",
        ".nuxt",
        "__pycache__",
        ".pytest_cache",
        ".mypy_cache",
        ".ruff_cache",
        ".tox",
        ".nox",
        "target"
    };

    private static readonly HashSet<string> IgnoredFileNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "package-lock.json",
        "yarn.lock",
        "pnpm-lock.yaml",
        "bun.lockb",
        "poetry.lock",
        "Pipfile.lock",
        "Cargo.lock",
        "composer.lock",
        "packages.lock.json",
        ".python-version"
    };

    public bool ShouldSkipDirectory(string directoryName) =>
        IgnoredDirectories.Contains(directoryName);

    public bool ShouldIncludeFile(string filePath, IReadOnlySet<string>? includedExtensions)
    {
        var fileName = Path.GetFileName(filePath);
        if (IgnoredFileNames.Contains(fileName))
        {
            return false;
        }

        var extension = NormalizeExtension(Path.GetExtension(filePath));
        if (string.IsNullOrWhiteSpace(extension))
        {
            return false;
        }

        return includedExtensions is null || includedExtensions.Count == 0 || includedExtensions.Contains(extension);
    }

    public static string NormalizeExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            return string.Empty;
        }

        return extension.StartsWith('.') ? extension.ToLowerInvariant() : $".{extension.ToLowerInvariant()}";
    }
}
