using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeParserFactory : IGCodeParserFactory
    {
        private readonly IPluginClassesService _pluginClassesService;

        public GCodeParserFactory(IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
        }

        public IGCodeParser CreateParser()
        {
            return new GCodeParser(_pluginClassesService);
        }
    }
}
