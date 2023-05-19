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
using GSendCommon.OverrideClasses;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

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
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MockGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "Machine 1" });
            IMachine mockMachine = gSendDataProvider.MachineGet(0);
            MockComPort mockComport = new MockComPort(mockMachine);
            IComPortFactory comPortFactory = new MockComPortFactory(mockComport);

            IGCodeProcessor processor = new GCodeProcessor(gSendDataProvider, mockMachine, comPortFactory, new MockServiceProvider());
            IGCodeOverrideContext context = new GCodeOverrideContext(new MockServiceProvider(), new MockStaticMethods(), processor, mockMachine, new MachineStateModel());
            context.ProcessGCodeOverrides(gCodeLine);
            using (SpindleActiveTime sut = new SpindleActiveTime(gSendDataProvider))
                sut.Process(context, CancellationToken.None);


            Assert.IsTrue(gSendDataProvider.SpindleTimeCreateCalled);
            Assert.IsTrue(gSendDataProvider.SpindleTimeFinishCalled);
        }

        [TestMethod]
        public void SpindleSoftStart_SpindleStarts_M3_SpindleSpeedIncreasesEvery200Ms()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MockGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "Machine 1" });
            IMachine mockMachine = gSendDataProvider.MachineGet(0);
            mockMachine.AddOptions(MachineOptions.SoftStart);
            mockMachine.SoftStartSeconds = 30;

            MockComPort mockComport = new MockComPort(mockMachine);
            IComPortFactory comPortFactory = new MockComPortFactory(mockComport);

            IGCodeProcessor processor = new GCodeProcessor(gSendDataProvider, mockMachine, comPortFactory, new MockServiceProvider());
            IGCodeOverrideContext context = new GCodeOverrideContext(new MockServiceProvider(), new MockStaticMethods(), processor, mockMachine, new MachineStateModel());

            Assert.AreEqual(0, processor.StateModel.CommandQueueSize);

            context.ProcessGCodeOverrides(analyses.Lines(out int lineCount)[0]);

            Assert.AreEqual(1, lineCount);
            Assert.IsTrue(processor.StateModel.CommandQueueSize > 10 || mockComport.Commands.Count > 0, "Queue processed in another thread, has it happened there?");

            SpindleSoftStart sut = new SpindleSoftStart();
            sut.Process(context, CancellationToken.None);

            Assert.IsFalse(context.SendCommand);
        }
    }
}
