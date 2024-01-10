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
    public class ReplaceMenuItemTests
    {
        [TestMethod]
        public void Construct_ReplaceMenuItem_Success()
        {
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            ReplaceMenuItem sut = new(parentMenu, new MockITextEditor());
            Assert.IsNotNull(sut);
            Assert.IsNull(sut.MenuImage);
            Assert.AreEqual(MenuType.MenuItem, sut.MenuType);
            Assert.IsNotNull(sut.ParentMenu);
            Assert.AreSame(parentMenu, sut.ParentMenu);
            Assert.AreEqual("Replace", sut.Text);
            Assert.AreEqual(1, sut.Index);
            Assert.IsFalse(sut.ReceiveClientMessages);
            Assert.IsTrue(sut.GetShortcut([], out string grpName, out string shrtCutName));
            Assert.AreEqual("Search Menu", grpName);
            Assert.AreEqual("Replace", shrtCutName);
            Assert.IsFalse(sut.IsChecked());
            Assert.IsFalse(sut.IsEnabled());
            Assert.IsTrue(sut.IsVisible());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ParentNull_Throws_ArgumentNullException()
        {
            new ReplaceMenuItem(null, new Mock<ITextEditor>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_TextEditorNull_Throws_ArgumentNullException()
        {
            new ReplaceMenuItem(new MockPluginMenu("test", MenuType.MenuItem, null), null);
        }

        [TestMethod]
        public void UpdateHost_DoesNotCrash_Success()
        {
            ReplaceMenuItem sut = new(new MockPluginMenu("test", MenuType.MenuItem, null), new MockITextEditor());
            Assert.IsNotNull(sut);
            sut.UpdateHost<ReplaceMenuItem>(null);
        }

        [TestMethod]
        public void IsEnabled_HasText_ReturnsTrue()
        {
            MockITextEditor mockITextEditor = new();
            mockITextEditor.Text = "Some Text";
            MockPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            ReplaceMenuItem sut = new(parentMenu, mockITextEditor);
            Assert.IsTrue(sut.IsEnabled());
        }
    }
}
