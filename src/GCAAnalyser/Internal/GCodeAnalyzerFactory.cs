using GSendAnalyser.Analysers;

using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeAnalyzerFactory : IGCodeAnalyzerFactory
    {
        private readonly IPluginClassesService _pluginClassesService;

        public GCodeAnalyzerFactory(IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
        }

        public IReadOnlyList<IGCodeAnalyzer> Create()
        {
            List<IGCodeAnalyzer> result = _pluginClassesService.GetPluginClasses<IGCodeAnalyzer>()
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
