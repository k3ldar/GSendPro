using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GSendCommon;

using GSendService.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shared.Classes;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessorMediatorTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ThreadManager.Initialise();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ThreadManager.Finalise();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_Logger_Throws_ArgumentNullException()
        {
            new ProcessorMediator(null, new MockMachineProvider(), new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_MachineProvider_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new Logger(), null, new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_NotificationService_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new Logger(), new MockMachineProvider(), new MockComPortFactory(), null, new MockSettingsProvider());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ComPortFactory_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new Logger(), new MockMachineProvider(), null, new MockNotification(), new MockSettingsProvider());
        }

        [TestMethod]
        public void GetEvents_ReturnsAllRegisteredEventIds()
        {
            ProcessorMediator sut = new ProcessorMediator(new Logger(), 
                new MockMachineProvider(), new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());
            Assert.IsNotNull(sut);

            List<string> events = sut.GetEvents();
            Assert.AreEqual(3, events.Count);
            Assert.AreEqual("MachineAdd", events[0]);
            Assert.AreEqual("MachineRemove", events[1]);
            Assert.AreEqual("MachineUpdate", events[2]);
        }

        [TestMethod]
        [Ignore]
        public void ExecuteAsync_StartsAllMachines_Success()
        {
            throw new NotImplementedException("needs work");
            //ProcessorMediator sut = new ProcessorMediator(new Logger(), new MockMachineProvider(), new MockNotification());
            //CancellationTokenSource cancellationToken = new CancellationTokenSource();

            //Task runResult = Task.Run(() => sut.InternalExecute(cancellationToken.Token));

            //while (runResult.Status == TaskStatus.WaitingForActivation)
            //    Thread.Sleep(0);

            //cancellationToken.Cancel();
        }

        [TestMethod]
        public void OpenProcessors_OpensAndInitializesAllProcessors_Success()
        {
            ProcessorMediator sut = new ProcessorMediator(new Logger(), 
                new MockMachineProvider(new string[] { "Machine 1", "Machine 2" }), 
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());

            sut.OpenProcessors();

            Assert.AreEqual(2, sut.Machines.Count);
        }

        [TestMethod]
        public void CloseProcessors_ClosesAllProcessors_Success()
        {
            
            ProcessorMediator sut = new ProcessorMediator(new Logger(),
                new MockMachineProvider(new string[] { "Machine 1", "Machine 2" }),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());

            sut.OpenProcessors();

            Assert.AreEqual(2, sut.Machines.Count);

            foreach (IGCodeProcessor machine in sut.Machines)
                machine.Connect();

            Assert.IsTrue(sut.Machines[0].IsConnected);
            Assert.IsTrue(sut.Machines[1].IsConnected);

            sut.CloseProcessors();

            Assert.AreEqual(0, sut.Machines.Count);
        }

        [TestMethod]
        public void EventRaised_NotRecognized_DoesNothing()
        {
            ProcessorMediator sut = new ProcessorMediator(new Logger(),
                new MockMachineProvider(new string[] { "Machine 1", "Machine 2" }),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());

            sut.OpenProcessors();

            Assert.AreEqual(2, sut.Machines.Count);

            foreach (IGCodeProcessor machine in sut.Machines)
                machine.Connect();

            Assert.IsTrue(sut.Machines[0].IsConnected);
            Assert.IsTrue(sut.Machines[1].IsConnected);

            sut.CloseProcessors();

            Assert.AreEqual(0, sut.Machines.Count);

            sut.EventRaised("unknown", null, null);
            object eventResult = null;

            Assert.IsFalse(sut.EventRaised("unknown", null, null, ref eventResult));
        }

        [TestMethod]
        public void EventRaised_MachineRemove_RemovesExistingMachine()
        {
            ProcessorMediator sut = new ProcessorMediator(new Logger(),
                new MockMachineProvider(new string[] { "Machine 1", "Machine 2" }), 
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());

            sut.OpenProcessors();

            Assert.AreEqual(2, sut.Machines.Count);

            foreach (IGCodeProcessor machine in sut.Machines)
                machine.Connect();

            Assert.IsTrue(sut.Machines[0].IsConnected);
            Assert.IsTrue(sut.Machines[1].IsConnected);

            sut.EventRaised("MachineRemove", 1L, null);

            Assert.AreEqual(1, sut.Machines.Count);
        }

        [TestMethod]
        public void EventRaised_MachineUpdate_RemovesExistingMachineAndReAdds()
        {
            MockMachineProvider mockMachineProvider = new MockMachineProvider(new string[] { "Machine 1", "Machine 2" });
            ProcessorMediator sut = new ProcessorMediator(new Logger(),
                mockMachineProvider, new MockComPortFactory(), new MockNotification(), new MockSettingsProvider());

            sut.OpenProcessors();

            Assert.AreEqual(2, sut.Machines.Count);

            foreach (IGCodeProcessor machine in sut.Machines)
                machine.Connect();

            Assert.IsTrue(sut.Machines[0].IsConnected);
            Assert.IsTrue(sut.Machines[1].IsConnected);
            Assert.AreEqual("Machine 2", mockMachineProvider.MachineGet(1).Name);
            mockMachineProvider.MachineGet(1).Name = "Machine 2a";
            sut.EventRaised("MachineUpdate", 1L, null);

            Assert.AreEqual(2, sut.Machines.Count);
            Assert.AreEqual("Machine 2a", mockMachineProvider.MachineGet(1).Name);
        }
    }
}
