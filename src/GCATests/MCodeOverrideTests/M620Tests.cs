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
using GSendShared.Models;
using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSendCommon.MCodeOverrides;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M620Tests
    {
        [TestMethod]
        public void Process_M600CodeNotFound_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("S3000M3");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new MockOverrideContext(machineStateModel);
            context.GCode = gCodeLine;

            M600Override sut = new M600Override();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M600CodeFound_NoPCode_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M600");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new MockOverrideContext(machineStateModel);
            context.GCode = gCodeLine;

            M600Override sut = new M600Override();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M600AndPCodeCodeFound_PCodeValueLessThanMinimum_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M600P0");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new MockOverrideContext(machineStateModel);
            context.GCode = gCodeLine;

            M600Override sut = new M600Override();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M600AndPCodeCodeFound_PCodeValueGreaterThanMaximum_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M600P2001");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new MockOverrideContext(machineStateModel);
            context.GCode = gCodeLine;

            M600Override sut = new M600Override();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M600AndPCodeCodeFound_PCodeValid_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M600P0.001");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new MachineStateModel();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new MockOverrideContext(machineStateModel);
            context.GCode = gCodeLine;

            M600Override sut = new M600Override();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
        }
    }
}
