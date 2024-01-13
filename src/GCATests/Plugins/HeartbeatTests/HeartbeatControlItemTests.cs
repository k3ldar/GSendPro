using System.Diagnostics.CodeAnalysis;

using GSendControls.Abstractions;
using GSendControls.Plugins.InternalPlugins.Hearbeats;

using GSendShared.Plugins;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.HeartbeatTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class HeartbeatControlItemTests
    {
        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            HeartbeatControlItem sut = new();
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType<IPluginControl>(sut);
            Assert.AreEqual("Graphs", sut.Name);
            Assert.IsNotNull(sut.Control);
            Assert.AreEqual(ControlLocation.Secondary, sut.Location);
            Assert.AreEqual("Heartbeat Graphs", sut.Text);
            Assert.AreEqual(20, sut.Index);
            Assert.IsTrue(sut.ReceiveClientMessages);
            Assert.IsTrue(sut.IsEnabled());
        }

        [TestMethod]
        public void ClientMessageReceived_DoesNotThrowException()
        {
            HeartbeatControlItem sut = new();
            bool exceptionRaised = false;
            try
            {
                sut.UpdateHost<object>(null);
            }
            catch
            {
                exceptionRaised = true;
            }

            Assert.IsFalse(exceptionRaised);
        }
    }
}
