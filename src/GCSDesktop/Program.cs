using System;
using System.Windows.Forms;

using GSendControls;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager;

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

            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendAnalyser.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendCommon.PluginInitialisation).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(GSendShared.PluginInitialisation).Assembly.Location);


            applicationPluginManager.ConfigureServices();

            IGSendContext gSendContext = applicationPluginManager.ServiceProvider.GetService<IGSendContext>();
            try
            {
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