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
    public class SearchMenuPluginTests
    {
        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            SearchMenuPlugin sut = new();
            Assert.IsNotNull(sut);
            sut.Initialize(new MockEditorPluginHost());
            Assert.AreEqual(1u, sut.Version);
            Assert.AreEqual(PluginHosts.Editor, sut.Host);
            Assert.AreEqual(PluginOptions.HasMenuItems, sut.Options);
            Assert.IsNull(sut.ToolbarItems);
            Assert.AreEqual(5, sut.MenuItems.Count);
        }

        [TestMethod]
        public void ClientMessageReceived_NullValue_DoesNotRaiseException()
        {
            SearchMenuPlugin sut = new();
            Assert.IsNotNull(sut);
            sut.ClientMessageReceived(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Initialize_NullValue_Throws_ArgumentNullException()
        {
            SearchMenuPlugin sut = new();
            Assert.IsNotNull(sut);
            sut.Initialize(null);
        }
    }
}
