using GSendAnalyzer;
using GSendAnalyzer.Internal;
using GSendCommon.MCodeOverrides;
using GSendShared;
using GSendShared.Models;
using GSendTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M630Tests
    {
        [TestMethod]
        public void Process_M630CodeNotFound_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M604");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M630Override sut = new(new MockRunProgram());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgs_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            M630Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.IsNull(mockRunProgram.Parameters);
            Assert.IsTrue(mockRunProgram.UseShellExecute);
            Assert.IsFalse(mockRunProgram.WaitForFinish);
            Assert.AreEqual(Int32.MinValue, mockRunProgram.ReturnValue);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithArgsPrefix_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630.1;-p /m\nM630;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            M630Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("-p /m", mockRunProgram.Parameters);
            Assert.IsTrue(mockRunProgram.UseShellExecute);
            Assert.IsFalse(mockRunProgram.WaitForFinish);
            Assert.AreEqual(Int32.MinValue, mockRunProgram.ReturnValue);
        }
    }
}
