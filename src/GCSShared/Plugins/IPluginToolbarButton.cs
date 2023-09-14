using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Plugins
{
    public interface IPluginToolbarButton : IPluginItemBase
    {
        /// <summary>
        /// Picture to be displayed on the button, null for no image
        /// </summary>
        Image Picture { get; }
    }
}
