using GSendShared;
using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeParserFactory : IGCodeParserFactory
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly ISubPrograms _subPrograms;

        public GCodeParserFactory(IPluginClassesService pluginClassesService, ISubPrograms subPrograms)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _subPrograms = subPrograms ?? throw new ArgumentNullException(nameof(subPrograms));
        }

        public IGCodeParser CreateParser()
        {
            return new GCodeParser(_pluginClassesService, _subPrograms);
        }
    }
}
