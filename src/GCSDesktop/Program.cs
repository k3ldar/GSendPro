using System;
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

namespace GSendDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
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