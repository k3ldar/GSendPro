using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendAnalyser.Abstractions;
using GSendAnalyser.Internal;

using GSendService.Internal;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GCodeProcessorTests
    {
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
            Assert.IsTrue(lockedCalled);
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

            sut.LoadGCode(analyses.Commands);

            Assert.AreEqual(13, sut.CommandCount);

            sut.LoadGCode(new List<IGCodeCommand>());

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

            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG0X139.948Y37.136Z40.000";
            GCodeParser parser = new();
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);


            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            Assert.AreEqual(0, sut.CommandCount);

            sut.LoadGCode(analyses.Commands);

            Assert.AreEqual(13, sut.CommandCount);

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
            Assert.IsTrue(sut.MistCoolantActive);

            sut.TurnFloodCoolantOn();

            Assert.AreEqual(2, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M8", mockComPortFactory.MockComPort.Commands[1]);
            Assert.IsTrue(sut.FloodCoolantActive);

            sut.CoolantOff();

            Assert.AreEqual(3, mockComPortFactory.MockComPort.Commands.Count);
            Assert.AreEqual("M9", mockComPortFactory.MockComPort.Commands[2]);
            Assert.IsFalse(sut.MistCoolantActive);
            Assert.IsFalse(sut.FloodCoolantActive);

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
    }
}
