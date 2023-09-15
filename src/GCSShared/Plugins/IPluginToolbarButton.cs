using System.Drawing;

namespace GSendShared.Plugins
{
    public interface IPluginToolbarButton : IPluginItemBase
    {
        /// <summary>
        /// Type of button to be created
        /// </summary>
        ButtonType ButtonType { get; }

        /// <summary>
        /// Picture to be displayed on the button, null for no image
        /// </summary>
        Image Picture { get; }
    }
}
