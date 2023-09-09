using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Plugins
{
    public interface IPluginModule
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
        PluginUsage Usage { get; }

        /// <summary>
        /// Requested Options
        /// </summary>
        PluginOptions Options { get; }

        /// <summary>
        /// Menu items supplied by plugin
        /// </summary>
        IReadOnlyList<IPluginMenu> MenuItems { get; }
    }
}
