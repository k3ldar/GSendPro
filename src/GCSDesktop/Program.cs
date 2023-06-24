using System;
using System.IO;
using System.Windows.Forms;

using GSendControls;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;
using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendDesktop
{
    internal static class Program
    {
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

            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendAnalyser.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendCommon.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendShared.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendApi.PluginInitialization).Assembly.Location);



            applicationPluginManager.ConfigureServices();

            IGSendContext gSendContext = applicationPluginManager.ServiceProvider.GetService<IGSendContext>();
            try
            {
                GlobalExceptionHandler.InitializeGlobalExceptionHandlers(applicationPluginManager.ServiceProvider.GetRequiredService<ILogger>());
                ApplicationConfiguration.Initialize();
                Application.Run(gSendContext.ServiceProvider.GetRequiredService<FormMain>());
            }
            finally
            {
                ThreadManager.CancelAll();
                gSendContext.CloseContext();
            }
        }
    }
}