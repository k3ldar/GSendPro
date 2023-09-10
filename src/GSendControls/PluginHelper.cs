using System;
using System.Collections.Generic;

using GSendShared.Interfaces;

using PluginManager.Abstractions;

namespace GSendShared.Plugins
{
    public sealed class PluginHelper : IPluginHelper
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly ILogger _logger;
        private readonly IPluginHelperService _pluginHelperService;

        public PluginHelper(ILogger logger, 
            IPluginHelperService pluginHelperService, 
            IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pluginHelperService = pluginHelperService ?? throw new ArgumentNullException(nameof(pluginHelperService));
        }

        public void AddMenu(object parent, IPluginMenu menu)
        {
            throw new NotImplementedException();
        }

        public void AddShortcut(List<IShortcut> shortcuts, IShortcut shortcut)
        {
            throw new NotImplementedException();
        }

        public void InitializeAllPlugins(ISenderPluginHost pluginHost)
        {
            if (pluginHost == null)
                throw new ArgumentNullException(nameof(pluginHost));

            List<IGSendPluginModule> plugins = _pluginClassesService.GetPluginClasses<IGSendPluginModule>();

            foreach (IGSendPluginModule plugin in plugins)
            {
                if (!plugin.Usage.HasFlag(pluginHost.Usage))
                {
                    _logger.AddToLog(PluginManager.LogLevel.Warning, $"Attempt to load invalid plugin: {plugin.Name}");
                    continue;
                }

                foreach (IPluginMenu pluginMenu in plugin.MenuItems)
                {
                    pluginHost.AddMenu(pluginMenu);
                }

                foreach (IShortcut shortcut in plugin.Shortcuts)
                {
                    pluginHost.AddShortcut(shortcut);
                }
            }
        }
    }
}
