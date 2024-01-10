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
    public class FindMenuItemTests
    {
        [TestMethod]
        public void Construct_FindMenuItem_Success()
        {
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            FindMenuItem sut = new(parentMenu, new MockITextEditor());
            Assert.IsNotNull(sut);
            Assert.IsNull(sut.MenuImage);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.IsNotNull(sut.ParentMenu);
            Assert.AreSame(parentMenu, sut.ParentMenu);
            Assert.AreEqual("Find", sut.Text);
            Assert.AreEqual(0, sut.Index);
            Assert.IsFalse(sut.ReceiveClientMessages);
            Assert.IsTrue(sut.GetShortcut([], out string grpName, out string shrtCutName));
            Assert.AreEqual("Search Menu", grpName);
            Assert.AreEqual("Find", shrtCutName);
            Assert.IsFalse(sut.IsChecked());
            Assert.IsFalse(sut.IsEnabled());
            Assert.IsTrue(sut.IsVisible());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ParentNull_Throws_ArgumentNullException()
        {
            new FindMenuItem(null, new Mock<ITextEditor>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_TextEditorNull_Throws_ArgumentNullException()
        {
            new FindMenuItem(new MockPluginMenu("test", MenuType.MenuItem, null), null);
        }

        [TestMethod]
        public void UpdateHost_DoesNotCrash_Success()
        {
            FindMenuItem sut = new(new MockPluginMenu("test", MenuType.MenuItem, null), new MockITextEditor());
            Assert.IsNotNull(sut);
            sut.UpdateHost<FindMenuItem>(null);
        }

        [TestMethod]
        public void IsEnabled_HasText_ReturnsTrue()
        {
            MockITextEditor mockITextEditor = new();
            mockITextEditor.Text = "Some Text";
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            FindMenuItem sut = new(parentMenu, mockITextEditor);
            Assert.IsTrue(sut.IsEnabled());
        }
    }
}
