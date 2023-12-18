using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GSendControls.Abstractions;
using GSendControls.Plugins;
using GSendControls.Plugins.InternalPlugins.HelpMenu;
using GSendControls.Plugins.InternalPlugins.ServerMenu;

using GSendShared;
using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Shared.Plugins
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ServerMenuPluginTests
    {
        [TestMethod]
        public void ServerMenuPlugin_ConstructValidInstance_Success()
        {
            IPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            MockSenderPluginHost pluginHost = new(parentMenu);
            ServerMenuPlugin sut = new(new ServerConfigurationUpdated(), new CommonUtils(), new MockRunProgram());
            sut.Initialize(pluginHost);

            Assert.IsNotNull(sut);
            Assert.AreEqual("Server Menu", sut.Name);
            Assert.AreEqual((ushort)1, sut.Version);
            Assert.AreEqual(PluginHosts.Editor | PluginHosts.SenderHost, sut.Host);
            Assert.AreEqual(PluginOptions.HasMenuItems, sut.Options);
            IReadOnlyList<IPluginMenu> menuItems = sut.MenuItems;
            Assert.IsNotNull(menuItems);
            Assert.AreEqual(12, menuItems.Count);
            // should not throw exception
            sut.ClientMessageReceived(null);

            Assert.AreEqual(0, pluginHost.GetMenuCalls.Count);
        }

        [TestMethod]
        public void SeperatorMenuItem_ConstructValidInstance_Success()
        {
            IPluginMenu parent = new MockPluginMenu("help", MenuType.MenuItem, null);
            SeperatorMenu sut = new(parent, 3);
            Assert.IsNotNull(sut);
            Assert.AreEqual("Seperator Menu", sut.Text);
            Assert.AreEqual(3, sut.Index);
            Assert.AreEqual(MenuType.Seperator, sut.MenuType);
            Assert.AreEqual(parent, sut.ParentMenu);
            Assert.IsFalse(sut.GetShortcut(out string _, out string _));
            Assert.IsFalse(sut.IsChecked());
            Assert.IsTrue(sut.IsEnabled());

            // should not throw exceptions
            sut.ClientMessageReceived(null);
            sut.UpdateHost<IPluginMenu>(null);
        }

        [TestMethod]
        public void HomePageMenuItem_ConstructValidInstance_Success()
        {
            IPluginMenu parent = new MockPluginMenu("help", MenuType.MenuItem, null);
            HomePageMenu sut = new(parent);
            Assert.IsNotNull(sut);
            Assert.AreEqual("Home Page", sut.Text);
            Assert.AreEqual(4, sut.Index);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.AreEqual(parent, sut.ParentMenu);
            Assert.IsFalse(sut.GetShortcut(out string _, out string _));
            Assert.IsFalse(sut.IsChecked());
            Assert.IsTrue(sut.IsEnabled());

            // should not throw exceptions
            sut.ClientMessageReceived(null);
            sut.UpdateHost<IPluginMenu>(null);
        }
    }
}
