using GSendApi;

using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeParserFactory : IGCodeParserFactory
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly IGSendApiWrapper _apiWrapper;

        public GCodeParserFactory(IPluginClassesService pluginClassesService, IGSendApiWrapper apiWrapper)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _apiWrapper = apiWrapper ?? throw new ArgumentNullException(nameof(apiWrapper));
        }

        public IGCodeParser CreateParser()
        {
            return new GCodeParser(_pluginClassesService, _apiWrapper);
        }
    }
}
