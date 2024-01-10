using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendTests.Mocks
{
    internal class MockEditorPluginHost : IEditorPluginHost
    {
        public MockEditorPluginHost()
        {
            Editor = new MockITextEditor();
        }

        public bool IsDirty => throw new NotImplementedException();

        public bool IsSubprogram => throw new NotImplementedException();

        public string FileName => throw new NotImplementedException();

        public ITextEditor Editor { get; set; }

        public PluginHosts Host => throw new NotImplementedException();

        public int MaximumMenuIndex => throw new NotImplementedException();

        public IGSendContext GSendContext => throw new NotImplementedException();

        public void AddMenu(IPluginMenu pluginMenu)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(InformationType informationType, string message)
        {
            throw new NotImplementedException();
        }

        public void AddPlugin(IGSendPluginModule pluginModule)
        {
            throw new NotImplementedException();
        }

        public void AddToolbar(IPluginToolbarButton toolbarButton)
        {
            throw new NotImplementedException();
        }

        public IPluginMenu GetMenu(MenuParent menuParent)
        {
            throw new NotImplementedException();
        }
    }
}
