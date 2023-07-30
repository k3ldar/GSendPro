using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;
using Shared.Classes;
using SharedLib.Win.Classes;

namespace GSendCommon
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRunProgram, WindowsRunProgram>();
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
