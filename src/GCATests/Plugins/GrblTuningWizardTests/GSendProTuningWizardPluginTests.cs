using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GrblTuningWizard;

using GSendShared.Interfaces;
using GSendShared.Plugins;

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
            Assert.AreEqual(PluginUsage.Sender, sut.Usage);
            Assert.AreEqual(PluginOptions.HasToolbarButtons | PluginOptions.HasMenuItems, sut.Options);
        }

        [TestMethod]
        public void Validate_MenuItems_Success()
        {
            GSendProTuningWizardPlugin sut = new();

            IReadOnlyList<IPluginMenu> menuItems = sut.MenuItems;

            Assert.AreEqual(1, menuItems.Count);
            Assert.AreEqual("Tuning Wizard", menuItems[0].Name);
        }

        [TestMethod]
        public void Validate_Shortcuts_Success()
        {
            GSendProTuningWizardPlugin sut = new();

            IReadOnlyList<IShortcut> menuItems = sut.Shortcuts;

            Assert.AreEqual(0, menuItems.Count);
        }
    }
}
