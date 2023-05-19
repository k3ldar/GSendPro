using GSendAnalyser.Internal;

using GSendShared.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

namespace GSendAnalyser
{
    public sealed class PluginInitialisation : IPlugin
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGCodeAnalyzerFactory, GCodeAnalyzerFactory>();
            services.AddSingleton<IGCodeParserFactory, GCodeParserFactory>();
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
