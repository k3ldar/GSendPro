using System;
using System.Diagnostics.CodeAnalysis;

using GSendControls.Abstractions;
using GSendControls.Plugins.InternalPlugins.SearchMenu;

using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace GSendTests.Plugins.SearchMenu
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GotoMenuItemTests
    {
        [TestMethod]
        public void Construct_GotoMenuItem_Success()
        {
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            GotoMenuItem sut = new(parentMenu, new MockITextEditor());
            Assert.IsNotNull(sut);
            Assert.IsNull(sut.MenuImage);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.IsNotNull(sut.ParentMenu);
            Assert.AreSame(parentMenu, sut.ParentMenu);
            Assert.AreEqual("Goto", sut.Text);
            Assert.AreEqual(20, sut.Index);
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
             new GotoMenuItem(null, new Mock<ITextEditor>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_TextEditorNull_Throws_ArgumentNullException()
        {
            new GotoMenuItem(new MockPluginMenu("test", MenuType.MenuItem, null), null);
        }

        [TestMethod]
        public void UpdateHost_DoesNotCrash_Success()
        {
            GotoMenuItem sut = new(new MockPluginMenu("test", MenuType.MenuItem, null), new MockITextEditor());
            Assert.IsNotNull(sut);
            sut.UpdateHost<GotoMenuItem>(null);
        }

        [TestMethod]
        public void IsEnabled_HasText_ReturnsTrue()
        {
            MockITextEditor mockITextEditor = new();
            mockITextEditor.Text = "Some Text";
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            GotoMenuItem sut = new(parentMenu, mockITextEditor);
            Assert.IsTrue(sut.IsEnabled());
        }

        [TestMethod]
        public void IsEnabled_HasNoText_ReturnsFalse()
        {
            MockITextEditor mockITextEditor = new();
            mockITextEditor.Text = "Some Text";
            mockITextEditor.LineCount = 0;
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            GotoMenuItem sut = new(parentMenu, mockITextEditor);
            Assert.IsFalse(sut.IsEnabled());
        }
    }
}
