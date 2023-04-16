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

            IServiceCollection serviceCollection = new ServiceCollection();
            RegisterServices(serviceCollection);
            IGSendContext gSendContext = new GSendContext(serviceCollection);
            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(gSendContext.ServiceProvider.GetRequiredService<FormMain>());
            }
            finally
            {
                gSendContext.CloseContext();
            }
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {

            //GSendSettings settings = settingsProvider.GetSettings<GSendSettings>(GSendShared.Constants.SettingsName);
            serviceCollection.AddSingleton(new GSendSettings());
            serviceCollection.AddSingleton(new ApiSettings(new Uri("https://localhost:7154/")));
            serviceCollection.AddSingleton<MachineApiWrapper>();
            serviceCollection.AddTransient<IMessageNotifier, MessageNotifier>();
            serviceCollection.AddTransient<IComPortProvider, ComPortProvider>();
            serviceCollection.AddTransient<ICommandProcessor, CommandProcessor>();
            //serviceCollection.AddSingleton<IProcessorMediator, ProcessorMediator>();
            serviceCollection.AddTransient<FormMain>();
            serviceCollection.AddTransient<FrmAddMachine>();
        }
    }
}