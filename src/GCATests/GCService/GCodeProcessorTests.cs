using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using GSendAnalyser;
using GSendAnalyser.Internal;

using GSendCommon;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shared.Classes;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GCodeProcessorTests
    {
        [TestInitialize]
        public void Setup()
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
        public void Construct_InvalidParam_MachineNull_Throws_ArgumentNullException()
        {
            new GCodeProcessor(new MockGSendDataProvider(), null, new MockComPortFactory(), new MockServiceProvider());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ComPortFactoryNull_Throws_ArgumentNullException()
        {
            new GCodeProcessor(new MockGSendDataProvider(), new MachineModel(), null, new MockServiceProvider());
        }

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            IMachine machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            Assert.IsNotNull(sut);
            Assert.AreSame(machineModel, mockComPortFactory.MockComPort.Machine);
            Assert.AreEqual(machineModel.Id, sut.Id);
        }

        [TestMethod]
        public void Connect_MultipleTimes_DoesNotThrowException_CreatesConnection()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool connectCalled = false;
            bool lockedCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.OnConnect += (sender, e) => { connectCalled = true; };
            sut.OnGrblError += (sender, e) => { lockedCalled = e.Equals(GrblError.Locked); };

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.IsConnected);
            Assert.AreEqual(ConnectResult.AlreadyConnected, sut.Connect());
            Assert.IsTrue(connectCalled);
        }

        [TestMethod]
        public void Disconnect_MultipleTimes_DoesNotThrowException_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool disconnectCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.OnDisconnect += (sender, e) => { disconnectCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.IsConnected);

            Assert.IsTrue(sut.Disconnect());
            Assert.IsTrue(sut.Disconnect());
            Assert.IsTrue(disconnectCalled);
        }

        [TestMethod]
        public void Start_NotConnected_Returns_False()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.IsFalse(sut.IsConnected);
            Assert.IsFalse(sut.Start());
        }

        [TestMethod]
        public void Start_SetsRunningToTrue_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool startCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.OnStart += (sender, e) => { startCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(startCalled);
        }

        [TestMethod]
        public void Pause_IsNotRunning_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Pause());
        }

        [TestMethod]
        public void Pause_PausesProcessingDoesNotDisconnect_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool pauseCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.OnPause += (sender, e) => { pauseCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.Pause());
            Assert.IsTrue(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(pauseCalled);
        }

        [TestMethod]
        public void Resume_IsNotPaused_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Resume());
        }

        [TestMethod]
        public void Resume_ResumesProcessing_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool resumeCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(1);
            sut.OnResume += (sender, e) => { resumeCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.Pause());
            Assert.IsTrue(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(sut.Resume());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(resumeCalled);
        }

        [TestMethod]
        public void Stop_IsNotRunning_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Stop());
        }

        [TestMethod]
        public void Stop_WhenPausedStopsProcessing_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);

            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.Pause());
            Assert.IsTrue(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(sut.Stop());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsFalse(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
        }

        [TestMethod]
        public void Stop_StopsProcessing_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new(),
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool stopCalled = false;
            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.OnStop += (sender, e) => { stopCalled = true; };
            Assert.IsFalse(sut.IsConnected);
            Assert.AreEqual(ConnectResult.Success, sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(sut.Stop());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsFalse(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(stopCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoadGCode_NullCommandsParam_Throws_InvalidOperationException()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.LoadGCode(null);
        }

        [TestMethod]
        public void LoadGCode_ClearsExistingCommands_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG0X139.948Y37.136Z40.000";
            GCodeParser parser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);


            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.AreEqual(0, sut.CommandCount);

            sut.LoadGCode(analyses);

            Assert.AreEqual(1, sut.CommandCount);

            sut.LoadGCode(new GCodeAnalyses(new MockPluginClassesService()));

            Assert.AreEqual(0, sut.CommandCount);
        }

        [TestMethod]
        public void Clear_ClearsExistingCommands_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000\nS8000M3\nG0X139.948Y37.136Z40.000";
            GCodeParser parser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);


            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            Assert.AreEqual(0, sut.CommandCount);

            sut.LoadGCode(analyses);

            Assert.AreEqual(3, sut.CommandCount);

            sut.Clear();

            Assert.AreEqual(0, sut.CommandCount);
        }

        [TestMethod]
        public void UpdateSpindleSpeed_InvalidParamMinusOne_ReturnsFalse()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool result = sut.UpdateSpindleSpeed(-1, true);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateSpindleSpeed_CommandSentToComPort_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.UpdateSpindleSpeed(8000, true);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("S8000M3"));
        }

        [TestMethod]
        public void UpdateSpindleSpeed_SpeedZeroIssuesStopSpindleCommand_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();
            bool spindleSpeedEventRaised = false;
            bool spindleSpeedOffEventRaised = false;

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.OnCommandSent += (sender, e) =>
            {
                switch (e)
                {
                    case CommandSent.SpindleSpeedSet:
                        spindleSpeedEventRaised = true;
                        break;

                    case CommandSent.SpindleOff:
                        spindleSpeedOffEventRaised = true;
                        break;
                }
            };
            sut.UpdateSpindleSpeed(8000, true);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("S8000M3", mockComPortFactory.MockComPort.Commands[0]);

            sut.UpdateSpindleSpeed(0, true);
            Assert.AreEqual(2, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M5", mockComPortFactory.MockComPort.Commands[1]);

            Assert.IsTrue(spindleSpeedEventRaised);
            Assert.IsTrue(spindleSpeedOffEventRaised);
        }

        [Ignore("Can't work now M600 is there as that adds the delay before hitting the com port")]
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void UpdateSpindleSpeed_CommandSentToComPort_TimeoutWaitingForResponse_Throws_TimeoutException()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            mockComPortFactory.MockComPort.DelayResponse = TimeSpan.FromMilliseconds(1);
            sut.TimeOut = TimeSpan.MinValue;
            sut.UpdateSpindleSpeed(8000, true);
        }

        [TestMethod]
        public void Coolant_TurnOnAndOff_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();
            bool mistOnEventRaised = false;
            bool floodOnEventRaised = false;
            bool coolantOffEventRaised = false;

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.OnCommandSent += (sender, e) =>
            {
                switch (e)
                {
                    case CommandSent.CoolantOff:
                        coolantOffEventRaised = true;
                        break;
                    case CommandSent.MistOn:
                        mistOnEventRaised = true;
                        break;
                    case CommandSent.FloodOn:
                        floodOnEventRaised = true;
                        break;
                }
            };

            sut.TurnMistCoolantOn();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M7", mockComPortFactory.MockComPort.Commands[0]);

            sut.TurnFloodCoolantOn();

            Assert.AreEqual(2, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M8", mockComPortFactory.MockComPort.Commands[1]);

            sut.CoolantOff();

            Assert.AreEqual(3, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M9", mockComPortFactory.MockComPort.Commands[2]);

            Assert.IsTrue(mistOnEventRaised);
            Assert.IsTrue(floodOnEventRaised);
            Assert.IsTrue(coolantOffEventRaised);
        }

        [TestMethod]
        public void Home_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            sut.Home();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("$H", mockComPortFactory.MockComPort.Commands[0]);
        }

        [TestMethod]
        public void Help_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            string helpText = sut.Help();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("$", mockComPortFactory.MockComPort.Commands[0]);
            Assert.AreEqual("HLP:$$ $# $G $I $N $x=val $Nx=line $J=line $SLP $C $X $H ~ ! ? ctrl-x]\r\n", helpText);
        }

        [TestMethod]
        public void Unlock_MachineNotLocked_ReturnsFalse()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

            bool result = sut.Unlock();

            Assert.IsFalse(result);
            Assert.AreEqual(0, mockComPortFactory.MockComPort.Commands.Count);
        }

        [TestMethod]
        [Ignore("unstable")]
        public void Unlock_MachineLocked_ReturnsTrueAndSendsCommandToCom()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new(),
            };

            MockComPort mockComPort = new(machineModel);
            MockComPortFactory mockComPortFactory = new MockComPortFactory(mockComPort);
            mockComPort.CommandsToReturn.Add("<Alarm|>");
            mockComPort.CommandsToReturn.Add("<Alarm|>");
            mockComPort.CommandsToReturn.Add("ok");

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.Connect();
            Thread.Sleep(500);
            bool result = sut.Unlock();

            Assert.IsTrue(result);
            Assert.AreEqual(0, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("$X", mockComPortFactory.MockComPort.Commands[0]);

        }

        [TestMethod]
        public void Settings_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            Dictionary<int, object> settings = sut.Settings();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("$$", mockComPortFactory.MockComPort.Commands[0]);
            Assert.AreEqual(33, settings.Count);
            Assert.AreEqual(0.01m, settings[11]);
            Assert.AreEqual(250.000m, settings[100]);
        }

        [TestMethod]
        public void ZeroAxis_X_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxes(ZeroAxis.X, 0);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G10 P0 L20 X0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ZeroAxis_Y_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxes(ZeroAxis.Y, 0);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G10 P0 L20 Y0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ZeroAxis_Z_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxes(ZeroAxis.Z, 0);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G10 P0 L20 Z0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ZeroAxis_All_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxes(ZeroAxis.All, 0);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G10 P0 L20 X0 Y0 Z0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void SerialPortPinChanged_RaisesEventAndStopsProcessing_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnSerialPinChanged += (sender, e) => { eventFired = true; };

            sut.Connect();
            Assert.IsTrue(sut.IsConnected);

            sut.Start();
            Assert.IsTrue(sut.IsRunning);

            mockComPortFactory.MockComPort.RaisePinError();

            Assert.IsFalse(sut.IsRunning);
            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void SerialPortErrorReceived_RaisesEventAndStopsProcessing_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnSerialError += (sender, e) => { eventFired = true; };

            sut.Connect();
            Assert.IsTrue(sut.IsConnected);

            sut.Start();
            Assert.IsTrue(sut.IsRunning);

            mockComPortFactory.MockComPort.RaiseSerialError();

            Assert.IsFalse(sut.IsRunning);
            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void SerialPortNotFoundErrorReceived_RaisesEventAndStopsProcessing_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));
            mockComPortFactory.MockComPort.ThrowFileNotFoundException = true;

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnInvalidComPort += (sender, e) => { eventFired = true; };

            ConnectResult connectResult = sut.Connect();
            Assert.AreEqual(ConnectResult.Error, connectResult);
            Assert.IsFalse(sut.IsConnected);

            Assert.IsFalse(sut.IsRunning);
            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(eventFired);
        }

        [Ignore("unstable")]
        [TestMethod]
        public void StatusReceived_MachineModelUpdated_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));
            mockComPortFactory.MockComPort.CommandsToReturn.Add("ok");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Idle|WCO:0,0,0|Ov:90,80,70|A:CF>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Run|WCO:23,39,43|>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Idle|MPos:17,63,58|FS:456,987|A:SFM>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Run|WPos:11,21,45|WCO:93,34,65|Bf:46,108|Ln:234|F:200|Ov:95,85,75>");

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            MachineStateModel machineStateModel = null;
            bool eventFired = false;
            sut.OnMachineStateChanged += (sender, e) => { eventFired = true; machineStateModel = e; };

            ConnectResult connectResult = sut.Connect();
            Assert.AreEqual(ConnectResult.Success, connectResult);
            Assert.IsTrue(sut.IsConnected);

            sut.WriteLine("?");
            Assert.IsNotNull(machineStateModel);
            Assert.AreEqual(MachineState.Run, machineStateModel.MachineState);
            Assert.AreEqual(-1, machineStateModel.SubState);
            Assert.AreEqual(23, machineStateModel.OffsetX);
            Assert.AreEqual(39, machineStateModel.OffsetY);
            Assert.AreEqual(43, machineStateModel.OffsetZ);
            Assert.AreEqual(90, machineStateModel.MachineOverrideFeeds);
            Assert.AreEqual(80, machineStateModel.MachineOverrideRapids);
            Assert.AreEqual(70, machineStateModel.MachineOverrideSpindle);
            Assert.IsFalse(machineStateModel.SpindleClockWise);
            Assert.IsTrue(machineStateModel.SpindleCounterClockWise);
            Assert.IsTrue(machineStateModel.FloodEnabled);
            Assert.IsFalse(machineStateModel.MistEnabled);

            sut.WriteLine("?");
            Assert.IsNotNull(machineStateModel);
            Assert.AreEqual(MachineState.Idle, machineStateModel.MachineState);
            Assert.AreEqual(-1, machineStateModel.SubState);
            Assert.AreEqual(17, machineStateModel.MachineX);
            Assert.AreEqual(63, machineStateModel.MachineY);
            Assert.AreEqual(58, machineStateModel.MachineZ);
            Assert.AreEqual(456, machineStateModel.FeedRate);
            Assert.AreEqual(987, machineStateModel.SpindleSpeed);
            Assert.IsTrue(machineStateModel.SpindleClockWise);
            Assert.IsFalse(machineStateModel.SpindleCounterClockWise);
            Assert.IsTrue(machineStateModel.FloodEnabled);
            Assert.IsTrue(machineStateModel.MistEnabled);

            sut.WriteLine("?");
            Assert.IsNotNull(machineStateModel);
            Assert.AreEqual(MachineState.Run, machineStateModel.MachineState);
            Assert.AreEqual(-1, machineStateModel.SubState);
            Assert.AreEqual(11, machineStateModel.WorkX);
            Assert.AreEqual(21, machineStateModel.WorkY);
            Assert.AreEqual(45, machineStateModel.WorkZ);
            Assert.AreEqual(93, machineStateModel.OffsetX);
            Assert.AreEqual(34, machineStateModel.OffsetY);
            Assert.AreEqual(65, machineStateModel.OffsetZ);
            Assert.AreEqual(46, machineStateModel.BufferAvailableBlocks);
            Assert.AreEqual(108, machineStateModel.AvailableRXbytes);
            Assert.AreEqual(234, machineStateModel.LineNumber);
            Assert.AreEqual(200, machineStateModel.FeedRate);
            Assert.AreEqual(95, machineStateModel.MachineOverrideFeeds);
            Assert.AreEqual(85, machineStateModel.MachineOverrideRapids);
            Assert.AreEqual(75, machineStateModel.MachineOverrideSpindle);


            Assert.IsFalse(sut.IsRunning);

            sut.Disconnect();

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(eventFired);

        }

        [TestMethod]
        public void JogStop_SendsCorrectCommand_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            machineModel.Settings.MaxAccelerationY = 4000;
            machineModel.Settings.MaxAccelerationX = 3000;
            machineModel.Settings.MaxAccelerationZ = 300;

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.JogStop();

            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("\u0085"));
        }

        [TestMethod]
        public void JogStart_SpecifiedStepSize_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            machineModel.Settings.MaxAccelerationX = 4000;
            machineModel.Settings.MaxAccelerationY = 3000;
            machineModel.Settings.MaxAccelerationZ = 300;

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.JogStart(JogDirection.XPlusYPlus, 0.01, 3500);

            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G20G91X0.0100Y0.0100F3500"));
        }

        [TestMethod]
        public void JogStart_Continuous_ZAxis_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            machineModel.Settings.MaxAccelerationX = 300;
            machineModel.Settings.MaxAccelerationY = 300;
            machineModel.Settings.MaxAccelerationZ = 30;
            machineModel.Settings.MaxTravelX = 200;
            machineModel.Settings.MaxTravelY = 200;
            machineModel.Settings.MaxTravelZ = 80;

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.StateModel.MachineZ = 23.85;

            sut.JogStart(JogDirection.ZPlus, 0, 2000);
            mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91Z56.150F2000");

            sut.JogStart(JogDirection.ZMinus, 0, 2000);
            mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91Z-56.150F2000");
        }

        [TestMethod]
        public void JogStart_Continuous_XMinusYMinus_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            machineModel.Settings.MaxAccelerationX = 300;
            machineModel.Settings.MaxAccelerationY = 300;
            machineModel.Settings.MaxAccelerationZ = 30;
            machineModel.Settings.MaxTravelX = 200;
            machineModel.Settings.MaxTravelY = 200;
            machineModel.Settings.MaxTravelZ = 80;

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.StateModel.MachineX = 123.168;
            sut.StateModel.MachineY = 73.855;

            sut.JogStart(JogDirection.XPlusYPlus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G20G91X200.0000Y200.0000F2000"));

            sut.JogStart(JogDirection.XMinusYMinus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G20G91X-200.0000Y-200.0000F2000"));
        }

        [TestMethod]
        public void JogStart_Continuous_XYAxis_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
            };

            machineModel.Settings.MaxAccelerationX = 300;
            machineModel.Settings.MaxAccelerationY = 300;
            machineModel.Settings.MaxAccelerationZ = 30;
            machineModel.Settings.MaxTravelX = 200;
            machineModel.Settings.MaxTravelY = 200;
            machineModel.Settings.MaxTravelZ = 80;

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.StateModel.MachineX = 123.168;
            sut.StateModel.MachineY = 73.855;

            sut.JogStart(JogDirection.XMinusYPlus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G20G91X-200.0000Y200.0000F2000"));

            sut.JogStart(JogDirection.XPlusYMinus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G20G91X200.0000Y-200.0000F2000"));
        }
    }
}
