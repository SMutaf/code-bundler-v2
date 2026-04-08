using CodeBundler.Core.Abstractions;
using CodeBundler.Core.Services;

namespace CodeBundler.App;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        IProjectTraversalPolicy traversalPolicy = new DefaultProjectTraversalPolicy();
        IProjectScanner projectScanner = new ProjectScanner(traversalPolicy);
        IBundleExportService bundleExportService = new JsonBundleExportService();
        IBundleOrchestrator bundleOrchestrator = new BundleOrchestrator(projectScanner, bundleExportService);

        Application.Run(new MainForm(projectScanner, bundleOrchestrator));
    }
}
