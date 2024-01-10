using System.Collections.Generic;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Abstractions
{
    public interface IGSendPluginModule
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Plugin Version
        /// </summary>
        ushort Version { get; }

        /// <summary>
        /// Where the plugin is used
        /// </summary>
        PluginHosts Host { get; }

        /// <summary>
        /// Requested Options
        /// </summary>
        PluginOptions Options { get; }

        /// <summary>
        /// Initializes the plugin
        /// </summary>
        /// <param name="pluginHost"></param>
        void Initialize(IPluginHost pluginHost);

        /// <summary>
        /// Menu items supplied by plugin
        /// </summary>
        IReadOnlyList<IPluginMenu> MenuItems { get; }

        /// <summary>
        /// Toolbar items to be displayed within the host
        /// </summary>
        IReadOnlyList<IPluginToolbarButton> ToolbarItems { get; }

        /// <summary>
        /// Controls to be displayed within host
        /// </summary>
        IReadOnlyList<IPluginControl> ControlItems { get; }

        void ClientMessageReceived(IClientBaseMessage clientBaseMessage);
    }
}
