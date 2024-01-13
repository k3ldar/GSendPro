using System;
using System.Diagnostics.CodeAnalysis;

using GSendControls.Plugins.InternalPlugins.HelpMenu;
using GSendControls.Plugins.InternalPlugins.SearchMenu;

using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.HelpMenuTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class HomePageMenuTests
    {
        [TestMethod]
        public void Construct_HomePageMenuItem_Success()
        {
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            HomePageMenu sut = new(parentMenu, new MockRunProgram());
            Assert.IsNotNull(sut);
            Assert.IsNull(sut.MenuImage);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.IsNotNull(sut.ParentMenu);
            Assert.AreSame(parentMenu, sut.ParentMenu);
            Assert.AreEqual("Home Page", sut.Text);
            Assert.AreEqual(4, sut.Index);
            Assert.IsFalse(sut.ReceiveClientMessages);
            Assert.IsFalse(sut.GetShortcut([], out string grpName, out string shrtCutName));
            Assert.IsNull(grpName);
            Assert.IsNull(shrtCutName);
            Assert.IsFalse(sut.IsChecked());
            Assert.IsTrue(sut.IsEnabled());
            Assert.IsTrue(sut.IsVisible());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ParentNull_Throws_ArgumentNullException()
        {
            new HomePageMenu(null, new MockRunProgram());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_RunProgramNull_Throws_ArgumentNullException()
        {
            new HomePageMenu(new MockPluginMenu("test", MenuType.MenuItem, null), null);
        }

        [TestMethod]
        public void UpdateHost_DoesNotCrash_Success()
        {
            HomePageMenu sut = new(new MockPluginMenu("test", MenuType.MenuItem, null), new MockRunProgram());
            Assert.IsNotNull(sut);
            sut.UpdateHost<FindMenuItem>(null);
        }

        [TestMethod]
        public void Clicked_RunsHomePageUsingShellExecute_Success()
        {
            MockRunProgram mockRunProgram = new();
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            HomePageMenu sut = new(parentMenu, mockRunProgram);
            sut.Clicked();
            Assert.AreEqual("https://www.gsend.pro/", mockRunProgram.ProgramName);
            Assert.IsNull(mockRunProgram.Parameters);
            Assert.IsTrue(mockRunProgram.UseShellExecute);
            Assert.IsFalse(mockRunProgram.WaitForFinish);
            Assert.AreEqual(500, mockRunProgram.TimeoutMilliseconds);
        }
    }
}
