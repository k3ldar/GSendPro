using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendControls.Plugins.InternalPlugins.HelpMenu;

using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.HelpMenuTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class HelpMenuItemTests
    {
        [TestMethod]
        public void Construct_HelpMenuItem_Success()
        {
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            HelpMenuItem sut = new(parentMenu, new MockRunProgram());
            Assert.IsNotNull(sut);
            Assert.IsNull(sut.MenuImage);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.IsNotNull(sut.ParentMenu);
            Assert.AreSame(parentMenu, sut.ParentMenu);
            Assert.AreEqual("Help", sut.Text);
            Assert.AreEqual(0, sut.Index);
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
            new HelpMenuItem(null, new MockRunProgram());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_RunProgramNull_Throws_ArgumentNullException()
        {
            new HelpMenuItem(new MockPluginMenu("test", MenuType.MenuItem, null), null);
        }

        [TestMethod]
        public void UpdateHost_DoesNotCrash_Success()
        {
            HelpMenuItem sut = new(new MockPluginMenu("test", MenuType.MenuItem, null), new MockRunProgram());
            Assert.IsNotNull(sut);
            sut.UpdateHost<HelpMenuItem>(null);
        }

        [TestMethod]
        public void Clicked_RunsHomePageUsingShellExecute_Success()
        {
            MockRunProgram mockRunProgram = new();
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            HelpMenuItem sut = new(parentMenu, mockRunProgram);
            sut.Clicked();
            Assert.AreEqual("https://www.gsend.pro/help", mockRunProgram.ProgramName);
            Assert.IsNull(mockRunProgram.Parameters);
            Assert.IsTrue(mockRunProgram.UseShellExecute);
            Assert.IsFalse(mockRunProgram.WaitForFinish);
            Assert.AreEqual(500, mockRunProgram.TimeoutMilliseconds);
        }
    }
}
