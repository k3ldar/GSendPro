using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using GSendAnalyser;
using GSendAnalyser.Internal;

using GSendCommon.MCodeOverrides;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M601Tests
    {
        [TestMethod]
        public void Process_M601CodeNotFound_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M601Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M601CodeFound_NoPCode_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M601");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M601Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M601AndPCodeCodeFound_PCodeValueLessThanMinimum_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M601P99");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M601Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M601AndPCodeCodeFound_PCodeValueGreaterThanMaximum_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M601P10001");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M601Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(context.SendCommand);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M601AndPCodeCodeFound_PCodeValid_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M601P100.001");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            M601Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(context.SendCommand);
            Assert.IsTrue(result);
            Assert.IsTrue(context.Variables[1].IsDecimal);
            Assert.AreEqual(100.001M, (Decimal)context.Variables[1].Value);
        }
    }
}
