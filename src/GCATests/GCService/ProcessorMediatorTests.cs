﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer.Internal;

using GSendCommon;

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
            new ProcessorMediator(new MockServiceProvider(), null, new MockGSendDataProvider(), new MockComPortFactory(),
                new MockNotification(), new MockSettingsProvider(), new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_GSendDataProvider_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new MockServiceProvider(), new Logger(), null, new MockComPortFactory(),
                new MockNotification(), new MockSettingsProvider(), new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_NotificationService_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new MockServiceProvider(), new Logger(), new MockGSendDataProvider(),
                new MockComPortFactory(), null, new MockSettingsProvider(), new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ComPortFactory_Throws_ArgumentNullException()
        {
            new ProcessorMediator(new MockServiceProvider(), new Logger(), new MockGSendDataProvider(),
                null, new MockNotification(), new MockSettingsProvider(), new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));
        }

        [TestMethod]
        public void GetEvents_ReturnsAllRegisteredEventIds()
        {
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                new MockGSendDataProvider(), new MockComPortFactory(), new MockNotification(),
                new MockSettingsProvider(), new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));
            Assert.IsNotNull(sut);

            List<string> events = sut.GetEvents();
            Assert.AreEqual(4, events.Count);
            Assert.AreEqual("MachineAdd", events[0]);
            Assert.AreEqual("MachineRemove", events[1]);
            Assert.AreEqual("MachineUpdate", events[2]);
            Assert.AreEqual("MachineConnected", events[3]);
        }

        [TestMethod]
        [Ignore("not yet finished")]
        public void ExecuteAsync_StartsAllMachines_Success()
        {
            throw new NotImplementedException("needs work");
            //ProcessorMediator sut = new ProcessorMediator(new MockServiceProvider(), new Logger(), new gSendDataProvider(), new MockNotification());
            //CancellationTokenSource cancellationToken = new CancellationTokenSource();

            //Task runResult = Task.Run(() => sut.InternalExecute(cancellationToken.Token));

            //while (runResult.Status == TaskStatus.WaitingForActivation)
            //    Thread.Sleep(0);

            //cancellationToken.Cancel();
        }

        [TestMethod]
        public void OpenProcessors_OpensAndInitializesAllProcessors_Success()
        {
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                new MockGSendDataProvider(["Machine 1", "Machine 2"]),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider(),
                new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));

            sut.OpenProcessors();

            foreach (IGCodeProcessor machine in sut.Machines)
            {
                Debug.WriteLine($"MachineId: {machine.Id}");
            }

            Assert.AreEqual(2, sut.Machines.Count);
        }

        [TestMethod]
        public void CloseProcessors_ClosesAllProcessors_Success()
        {
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                new MockGSendDataProvider(["Machine 1", "Machine 2"]),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider(),
                new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));

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
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                new MockGSendDataProvider(["Machine 1", "Machine 2"]),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider(),
                new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));

            sut.OpenProcessors();

            foreach (IGCodeProcessor machine in sut.Machines)
            {
                Debug.WriteLine($"MachineId: {machine.Id}");
            }

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
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                new MockGSendDataProvider(["Machine 1", "Machine 2"]),
                new MockComPortFactory(), new MockNotification(), new MockSettingsProvider(),
                new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));

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
            MockGSendDataProvider gSendDataProvider = new(["Machine 1", "Machine 2"]);
            ProcessorMediator sut = new(new MockServiceProvider(), new Logger(),
                gSendDataProvider, new MockComPortFactory(), new MockNotification(), new MockSettingsProvider(),
                new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms()));

            sut.OpenProcessors();

            foreach (IGCodeProcessor machine in sut.Machines)
            {
                Debug.WriteLine($"MachineId: {machine.Id}");
            }

            Assert.AreEqual(2, sut.Machines.Count);

            foreach (IGCodeProcessor machine in sut.Machines)
                machine.Connect();

            Assert.IsTrue(sut.Machines[0].IsConnected);
            Assert.IsTrue(sut.Machines[1].IsConnected);
            Assert.AreEqual("Machine 2", gSendDataProvider.MachineGet(1).Name);
            gSendDataProvider.MachineGet(1).Name = "Machine 2a";
            sut.EventRaised("MachineUpdate", 1L, null);

            Assert.AreEqual(2, sut.Machines.Count);
            Assert.AreEqual("Machine 2a", gSendDataProvider.MachineGet(1).Name);
        }
    }
}
