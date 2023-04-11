using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GSendAnalyser;
using GSendAnalyser.Internal;

using GSendCommon;
using GSendCommon.Overrides;

using GSendShared;
using GSendShared.Interfaces;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shared.Classes;

namespace GSendTests.OverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class SpindleStartStopTests
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
        public void SpindleActiveTime_SpindleStarts_M3_StartsTimer()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new();
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MockMachineProvider mockMachineProvider = new MockMachineProvider(new string[] { "Machine 1" });
            IMachine mockMachine = mockMachineProvider.MachineGet(0);
            MockComPort mockComport = new MockComPort(mockMachine);
            IComPortFactory comPortFactory = new MockComPortFactory(mockComport);

            IGCodeProcessor processor = new GCodeProcessor(mockMachineProvider, mockMachine, comPortFactory, new MockServiceProvider());
            IGCodeOverrideContext context = new GCodeOverrideContext(new MockServiceProvider(), new MockStaticMethods(), processor, mockMachine, mockComport);
            context.ProcessGCodeLine(gCodeLine);
            SpindleActiveTime sut = new SpindleActiveTime();
            sut.Process(context, CancellationToken.None);


            Assert.IsTrue(false, "Record in table not saved or even created yet! finish this");
        }

        [TestMethod]
        public void SpindleSoftStart_SpindleStarts_M3_SpindleSpeedIncreasesEvery200Ms()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new();
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MockMachineProvider mockMachineProvider = new MockMachineProvider(new string[] { "Machine 1" });
            IMachine mockMachine = mockMachineProvider.MachineGet(0);
            mockMachine.AddOptions(MachineOptions.SoftStart);
            mockMachine.SoftStartSeconds = 30;

            MockComPort mockComport = new MockComPort(mockMachine);
            IComPortFactory comPortFactory = new MockComPortFactory(mockComport);

            IGCodeProcessor processor = new GCodeProcessor(mockMachineProvider, mockMachine, comPortFactory, new MockServiceProvider());
            IGCodeOverrideContext context = new GCodeOverrideContext(new MockServiceProvider(), new MockStaticMethods(), processor, mockMachine, mockComport);
            SpindleSoftStart sut = new SpindleSoftStart();
            sut.Process(context, CancellationToken.None);

            Assert.IsFalse(context.SendCommand);


            Assert.AreEqual(150, mockComport.Commands.Count);
            Assert.AreEqual("S3000M3", mockComport.Commands[149]);
        }
    }
}
