using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GrblTuningWizard;

using GSendShared.Interfaces;
using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.GrblTuningWizardTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class GSendProTuningWizardPluginTests
    {
        [TestMethod]
        public void Create_ValidInstance_Success()
        {
            GSendProTuningWizardPlugin sut = new();
            Assert.IsNotNull(sut);
            Assert.AreEqual("GRBL Tuning Wizard", sut.Name);
            Assert.AreEqual(1u, sut.Version);
            Assert.AreEqual(PluginHosts.Sender, sut.Host);
            Assert.AreEqual(PluginOptions.HasMenuItems | PluginOptions.MessageReceived, sut.Options);
        }

        [TestMethod]
        public void Validate_MenuItems_Success()
        {
            IPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            MockSenderPluginHost pluginHost = new MockSenderPluginHost(parentMenu);

            GSendProTuningWizardPlugin sut = new();

            IReadOnlyList<IPluginMenu> menuItems = sut.MenuItems(pluginHost);

            Assert.AreEqual(1, menuItems.Count);
            Assert.AreEqual("Grbl Tuning Wizard", menuItems[0].Text);
            Assert.AreEqual(MenuParent.Tools, pluginHost.GetMenuCalls[0]);
        }
    }
}
