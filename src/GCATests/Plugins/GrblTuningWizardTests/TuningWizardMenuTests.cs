using System.Diagnostics.CodeAnalysis;

using GrblTuningWizard;

using GSendShared.Plugins;

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
            TuningWizardMenuItem sut = new TuningWizardMenuItem();
            Assert.AreEqual("Tuning Wizard", sut.Name);
            Assert.AreEqual(-1, sut.Index);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.AreEqual(MenuParent.Tools, sut.ParentMenu);
        }
    }
}
