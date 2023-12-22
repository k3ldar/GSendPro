using GSendShared.Helpers;
using GSendShared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendShared
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupEncryption();
            services.AddSingleton<IServerConfigurationUpdated, ServerConfigurationUpdated>();
            services.AddTransient<ICommonUtils, CommonUtils>();
        }

        public void Finalise()
        {
            // required by interface, not used in this context
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void Initialise(ILogger logger)
        {
            // required by interface, not used in this context
        }
    }
}
