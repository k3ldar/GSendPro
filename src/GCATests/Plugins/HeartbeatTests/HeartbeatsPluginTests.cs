using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GSendControls.Abstractions;
using GSendControls.Plugins.InternalPlugins.Hearbeats;

using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.HeartbeatTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class HeartbeatsPluginTests
    {
        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            HearbeatsPlugin sut = new();
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType<IGSendPluginModule>(sut);
            Assert.AreEqual("Heartbeats", sut.Name);
            Assert.AreEqual(1u, sut.Version);
            Assert.AreEqual(PluginHosts.Sender, sut.Host);
            Assert.AreEqual(PluginOptions.HasControls, sut.Options);
            Assert.IsNull(sut.MenuItems);
            Assert.IsNull(sut.ToolbarItems);
            Assert.IsFalse(sut.ReceiveClientMessages);
        }

        [TestMethod]
        public void Validate_ControlItems_Success()
        {
            IPluginMenu parentMenu = new MockPluginMenu("test", MenuType.MenuItem, null);
            MockSenderPluginHost pluginHost = new(parentMenu);

            HearbeatsPlugin sut = new();
            sut.Initialize(pluginHost);
            IReadOnlyList<IPluginControl> controlItems = sut.ControlItems;

            Assert.AreEqual(1, controlItems.Count);
            Assert.AreEqual("Heartbeat Graphs", controlItems[0].Text);
        }

        [TestMethod]
        public void ClientMessageReceived_DoesNotThrowException()
        {
            HearbeatsPlugin sut = new();
            bool exceptionRaised = false;
            try
            {
                sut.ClientMessageReceived(null);
            }
            catch
            {
                exceptionRaised = true;
            }

            Assert.IsFalse(exceptionRaised);
        }

        [TestMethod]
        public void Initialize_DoesNotThrowException()
        {
            HearbeatsPlugin sut = new();
            bool exceptionRaised = false;
            try
            {
                sut.Initialize(null);
            }
            catch
            {
                exceptionRaised = true;
            }

            Assert.IsFalse(exceptionRaised);
        }
    }
}
