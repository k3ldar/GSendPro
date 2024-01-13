using GSendControls.Controls;

using GSendShared.Plugins;

namespace GSendControls.Abstractions
{
    public interface IPluginControl : IPluginItemBase
    {
        string Name { get; }

        PluginControl Control { get; }

        ControlLocation Location { get; }
    }
}
