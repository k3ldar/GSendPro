using GSendControls;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;
using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ThreadManager.Initialise(new SharedLib.Win.WindowsCpuUsage());
            ThreadManager.AllowThreadPool = true;
            ThreadManager.MaximumPoolSize = 5000;

            ApplicationPluginManager applicationPluginManager = new(
                new PluginManagerConfiguration(),
                new PluginSettings());

            Environment.SetEnvironmentVariable("GSendProRootPath",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder));

            Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath")));

            applicationPluginManager.RegisterPlugin(typeof(GSendApi.PluginInitialization).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(Internal.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendAnalyzer.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendCommon.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendShared.PluginInitialisation).Assembly.Location);



            IServiceCollection serviceCollection = new ServiceCollection();
            applicationPluginManager.ConfigureServices(serviceCollection);
            applicationPluginManager.ServiceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                applicationPluginManager.ServiceProvider.GetRequiredService<ISubprograms>();
                GlobalExceptionHandler.InitializeGlobalExceptionHandlers(applicationPluginManager.ServiceProvider.GetRequiredService<ILogger>());
                ApplicationConfiguration.Initialize();
                Application.Run(applicationPluginManager.ServiceProvider.GetRequiredService<FrmMain>());
            }
            finally
            {
                ThreadManager.CancelAll();
            }
        }
    }
}