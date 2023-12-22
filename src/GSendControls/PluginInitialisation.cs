using GSendControls.Abstractions;
using GSendControls.Forms;
using GSendControls.Plugins;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendControls
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPluginHelper, PluginHelper>();
            services.AddTransient(typeof(FrmConfigureServer));
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
