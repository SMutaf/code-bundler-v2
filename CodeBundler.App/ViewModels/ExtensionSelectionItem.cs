namespace CodeBundler.App.ViewModels;

public sealed class ExtensionSelectionItem
{
    public string Extension { get; init; } = string.Empty;
    public int Count { get; init; }

    public override string ToString() =>
        Count > 0 ? $"{Extension} ({Count})" : $"{Extension} (manual)";
}
