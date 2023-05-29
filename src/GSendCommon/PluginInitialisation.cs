using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendCommon
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISubPrograms, SubPrograms>();
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
