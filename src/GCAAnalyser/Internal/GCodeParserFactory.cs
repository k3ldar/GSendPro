using GSendShared;
using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyzer.Internal
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
            return CreateParser(_subprograms);
        }

        public IGCodeParser CreateParser(ISubprograms subprograms)
        {
            return new GCodeParser(_pluginClassesService, subprograms);
        }
    }
}
