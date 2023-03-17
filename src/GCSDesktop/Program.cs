using System;
using System.Windows.Forms;

using PluginManager;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Shared.Classes;
using GSendApi;
using GSendDesktop.Abstractions;
using GSendDesktop.Internal;
using GSendShared.Interfaces;
using GSendShared.Providers;
using GSendDesktop.Forms;
using GSendShared;
using GSendCommon;

namespace GSendDesktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            RegisterServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(Services.GetRequiredService<FormMain>());
            }
            finally
            {
                Services.Dispose();
            }
        }

        internal static ServiceProvider Services { get; private set; }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGSendContext, GSendContext>();
            serviceCollection.AddSingleton(new ApiSettings(new Uri("https://localhost:7154/")));
            serviceCollection.AddSingleton<MachineApiWrapper>();
            serviceCollection.AddTransient<IMessageNotifier, MessageNotifier>();
            serviceCollection.AddTransient<IComPortProvider, ComPortProvider>();
            serviceCollection.AddTransient<ICommandProcessor, CommandProcessor>();
            serviceCollection.AddSingleton<IProcessorMediator, ProcessorMediator>();
            serviceCollection.AddTransient<FormMain>();
            serviceCollection.AddTransient<FrmAddMachine>();
        }
    }
}