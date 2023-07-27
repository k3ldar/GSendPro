using GSendShared.Abstractions;

using PluginManager.Abstractions;

namespace GSendAnalyzer.Internal
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
            return _pluginClassesService.GetPluginClasses<IGCodeAnalyzer>()
                .OrderBy(o => o.Order)
                .ToList();
        }
    }
}
