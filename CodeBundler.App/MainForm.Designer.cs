namespace CodeBundler.App;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel rootLayout = null!;
    private Label projectPathLabel = null!;
    private TextBox projectPathTextBox = null!;
    private Button browseProjectButton = null!;
    private Button refreshExtensionsButton = null!;
    private Label outputPathLabel = null!;
    private TextBox outputPathTextBox = null!;
    private Button browseOutputButton = null!;
    private CheckBox minifyJsonCheckBox = null!;
    private TableLayoutPanel extensionLayout = null!;
    private GroupBox extensionOptionsGroup = null!;
    private GroupBox extensionsListGroup = null!;
    private Button selectAllExtensionsButton = null!;
    private Button clearExtensionsButton = null!;
    private Label manualExtensionLabel = null!;
    private TextBox manualExtensionTextBox = null!;
    private Button addExtensionButton = null!;
    private CheckedListBox extensionsCheckedListBox = null!;
    private Button bundleButton = null!;
    private Label statusLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        rootLayout = new TableLayoutPanel();
        projectPathLabel = new Label();
        projectPathTextBox = new TextBox();
        browseProjectButton = new Button();
        refreshExtensionsButton = new Button();
        outputPathLabel = new Label();
        outputPathTextBox = new TextBox();
        browseOutputButton = new Button();
        minifyJsonCheckBox = new CheckBox();
        extensionLayout = new TableLayoutPanel();
        extensionOptionsGroup = new GroupBox();
        addExtensionButton = new Button();
        manualExtensionTextBox = new TextBox();
        manualExtensionLabel = new Label();
        clearExtensionsButton = new Button();
        selectAllExtensionsButton = new Button();
        extensionsListGroup = new GroupBox();
        extensionsCheckedListBox = new CheckedListBox();
        bundleButton = new Button();
        statusLabel = new Label();
        rootLayout.SuspendLayout();
        extensionLayout.SuspendLayout();
        extensionOptionsGroup.SuspendLayout();
        extensionsListGroup.SuspendLayout();
        SuspendLayout();
        //
        // rootLayout
        //
        rootLayout.ColumnCount = 4;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        rootLayout.Controls.Add(projectPathLabel, 0, 0);
        rootLayout.Controls.Add(projectPathTextBox, 1, 0);
        rootLayout.Controls.Add(browseProjectButton, 2, 0);
        rootLayout.Controls.Add(refreshExtensionsButton, 3, 0);
        rootLayout.Controls.Add(outputPathLabel, 0, 1);
        rootLayout.Controls.Add(outputPathTextBox, 1, 1);
        rootLayout.Controls.Add(browseOutputButton, 2, 1);
        rootLayout.Controls.Add(minifyJsonCheckBox, 3, 1);
        rootLayout.Controls.Add(extensionLayout, 0, 2);
        rootLayout.Controls.Add(bundleButton, 3, 3);
        rootLayout.Controls.Add(statusLabel, 0, 4);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(12, 12);
        rootLayout.Name = "rootLayout";
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        rootLayout.Size = new Size(960, 597);
        rootLayout.TabIndex = 0;
        rootLayout.SetColumnSpan(extensionLayout, 4);
        rootLayout.SetColumnSpan(statusLabel, 4);
        //
        // projectPathLabel
        //
        projectPathLabel.Anchor = AnchorStyles.Left;
        projectPathLabel.AutoSize = true;
        projectPathLabel.Location = new Point(3, 10);
        projectPathLabel.Name = "projectPathLabel";
        projectPathLabel.Size = new Size(92, 20);
        projectPathLabel.TabIndex = 0;
        projectPathLabel.Text = "Project Path";
        //
        // projectPathTextBox
        //
        projectPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        projectPathTextBox.Location = new Point(123, 6);
        projectPathTextBox.Name = "projectPathTextBox";
        projectPathTextBox.Size = new Size(604, 27);
        projectPathTextBox.TabIndex = 1;
        //
        // browseProjectButton
        //
        browseProjectButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        browseProjectButton.Location = new Point(733, 5);
        browseProjectButton.Name = "browseProjectButton";
        browseProjectButton.Size = new Size(104, 30);
        browseProjectButton.TabIndex = 2;
        browseProjectButton.Text = "Browse...";
        browseProjectButton.UseVisualStyleBackColor = true;
        browseProjectButton.Click += browseProjectButton_Click;
        //
        // refreshExtensionsButton
        //
        refreshExtensionsButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        refreshExtensionsButton.Location = new Point(843, 5);
        refreshExtensionsButton.Name = "refreshExtensionsButton";
        refreshExtensionsButton.Size = new Size(114, 30);
        refreshExtensionsButton.TabIndex = 3;
        refreshExtensionsButton.Text = "Scan";
        refreshExtensionsButton.UseVisualStyleBackColor = true;
        refreshExtensionsButton.Click += refreshExtensionsButton_Click;
        //
        // outputPathLabel
        //
        outputPathLabel.Anchor = AnchorStyles.Left;
        outputPathLabel.AutoSize = true;
        outputPathLabel.Location = new Point(3, 50);
        outputPathLabel.Name = "outputPathLabel";
        outputPathLabel.Size = new Size(87, 20);
        outputPathLabel.TabIndex = 4;
        outputPathLabel.Text = "Output Path";
        //
        // outputPathTextBox
        //
        outputPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        outputPathTextBox.Location = new Point(123, 46);
        outputPathTextBox.Name = "outputPathTextBox";
        outputPathTextBox.Size = new Size(604, 27);
        outputPathTextBox.TabIndex = 5;
        //
        // browseOutputButton
        //
        browseOutputButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        browseOutputButton.Location = new Point(733, 45);
        browseOutputButton.Name = "browseOutputButton";
        browseOutputButton.Size = new Size(104, 30);
        browseOutputButton.TabIndex = 6;
        browseOutputButton.Text = "Save As...";
        browseOutputButton.UseVisualStyleBackColor = true;
        browseOutputButton.Click += browseOutputButton_Click;
        //
        // minifyJsonCheckBox
        //
        minifyJsonCheckBox.Anchor = AnchorStyles.Left;
        minifyJsonCheckBox.AutoSize = true;
        minifyJsonCheckBox.Location = new Point(843, 48);
        minifyJsonCheckBox.Name = "minifyJsonCheckBox";
        minifyJsonCheckBox.Size = new Size(114, 24);
        minifyJsonCheckBox.TabIndex = 7;
        minifyJsonCheckBox.Text = "Compact JSON";
        minifyJsonCheckBox.UseVisualStyleBackColor = true;
        //
        // extensionLayout
        //
        extensionLayout.ColumnCount = 2;
        extensionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
        extensionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        extensionLayout.Controls.Add(extensionOptionsGroup, 0, 0);
        extensionLayout.Controls.Add(extensionsListGroup, 1, 0);
        extensionLayout.Dock = DockStyle.Fill;
        extensionLayout.Location = new Point(3, 83);
        extensionLayout.Name = "extensionLayout";
        extensionLayout.RowCount = 1;
        extensionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        extensionLayout.Size = new Size(954, 433);
        extensionLayout.TabIndex = 8;
        //
        // extensionOptionsGroup
        //
        extensionOptionsGroup.Controls.Add(addExtensionButton);
        extensionOptionsGroup.Controls.Add(manualExtensionTextBox);
        extensionOptionsGroup.Controls.Add(manualExtensionLabel);
        extensionOptionsGroup.Controls.Add(clearExtensionsButton);
        extensionOptionsGroup.Controls.Add(selectAllExtensionsButton);
        extensionOptionsGroup.Dock = DockStyle.Fill;
        extensionOptionsGroup.Location = new Point(3, 3);
        extensionOptionsGroup.Name = "extensionOptionsGroup";
        extensionOptionsGroup.Padding = new Padding(12);
        extensionOptionsGroup.Size = new Size(274, 427);
        extensionOptionsGroup.TabIndex = 0;
        extensionOptionsGroup.TabStop = false;
        extensionOptionsGroup.Text = "Extension Options";
        //
        // addExtensionButton
        //
        addExtensionButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        addExtensionButton.Location = new Point(15, 159);
        addExtensionButton.Name = "addExtensionButton";
        addExtensionButton.Size = new Size(244, 34);
        addExtensionButton.TabIndex = 4;
        addExtensionButton.Text = "Add Extension ->";
        addExtensionButton.UseVisualStyleBackColor = true;
        addExtensionButton.Click += addExtensionButton_Click;
        //
        // manualExtensionTextBox
        //
        manualExtensionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        manualExtensionTextBox.Location = new Point(15, 126);
        manualExtensionTextBox.Name = "manualExtensionTextBox";
        manualExtensionTextBox.PlaceholderText = ".cs, py, json";
        manualExtensionTextBox.Size = new Size(244, 27);
        manualExtensionTextBox.TabIndex = 3;
        //
        // manualExtensionLabel
        //
        manualExtensionLabel.AutoSize = true;
        manualExtensionLabel.Location = new Point(15, 103);
        manualExtensionLabel.Name = "manualExtensionLabel";
        manualExtensionLabel.Size = new Size(115, 20);
        manualExtensionLabel.TabIndex = 2;
        manualExtensionLabel.Text = "Manual Include";
        //
        // clearExtensionsButton
        //
        clearExtensionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        clearExtensionsButton.Location = new Point(15, 63);
        clearExtensionsButton.Name = "clearExtensionsButton";
        clearExtensionsButton.Size = new Size(244, 34);
        clearExtensionsButton.TabIndex = 1;
        clearExtensionsButton.Text = "Clear All";
        clearExtensionsButton.UseVisualStyleBackColor = true;
        clearExtensionsButton.Click += clearExtensionsButton_Click;
        //
        // selectAllExtensionsButton
        //
        selectAllExtensionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        selectAllExtensionsButton.Location = new Point(15, 27);
        selectAllExtensionsButton.Name = "selectAllExtensionsButton";
        selectAllExtensionsButton.Size = new Size(244, 34);
        selectAllExtensionsButton.TabIndex = 0;
        selectAllExtensionsButton.Text = "Select All";
        selectAllExtensionsButton.UseVisualStyleBackColor = true;
        selectAllExtensionsButton.Click += selectAllExtensionsButton_Click;
        //
        // extensionsListGroup
        //
        extensionsListGroup.Controls.Add(extensionsCheckedListBox);
        extensionsListGroup.Dock = DockStyle.Fill;
        extensionsListGroup.Location = new Point(283, 3);
        extensionsListGroup.Name = "extensionsListGroup";
        extensionsListGroup.Padding = new Padding(12);
        extensionsListGroup.Size = new Size(668, 427);
        extensionsListGroup.TabIndex = 1;
        extensionsListGroup.TabStop = false;
        extensionsListGroup.Text = "Extensions To Be Added";
        //
        // extensionsCheckedListBox
        //
        extensionsCheckedListBox.CheckOnClick = true;
        extensionsCheckedListBox.Dock = DockStyle.Fill;
        extensionsCheckedListBox.FormattingEnabled = true;
        extensionsCheckedListBox.IntegralHeight = false;
        extensionsCheckedListBox.Location = new Point(12, 32);
        extensionsCheckedListBox.Name = "extensionsCheckedListBox";
        extensionsCheckedListBox.Size = new Size(644, 383);
        extensionsCheckedListBox.TabIndex = 0;
        //
        // bundleButton
        //
        bundleButton.Anchor = AnchorStyles.Right;
        bundleButton.Location = new Point(836, 529);
        bundleButton.Name = "bundleButton";
        bundleButton.Size = new Size(121, 34);
        bundleButton.TabIndex = 9;
        bundleButton.Text = "Bundle";
        bundleButton.UseVisualStyleBackColor = true;
        bundleButton.Click += bundleButton_Click;
        //
        // statusLabel
        //
        statusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        statusLabel.AutoEllipsis = true;
        statusLabel.Location = new Point(3, 568);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(954, 23);
        statusLabel.TabIndex = 10;
        statusLabel.Text = "Select a project folder to begin.";
        statusLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(984, 621);
        Controls.Add(rootLayout);
        MinimumSize = new Size(900, 500);
        Name = "MainForm";
        Padding = new Padding(12);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code Bundler";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        extensionLayout.ResumeLayout(false);
        extensionOptionsGroup.ResumeLayout(false);
        extensionOptionsGroup.PerformLayout();
        extensionsListGroup.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
}
