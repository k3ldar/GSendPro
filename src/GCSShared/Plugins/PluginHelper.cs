using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PluginManager.Abstractions;

namespace GSendShared.Plugins
{
    public sealed class PluginHelper : IPluginHelper
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly ILogger _logger;

        public PluginHelper(ILogger logger, IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void InitializeAllPlugins(IPluginHost pluginHost)
        {
            if (pluginHost == null) 
                throw new ArgumentNullException(nameof(pluginHost));

            if (pluginHost.Usage == PluginUsage.None)
                throw new InvalidOperationException("Invalid host usage");

            List<IGSendPluginModule> plugins = _pluginClassesService.GetPluginClasses<IGSendPluginModule>();

            foreach (IGSendPluginModule plugin in plugins)
            {
                if (plugin.Usage != pluginHost.Usage)
                {
                    _logger.AddToLog(PluginManager.LogLevel.Warning, $"Attempt to load invalid plugin: {plugin.Name}");
                    continue;
                }
            }
        }
    }
}
