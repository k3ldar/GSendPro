using System;
using System.Configuration;
using System.Runtime;
using System.Windows.Forms;

using GSendApi;

using GSendCommon;

using GSendDesktop.Abstractions;
using GSendDesktop.Forms;
using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Providers;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using PluginManager;

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

            ApplicationPluginManager applicationPluginManager = new ApplicationPluginManager(
                new PluginManagerConfiguration(),
                new PluginSettings());

            applicationPluginManager.RegisterPlugin(typeof(ApplicationPluginManager).Assembly.Location);
            applicationPluginManager.RegisterPlugin(typeof(PluginSetting).Assembly.Location);


            applicationPluginManager.ConfigureServices();

            IGSendContext gSendContext = applicationPluginManager.GetServiceProvider.GetService<IGSendContext>();
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