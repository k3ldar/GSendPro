using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GSendAnalyser;
using GSendAnalyser.Internal;

using GSendCommon;

using GSendService.Internal;

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
            new GCodeProcessor(null, new MockComPortFactory());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_ComPortFactoryNull_Throws_ArgumentNullException()
        {
            new GCodeProcessor(new MachineModel(), null);
        }

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            IMachine machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            Assert.IsNotNull(sut);
            Assert.AreSame(machineModel, mockComPortFactory.MockComPort.Machine);
            Assert.AreEqual(machineModel.Id, sut.Id);
        }

        [TestMethod]
        public void Connect_MultipleTimes_DoesNotThrowException_CreatesConnection()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool connectCalled = false;
            bool lockedCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnConnect += (sender, e) => { connectCalled = true; };
            sut.OnGrblError += (sender, e) => { lockedCalled = e.Equals(GrblError.Locked); };

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
            Assert.IsTrue(connectCalled);
        }

        [TestMethod]
        public void Disconnect_MultipleTimes_DoesNotThrowException_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool disconnectCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnDisconnect += (sender, e) => { disconnectCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.IsFalse(sut.IsConnected);
            Assert.IsFalse(sut.Start());
        }

        [TestMethod]
        public void Start_SetsRunningToTrue_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool startCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnStart += (sender, e) => { startCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(startCalled);
        }

        [TestMethod]
        public void Pause_IsNotRunning_Returns_False()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.IsFalse(sut.IsConnected);
            Assert.IsFalse(sut.Pause());
        }

        [TestMethod]
        public void Pause_PausesProcessingDoesNotDisconnect_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool pauseCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnPause += (sender, e) => { pauseCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
            Assert.IsTrue(sut.Start());
            Assert.IsFalse(sut.IsPaused);
            Assert.IsTrue(sut.Pause());
            Assert.IsTrue(sut.IsPaused);
            Assert.IsTrue(sut.IsRunning);
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(pauseCalled);
        }

        [TestMethod]
        public void Resume_IsNotPaused_Returns_False()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.IsFalse(sut.IsConnected);
            Assert.IsFalse(sut.Resume());
        }

        [TestMethod]
        public void Resume_ResumesProcessing_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool resumeCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnResume += (sender, e) => { resumeCalled = true; };

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
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
        public void Stop_IsNotRunning_Returns_False()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.IsFalse(sut.IsConnected);
            Assert.IsFalse(sut.Stop());
        }

        [TestMethod]
        public void Stop_WhenPausedStopsProcessing_Returns_True()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
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
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            bool stopCalled = false;
            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.OnStop += (sender, e) => { stopCalled = true; };
            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(sut.Connect());
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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
            GCodeParser parser = new();
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);


            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.AreEqual(0, sut.CommandCount);

            sut.LoadGCode(analyses);

            Assert.AreEqual(1, sut.CommandCount);

            sut.LoadGCode(new GCodeAnalyses());

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
            GCodeParser parser = new();
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);


            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.AreEqual(0, sut.CommandCount);

            sut.LoadGCode(analyses);

            Assert.AreEqual(3, sut.CommandCount);

            sut.Clear();

            Assert.AreEqual(0, sut.CommandCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateSpindleSpeed_InvalidParamMinusOne_Throws_ArgumentOutOfRangeException()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.UpdateSpindleSpeed(-1);
        }

        [TestMethod]
        public void UpdateSpindleSpeed_CommandSentToComPort_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.UpdateSpindleSpeed(8000);

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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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
            sut.UpdateSpindleSpeed(8000);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("S8000M3", mockComPortFactory.MockComPort.Commands[0]);

            sut.UpdateSpindleSpeed(0);
            Assert.AreEqual(2, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M5", mockComPortFactory.MockComPort.Commands[1]);

            Assert.IsTrue(spindleSpeedEventRaised);
            Assert.IsTrue(spindleSpeedOffEventRaised);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void UpdateSpindleSpeed_CommandSentToComPort_TimeoutWaitingForResponse_Throws_TimeoutException()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            mockComPortFactory.MockComPort.DelayResponse = TimeSpan.FromSeconds(1);
            sut.TimeOut = TimeSpan.MinValue;
            sut.UpdateSpindleSpeed(8000);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.TimeOut = TimeSpan.FromSeconds(5);
            string helpText = sut.Help();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("$", mockComPortFactory.MockComPort.Commands[0]);
            Assert.AreEqual("HLP:$$ $# $G $I $N $x=val $Nx=line $J=line $SLP $C $X $H ~ ! ? ctrl-x]\r\n", helpText);
        }

        [TestMethod]
        public void Unlock_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            sut.Unlock();

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.TimeOut = TimeSpan.FromSeconds(5);
            Dictionary<int, decimal> settings = sut.Settings();

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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxis(Axis.X);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G92X0", mockComPortFactory.MockComPort.Commands[0]);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxis(Axis.Y);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G92Y0", mockComPortFactory.MockComPort.Commands[0]);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxis(Axis.Z);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G92Z0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ZeroAxis_XY_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxis(Axis.X | Axis.Y);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G92X0Y0", mockComPortFactory.MockComPort.Commands[0]);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ZeroAxis_XYZ_CommandsSentToCom_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory();

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnCommandSent += (sender, e) => { eventFired = true; };
            sut.ZeroAxis(Axis.X | Axis.Y | Axis.Z);

            Assert.AreEqual(1, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("G92X0Y0Z0", mockComPortFactory.MockComPort.Commands[0]);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnInvalidComPort += (sender, e) => { eventFired = true; };

            bool connectResult = sut.Connect();
            Assert.IsFalse(connectResult);
            Assert.IsFalse(sut.IsConnected);

            Assert.IsFalse(sut.IsRunning);
            Assert.IsFalse(sut.IsConnected);
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void StatusReceived_MachineModelUpdated_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7"
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));
            mockComPortFactory.MockComPort.CommandsToReturn.Add("ok");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Idle|WCO:0,0,0|Ov:90,80,70|A:CF>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Run|WCO:23,39,43|>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Idle|MPos:17,63,58|FS:456,987|A:SFM>");
            mockComPortFactory.MockComPort.CommandsToReturn.Add("<Run|WPos:11,21,45|WCO:93,34,65|Bf:46,108|Ln:234|F:200|Ov:95,85,75>");

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            MachineStateModel machineStateModel = null;
            bool eventFired = false;
            sut.OnMachineStateChanged += (sender, e) => { eventFired = true; machineStateModel = e; };

            bool connectResult = sut.Connect();
            Assert.IsTrue(connectResult);
            Assert.IsTrue(sut.IsConnected);

            sut.WriteLine("?");
            Assert.IsNotNull(machineStateModel);
            Assert.AreEqual(MachineState.Run, machineStateModel.MachineState);
            Assert.AreEqual(-1, machineStateModel.SubState);
            Assert.AreEqual(23, machineStateModel.OffsetX);
            Assert.AreEqual(39, machineStateModel.OffsetY);
            Assert.AreEqual(43, machineStateModel.OffsetZ);
            Assert.AreEqual(90, machineStateModel.OverrideFeeds);
            Assert.AreEqual(80, machineStateModel.OverrideRapids);
            Assert.AreEqual(70, machineStateModel.OverrideSpindleSpeed);
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
            Assert.AreEqual(95, machineStateModel.OverrideFeeds);
            Assert.AreEqual(85, machineStateModel.OverrideRapids);
            Assert.AreEqual(75, machineStateModel.OverrideSpindleSpeed);


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
                {
                    { 120, 4000 },
                    { 121, 3000 },
                    { 122, 300 }
                },
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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
                {
                    { 120, 4000 },
                    { 121, 3000 },
                    { 122, 300 }
                },
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.JogStart(JogDirection.XPlusYPlus, 0.01, 3500);

            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91X0.01Y0.01F3500"));
        }

        [TestMethod]
        public void JogStart_Continuous_ZAxis_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
                {
                    { 120, 300 },
                    { 121, 300 },
                    { 122, 30 },
                    { 130, 200 },
                    { 131, 200 },
                    { 132, 80 }
                },
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
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
                {
                    { 120, 300 },
                    { 121, 300 },
                    { 122, 30 },
                    { 130, 200 },
                    { 131, 200 },
                    { 132, 80 }
                },
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.StateModel.MachineX = 123.168;
            sut.StateModel.MachineY = 73.855;

            sut.JogStart(JogDirection.XPlusYPlus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91X76.832Y126.145F2000"));

            sut.JogStart(JogDirection.XMinusYMinus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91X-76.832Y-126.145F2000"));
        }

        [TestMethod]
        public void JogStart_Continuous_XYAxis_Success()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM7",
                Settings = new()
                {
                    { 120, 300 },
                    { 121, 300 },
                    { 122, 30 },
                    { 130, 200 },
                    { 131, 200 },
                    { 132, 80 }
                },
            };

            MockComPortFactory mockComPortFactory = new MockComPortFactory(new MockComPort(machineModel));

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.StateModel.MachineX = 123.168;
            sut.StateModel.MachineY = 73.855;

            sut.JogStart(JogDirection.XMinusYPlus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91X-76.832Y126.145F2000"));

            sut.JogStart(JogDirection.XPlusYMinus, 0, 2000);
            Assert.IsTrue(mockComPortFactory.MockComPort.Commands.Contains("$J=G21G91X76.832Y-126.145F2000"));
        }
    }
}
