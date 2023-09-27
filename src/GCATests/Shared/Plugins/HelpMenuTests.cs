using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Plugins;
using GSendShared.Plugins.InternalPlugins.HelpMenu;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Shared.Plugins
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HelpMenuPluginTests
    {
        [TestMethod]
        public void HelpMenuPlugin_ConstructValidInstance_Success()
        {
            IPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            MockSenderPluginHost pluginHost = new MockSenderPluginHost(parentMenu);
            HelpMenuPlugin sut = new HelpMenuPlugin();
            Assert.IsNotNull(sut);
            Assert.AreEqual("Help Menu", sut.Name);
            Assert.AreEqual((ushort)1, sut.Version);
            Assert.AreEqual(PluginHosts.Any, sut.Host);
            Assert.AreEqual(PluginOptions.HasMenuItems, sut.Options);
            IReadOnlyList<IPluginMenu> menuItems = sut.MenuItems(pluginHost);
            Assert.IsNotNull(menuItems);
            Assert.AreEqual(6, menuItems.Count);
            // should not throw exception
            sut.ClientMessageReceived(null);

            Assert.AreEqual(MenuParent.Help, pluginHost.GetMenuCalls[0]);
        }

        [TestMethod]
        public void HelpMenuItem_ConstructValidInstance_Success()
        {
            IPluginMenu parent = new MockPluginMenu("help", MenuType.MenuItem, null);
            HelpMenuItem sut = new HelpMenuItem(parent);

            Assert.IsNotNull(sut);
            Assert.AreEqual("Help", sut.Text);
            Assert.AreEqual(0, sut.Index);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.AreEqual(parent, sut.ParentMenu);
            Assert.IsFalse(sut.GetShortcut(out string _, out string _));
            Assert.IsFalse(sut.IsChecked());
            Assert.IsTrue(sut.IsEnabled());

            // should not throw exceptions
            sut.ClientMessageReceived(null);
            sut.UpdateHost<IPluginMenu>(null);
        }

        [TestMethod]
        public void SeperatorMenuItem_ConstructValidInstance_Success()
        {
            IPluginMenu parent = new MockPluginMenu("help", MenuType.MenuItem, null);
            SeperatorMenu sut = new SeperatorMenu(parent, 3);
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
        public void BugsAndIdeasMenuItem_ConstructValidInstance_Success()
        {
            IPluginMenu parent = new MockPluginMenu("help", MenuType.MenuItem, null);
            BugsAndIdeasMenu sut = new BugsAndIdeasMenu(parent);
            Assert.IsNotNull(sut);
            Assert.AreEqual("Bugs and Ideas", sut.Text);
            Assert.AreEqual(2, sut.Index);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
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
            HomePageMenu sut = new HomePageMenu(parent);
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
