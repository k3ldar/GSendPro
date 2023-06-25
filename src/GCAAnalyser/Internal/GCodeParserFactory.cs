using GSendApi;

using GSendShared;
using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeParserFactory : IGCodeParserFactory
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly ISubprograms _subprograms;

        public GCodeParserFactory(IPluginClassesService pluginClassesService, ISubprograms subprograms)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _subprograms = subprograms ?? throw new ArgumentNullException(nameof(subprograms));
        }

        public IGCodeParser CreateParser()
        {
            return new GCodeParser(_pluginClassesService, _subprograms);
        }
    }
}
