using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendService.Internal;
using GSendShared.Models;
using GSendTests.Mocks;

using GSendShared;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSendAnalyser;
using GSendAnalyser.Internal;
using GSendAnalyser.Abstractions;
using Shared.Classes;
using System.Threading;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LiveTestsWithArduino
    {
        [TestMethod]
        public void ConnectToGrblAfterReset()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM4"
            };

            ComPortFactory mockComPortFactory = new ComPortFactory(new MockSettingsProvider());

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.Connect();
            Assert.IsTrue(sut.IsConnected);

            sut.Disconnect();
            Assert.IsFalse(sut.IsConnected);
        }

        [TestMethod]
        public void ConnectToGrbl_InvalidPort_AfterReset()
        {
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM12"
            };

            ComPortFactory mockComPortFactory = new ComPortFactory(new MockSettingsProvider());

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);
            bool eventFired = false;
            sut.OnInvalidComPort += (sender, e) => { eventFired = true; };
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.Connect();
            Assert.IsFalse(sut.IsConnected);

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void ConnectToGrblSendTwoCommands()
        {
            ThreadManager.Initialise();
            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG1 Z40.000 F10.0 X139.948 F20.0 Y37.136";
            GCodeParser parser = new();
            IGCodeAnalyses analyses = parser.Parse(ZProbeCommand);
            MachineModel machineModel = new MachineModel()
            {
                ComPort = "COM4"
            };

            ComPortFactory mockComPortFactory = new ComPortFactory(new MockSettingsProvider());

            GCodeProcessor sut = new GCodeProcessor(machineModel, mockComPortFactory);

            sut.TimeOut = TimeSpan.FromSeconds(1000);

            ThreadManager.ThreadStart(sut, "COM4", System.Threading.ThreadPriority.Normal);
            sut.OnGrblError += (sender, e) => 
            {
                if (e.Equals(GrblError.Locked))
                    sut.Unlock();
            };

            bool unlocked = false;
            sut.OnCommandSent += (sender, e) =>
            {
                switch (e)
                {
                    case CommandSent.Unlock:
                        unlocked = true; 
                        break;

                    default:
                        throw new NotImplementedException();
                }
            };

            sut.Connect();
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(unlocked);

            sut.LoadGCode(analyses.Commands);

            sut.Start();

            while (sut.IsRunning)
            {
                Thread.Sleep(0);
            }

            Assert.IsFalse(sut.IsRunning);

            sut.Disconnect();
            Assert.IsFalse(sut.IsConnected);
        }
    }
}
