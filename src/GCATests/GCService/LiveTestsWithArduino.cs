using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

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
    public class LiveTestsWithArduino
    {
        [Ignore("To be run occasionally for dev purposes")]
        [TestMethod]
        public void ConnectToGrblAfterReset()
        {
            MachineModel machineModel = new()
            {
                ComPort = "COM4"
            };

            ComPortFactory mockComPortFactory = new(new MockSettingsProvider());

            GCodeProcessor sut = new(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.Connect();
            Assert.IsTrue(sut.IsConnected);

            sut.Disconnect();
            Assert.IsFalse(sut.IsConnected);
        }

        [Ignore("To be run occasionally for dev purposes")]
        [TestMethod]
        public void HomeAndPause()
        {
            ThreadManager.Initialise();
            MachineModel machineModel = new()
            {
                ComPort = "COM4"
            };

            ComPortFactory mockComPortFactory = new(new MockSettingsProvider());


            GCodeProcessor sut = new(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

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

            sut.WriteLine("$H");
            Thread.Sleep(500);
            sut.WriteLine("~");

            if (unlocked)
            {
                // do nothing
            }

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
            }
            sut.Disconnect();
            Assert.IsFalse(sut.IsConnected);
        }

        [Ignore("To be run occasionally for dev purposes")]
        [TestMethod]
        public void ConnectToGrbl_InvalidPort_AfterReset()
        {
            MachineModel machineModel = new()
            {
                ComPort = "COM12"
            };

            ComPortFactory mockComPortFactory = new(new MockSettingsProvider());

            GCodeProcessor sut = new(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());
            bool eventFired = false;
            sut.OnInvalidComPort += (sender, e) => { eventFired = true; };
            sut.TimeOut = TimeSpan.FromSeconds(5);
            sut.Connect();
            Assert.IsFalse(sut.IsConnected);

            Assert.IsTrue(eventFired);
        }

        [Ignore("To be run occasionally for dev purposes")]
        [TestMethod]
        public void ConnectToGrblSendTwoCommands()
        {
            ThreadManager.Initialise();
            const string gCode = "G17G21\nG0Z20.000\nG0X0.000Y0.000S8000M3\nG1 Z40.000 F500.0\nX139.948 F500.0\nY37.136\nG0X0Y0Z0\nM30";
            GCodeParser parser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = parser.Parse(gCode);

            Assert.AreEqual(1, analyses.Commands[0].LineNumber);
            Assert.AreEqual(1, analyses.Commands[1].LineNumber);
            Assert.AreEqual(2, analyses.Commands[2].LineNumber);
            Assert.AreEqual(2, analyses.Commands[3].LineNumber);
            Assert.AreEqual(3, analyses.Commands[4].LineNumber);
            Assert.AreEqual(3, analyses.Commands[5].LineNumber);
            Assert.AreEqual(3, analyses.Commands[6].LineNumber);
            Assert.AreEqual(3, analyses.Commands[7].LineNumber);
            Assert.AreEqual(3, analyses.Commands[8].LineNumber);
            Assert.AreEqual(4, analyses.Commands[9].LineNumber);
            Assert.AreEqual(4, analyses.Commands[10].LineNumber);
            Assert.AreEqual(4, analyses.Commands[11].LineNumber);
            Assert.AreEqual(5, analyses.Commands[12].LineNumber);
            Assert.AreEqual(5, analyses.Commands[13].LineNumber);
            Assert.AreEqual(6, analyses.Commands[14].LineNumber);

            MachineModel machineModel = new()
            {
                ComPort = "COM4"
            };

            ComPortFactory mockComPortFactory = new(new MockSettingsProvider());

            GCodeProcessor sut = new(new MockGSendDataProvider(), machineModel, mockComPortFactory, new MockServiceProvider());

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
            sut.WriteLine("?");
            sut.Unlock();
            Assert.IsTrue(sut.IsConnected);
            Assert.IsTrue(unlocked);

            sut.LoadGCode(analyses);

            sut.Start(new ToolProfileModel());

            Thread.Sleep(500);
            sut.WriteLine("!");
            Thread.Sleep(2500);
            sut.WriteLine("~");

            int runcount = 0;
            while (sut.IsRunning)
            {
                Thread.Sleep(10);
                runcount++;

                if (runcount == 20)
                    sut.Stop();
            }

            for (int i = 0; i < 10; i++)
            {
                sut.WriteLine("?");

                Thread.Sleep(1000);
            }
            Assert.IsFalse(sut.IsRunning);

            sut.Disconnect();
            Assert.IsFalse(sut.IsConnected);
        }
    }
}
