using GSendControls;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;

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

            applicationPluginManager.RegisterPlugin(typeof(GSendApi.PluginInitialization).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(Internal.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendAnalyser.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendCommon.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendShared.PluginInitialisation).Assembly.Location);



            applicationPluginManager.ConfigureServices();

            try
            {
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