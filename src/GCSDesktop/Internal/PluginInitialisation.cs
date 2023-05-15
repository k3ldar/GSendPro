using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendApi;
using GSendCommon;
using GSendDesktop.Abstractions;
using GSendDesktop.Forms;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Providers;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using SimpleDB;

namespace GSendDesktop.Internal
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGSendContext, GSendContext>();
            services.AddSingleton(new GSendSettings());
            services.AddSingleton(new ApiSettings(new Uri("https://localhost:7154/")));
            services.AddSingleton<MachineApiWrapper>();
            services.AddTransient<IMessageNotifier, MessageNotifier>();
            services.AddTransient<IComPortProvider, ComPortProvider>();
            services.AddTransient<ICommandProcessor, CommandProcessor>();
            services.AddTransient<FormMain>();
            services.AddTransient<FrmAddMachine>();
        }

        public void Finalise()
        {
            
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(ILogger logger)
        {
            
        }
    }
}
