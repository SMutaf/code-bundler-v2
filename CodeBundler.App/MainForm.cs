using CodeBundler.App.ViewModels;
using CodeBundler.Core.Abstractions;
using CodeBundler.Core.Models;
using CodeBundler.Core.Options;

namespace CodeBundler.App;

public partial class MainForm : Form
{
    private readonly IProjectScanner _projectScanner;
    private readonly IBundleOrchestrator _bundleOrchestrator;
    private CancellationTokenSource? _bundleCancellationTokenSource;

    public MainForm(IProjectScanner projectScanner, IBundleOrchestrator bundleOrchestrator)
    {
        _projectScanner = projectScanner;
        _bundleOrchestrator = bundleOrchestrator;

        InitializeComponent();
    }

    private async void browseProjectButton_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select the project folder to scan."
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        projectPathTextBox.Text = dialog.SelectedPath;
        await LoadExtensionsAsync();
    }

    private void browseOutputButton_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select the output folder."
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            outputPathTextBox.Text = dialog.SelectedPath;
        }
    }

    private async void refreshExtensionsButton_Click(object sender, EventArgs e)
    {
        await LoadExtensionsAsync();
    }

    private void addExtensionButton_Click(object sender, EventArgs e)
    {
        var normalizedExtension = NormalizeUserExtension(manualExtensionTextBox.Text);
        if (string.IsNullOrWhiteSpace(normalizedExtension))
        {
            statusLabel.Text = "Enter a valid extension such as .cs or py.";
            return;
        }

        var existingItem = extensionsCheckedListBox.Items
            .OfType<ExtensionSelectionItem>()
            .FirstOrDefault(item => string.Equals(item.Extension, normalizedExtension, StringComparison.OrdinalIgnoreCase));

        if (existingItem is not null)
        {
            var index = extensionsCheckedListBox.Items.IndexOf(existingItem);
            extensionsCheckedListBox.SetItemChecked(index, true);
            statusLabel.Text = $"{normalizedExtension} is already in the list and has been selected.";
        }
        else
        {
            var newItem = new ExtensionSelectionItem
            {
                Extension = normalizedExtension,
                Count = 0
            };

            var insertIndex = GetInsertIndex(normalizedExtension);
            extensionsCheckedListBox.Items.Insert(insertIndex, newItem);
            extensionsCheckedListBox.SetItemChecked(insertIndex, true);
            statusLabel.Text = $"{normalizedExtension} added.";
        }

        manualExtensionTextBox.Clear();
    }

    private void selectAllExtensionsButton_Click(object sender, EventArgs e)
    {
        SetExtensionSelection(true);
    }

    private void clearExtensionsButton_Click(object sender, EventArgs e)
    {
        SetExtensionSelection(false);
    }

    private async void bundleButton_Click(object sender, EventArgs e)
    {
        if (!ValidateInputs(out var message))
        {
            statusLabel.Text = message;
            return;
        }

        ToggleBusyState(true, "Preparing bundle...");

        try
        {
            var selectedExtensions = extensionsCheckedListBox.CheckedItems
                .OfType<ExtensionSelectionItem>()
                .Select(static item => item.Extension)
                .ToArray();

            var request = new BundleRequest
            {
                RootPath = projectPathTextBox.Text.Trim(),
                OutputPath = BuildOutputFilePath(),
                IncludedExtensions = selectedExtensions,
                MinifyJson = minifyJsonCheckBox.Checked
            };

            _bundleCancellationTokenSource = new CancellationTokenSource();
            var progress = new Progress<BundleProgress>(update =>
            {
                statusLabel.Text = $"Bundling {update.ProcessedFileCount}/{update.TotalFileCount}: {update.CurrentPath}";
            });

            await Task.Run(async () =>
            {
                await _bundleOrchestrator.CreateBundleAsync(request, progress, _bundleCancellationTokenSource.Token);
            });

            statusLabel.Text = $"Bundle created: {request.OutputPath}";
        }
        catch (Exception ex)
        {
            statusLabel.Text = ex.Message;
        }
        finally
        {
            _bundleCancellationTokenSource?.Dispose();
            _bundleCancellationTokenSource = null;
            ToggleBusyState(false, statusLabel.Text);
        }
    }

    private async Task LoadExtensionsAsync()
    {
        var rootPath = projectPathTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(rootPath))
        {
            statusLabel.Text = "Select a project folder first.";
            return;
        }

        ToggleBusyState(true, "Scanning project...");

        try
        {
            var extensions = await _projectScanner.GetExtensionsAsync(rootPath);

            extensionsCheckedListBox.Items.Clear();
            foreach (var extension in extensions)
            {
                var item = new ExtensionSelectionItem
                {
                    Extension = extension.Extension,
                    Count = extension.Count
                };

                var index = extensionsCheckedListBox.Items.Add(item);
                extensionsCheckedListBox.SetItemChecked(index, true);
            }

            statusLabel.Text = $"{extensions.Count} extensions found.";
        }
        catch (Exception ex)
        {
            extensionsCheckedListBox.Items.Clear();
            statusLabel.Text = ex.Message;
        }
        finally
        {
            ToggleBusyState(false, statusLabel.Text);
        }
    }

    private bool ValidateInputs(out string message)
    {
        if (string.IsNullOrWhiteSpace(projectPathTextBox.Text))
        {
            message = "Project folder is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(outputPathTextBox.Text))
        {
            message = "Output folder is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(outputFileNameTextBox.Text))
        {
            message = "Please enter a file name.";
            return false;
        }

        if (outputFileNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            message = "The file name contains invalid characters.";
            return false;
        }

        if (extensionsCheckedListBox.CheckedItems.Count == 0)
        {
            message = "Select at least one extension.";
            return false;
        }

        message = string.Empty;
        return true;
    }

    private void SetExtensionSelection(bool isSelected)
    {
        for (var index = 0; index < extensionsCheckedListBox.Items.Count; index++)
        {
            extensionsCheckedListBox.SetItemChecked(index, isSelected);
        }

        statusLabel.Text = isSelected
            ? "All extensions selected."
            : "All extensions cleared.";
    }

    private int GetInsertIndex(string normalizedExtension)
    {
        for (var index = 0; index < extensionsCheckedListBox.Items.Count; index++)
        {
            if (extensionsCheckedListBox.Items[index] is not ExtensionSelectionItem item)
            {
                continue;
            }

            if (string.Compare(item.Extension, normalizedExtension, StringComparison.OrdinalIgnoreCase) > 0)
            {
                return index;
            }
        }

        return extensionsCheckedListBox.Items.Count;
    }

    private static string NormalizeUserExtension(string extensionText)
    {
        var value = extensionText.Trim();
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (value.StartsWith("*.", StringComparison.Ordinal))
        {
            value = value[1..];
        }

        if (!value.StartsWith(".", StringComparison.Ordinal))
        {
            value = $".{value}";
        }

        if (value.Length < 2 || value.Skip(1).Any(char.IsWhiteSpace))
        {
            return string.Empty;
        }

        return value.ToLowerInvariant();
    }

    private void ToggleBusyState(bool isBusy, string statusText)
    {
        UseWaitCursor = isBusy;
        browseProjectButton.Enabled = !isBusy;
        browseOutputButton.Enabled = !isBusy;
        refreshExtensionsButton.Enabled = !isBusy;
        addExtensionButton.Enabled = !isBusy;
        manualExtensionTextBox.Enabled = !isBusy;
        extensionsCheckedListBox.Enabled = !isBusy;
        outputFileNameTextBox.Enabled = !isBusy;
        selectAllExtensionsButton.Enabled = !isBusy;
        clearExtensionsButton.Enabled = !isBusy;
        bundleButton.Enabled = !isBusy;
        statusLabel.Text = statusText;
    }

    private string BuildOutputFilePath()
    {
        var folderPath = outputPathTextBox.Text.Trim();
        var fileName = outputFileNameTextBox.Text.Trim();

        if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            fileName += ".json";
        }

        return Path.Combine(folderPath, fileName);
    }
}
