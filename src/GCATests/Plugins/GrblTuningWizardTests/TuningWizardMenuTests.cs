using System.Diagnostics.CodeAnalysis;

using GrblTuningWizard;
using GSendControls.Abstractions;
using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.GrblTuningWizardTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TuningWizardMenuTests
    {
        [TestMethod]
        public void ConstructValidInstance_Success()
        {
            IPluginMenu pluginMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            TuningWizardMenuItem sut = new(pluginMenu);
            Assert.AreEqual("Grbl Tuning Wizard", sut.Text);
            Assert.AreEqual(-1, sut.Index);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.AreEqual(pluginMenu, sut.ParentMenu);
        }
    }
}
