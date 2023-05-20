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
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_NoCommandWithFeedRate_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_ContiansXYZValues_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1X1Y1Z1F300");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_SetsOfficialXYFeedRate_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1X1F3000");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            _ = sut.Process(context, CancellationToken.None);

            Assert.AreEqual(3000, sut.OfficialXYFeedRate);
            Assert.AreEqual(3000, sut.CurrentXYFeedRate);
            Assert.IsTrue(sut.IsG1Command);
        }

        [TestMethod]
        public void Process_SetsOfficialZFeedRate_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse("G1Z15F400");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            _ = sut.Process(context, CancellationToken.None);

            Assert.AreEqual(400, sut.OfficialZFeedRate);
            Assert.AreEqual(400, sut.CurrentZDownFeedRate);
            Assert.AreEqual(400, sut.CurrentZUpFeedRate);
            Assert.IsTrue(sut.IsG1Command);
        }

        [TestMethod]
        public void Process_OverridingX_OriginalValueNotReset_ResetsToOriginalValue_Success()
        {
            IGCodeLine gCodeLine = new GCodeLine();
            CreateGCodeCommands(gCodeLine, "G1X15F1000");

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            IGCodeOverrideContext context = new MockOverrideContext(machineStateModel, gCodeLine);

            FeedAndSpeedOverride sut = new FeedAndSpeedOverride();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.AreEqual(1000, sut.CurrentXYFeedRate);
            Assert.IsTrue(sut.IsG1Command);

            machineStateModel.Overrides.OverridesDisabled = false;
            machineStateModel.Overrides.OverrideXY = true;
            machineStateModel.Overrides.AxisXY.NewValue = 2000;

            CreateGCodeCommands(gCodeLine, "X34Y100");
            result = sut.Process(context, CancellationToken.None);
            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(1, context.CommandQueue.Count);

            if (context.CommandQueue.TryPeek(out IGCodeLine command))
            {
                string gCode = command.GetGCode();
                Assert.AreEqual("X34Y100F2000", gCode);
            }
            else
            {
                Assert.IsFalse(true, "Command missing, where has it gone!");
            }
        }

        private void CreateGCodeCommands(IGCodeLine gCodeLine, string gCode)
        {
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            gCodeLine.Commands.Clear();
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);
            gCodeLine.Commands.AddRange(analyses.Commands);
        }
    }
}
