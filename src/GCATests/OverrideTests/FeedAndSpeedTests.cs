using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSendAnalyser.Internal;
using GSendAnalyser;
using GSendCommon.OverrideClasses;
using GSendCommon;
using GSendShared.Abstractions;
using GSendShared.Models;
using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.OverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class FeedAndSpeedTests
    {
        [TestMethod]
        public void Process_OverridesAreDisabled_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_NoCommandWithFeedRate_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_ContiansXYZValues_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1X1Y1Z1F300");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_SetsOfficialXYFeedRate_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1X1F3000");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            _ = sut.Process(context, CancellationToken.None);

            Assert.AreEqual(3000, sut.OfficialXYFeedRate);
            Assert.AreEqual(3000, sut.CurrentXYFeedRate);
            Assert.IsTrue(sut.IsG1Command);
        }

        [TestMethod]
        public void Process_SetsOfficialZFeedRate_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1Z15F400");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            _ = sut.Process(context, CancellationToken.None);

            Assert.AreEqual(400, sut.OfficialZFeedRate);
            Assert.AreEqual(400, sut.CurrentZDownFeedRate);
            Assert.AreEqual(400, sut.CurrentZUpFeedRate);
            Assert.IsTrue(sut.IsG1Command);
        }

        [TestMethod]
        public void Process_OverridingX_False_OriginalValueNotReset_ResetsToOriginalValue_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            CreateGCodeCommands(gCodeLine, "G1X15F1000");

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;

            FeedAndSpeedOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.AreEqual(1000, sut.CurrentXYFeedRate);
            Assert.IsTrue(sut.IsG1Command);

            machineStateModel.Overrides.OverridesDisabled = false;
            machineStateModel.Overrides.OverrideXY = false;
            machineStateModel.Overrides.AxisXY.NewValue = 2000;

            CreateGCodeCommands(gCodeLine, "X34Y100");
            result = sut.Process(context, CancellationToken.None);
            Assert.IsFalse(result);
            Assert.IsTrue(context.SendCommand);
            Assert.AreEqual(0, context.CommandQueue.Count);
        }

        [TestMethod]
        public void Process_OverridingX_OriginalValueNotReset_ResetsToOriginalValue_Success()
        {
            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel);
            FeedAndSpeedOverride sut = new();

            // initial speed, no override
            SendAndValidateXYCommand(sut, context, "G1X15F1000", "", false, true, 0, 1000, true);

            // configure overrides
            machineStateModel.Overrides.OverridesDisabled = false;
            machineStateModel.Overrides.OverrideXY = true;
            machineStateModel.Overrides.AxisXY.NewValue = 2000;

            // send command with overrides
            SendAndValidateXYCommand(sut, context, "X34Y100", "X34Y100F2000", true, false, 1, 2000, true);

            //disable override
            machineStateModel.Overrides.OverrideXY = false;

            //send command, should use existing feed but still sent as previous was overridden
            SendAndValidateXYCommand(sut, context, "X590Y630", "X590Y630F1000", true, false, 1, 1000, true);

            // send command, will not be overridden at all
            SendAndValidateXYCommand(sut, context, "X20.123", "", false, true, 0, 1000, true);

            machineStateModel.Overrides.OverridesDisabled = false;
            machineStateModel.Overrides.OverrideXY = true;

            // send command with overrides
            SendAndValidateXYCommand(sut, context, "X34Y100", "X34Y100F2000", true, false, 1, 2000, true);

            // send command already overridden, should not include the F value
            SendAndValidateXYCommand(sut, context, "X34Y100", "X34Y100", false, true, 0, 2000, true);

            // change feed rate for x
            machineStateModel.Overrides.AxisXY.NewValue = 5000;

            SendAndValidateXYCommand(sut, context, "X1Y1", "X1Y1F5000", true, false, 1, 5000, true);

            SendAndValidateXYCommand(sut, context, "X100Y100", "X100Y100", false, true, 0, 5000, true);

            SendAndValidateXYCommand(sut, context, "G0X1Y1", "", false, true, 0, 5000, false);

            SendAndValidateXYCommand(sut, context, "G1X100Y100F1000", "G1X100Y100F5000", true, false, 1, 5000, true);


            // reset back to F2000
            machineStateModel.Overrides.AxisXY.NewValue = 2000;



            // disable all overrides
            machineStateModel.Overrides.OverridesDisabled = true;

            //send command, should use existing feed but still sent as previous was overridden
            SendAndValidateXYCommand(sut, context, "X590Y630", "X590Y630F1000", true, false, 1, 1000, true);

            // send command, will not be overridden at all
            SendAndValidateXYCommand(sut, context, "X20.123", "", false, true, 0, 1000, true);

            SendAndValidateXYCommand(sut, context, "G0X5Y5", "", false, true, 0, 1000, false);

            // overrides enabled, override xy is still true
            machineStateModel.Overrides.OverridesDisabled = false;
            Assert.IsTrue(machineStateModel.Overrides.OverrideXY);
            Assert.AreEqual(2000, machineStateModel.Overrides.AxisXY.NewValue);

            // send g1 after previous g0, with speed which is overridden
            SendAndValidateXYCommand(sut, context, "G1X50Y50F1000", "G1X50Y50F2000", true, false, 1, 2000, true);
        }

        private void SendAndValidateXYCommand(FeedAndSpeedOverride sut, MockOverrideContext context, 
            string gCode, string expectedGCodeResponse, bool expectedProcessResult, 
            bool expectedSendCommand, int expectedQueueCount, int expectedCurrentFeedRate, 
            bool expectedG1Command)
        {
            context.SendCommand = true;
            Assert.AreEqual(0, context.CommandQueue.Count);
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            CreateGCodeCommands(gCodeLine, gCode);
            context.GCode = gCodeLine;
            bool result = sut.Process(context, CancellationToken.None);
            Assert.AreEqual(expectedProcessResult, result);
            Assert.AreEqual(expectedSendCommand, context.SendCommand);
            Assert.AreEqual(expectedQueueCount, context.CommandQueue.Count);
            Assert.AreEqual(expectedCurrentFeedRate, sut.CurrentXYFeedRate);
            Assert.AreEqual(expectedG1Command, sut.IsG1Command);

            if (expectedQueueCount > 0)
            {
                if (context.CommandQueue.TryDequeue(out IGCodeLine command))
                {
                    string queuedGCode = command.GetGCode();
                    Assert.AreEqual(expectedGCodeResponse, queuedGCode);
                }
                else
                {
                    Assert.IsFalse(true, "Command missing, where has it gone!");
                }
            }
        }

        private void CreateGCodeCommands(IGCodeLine gCodeLine, string gCode)
        {
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            gCodeLine.Commands.Clear();
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);
            gCodeLine.Commands.AddRange(analyses.Commands);
        }
    }
}
