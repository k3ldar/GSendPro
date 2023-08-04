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
    public class M631Tests
    {
        [TestMethod]
        public void Process_M631CodeNotFound_Returns_False()
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

            M631Override sut = new(new MockRunProgram());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgs_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.2P0\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 0;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.IsNull(mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(0, mockRunProgram.ReturnValue);
            Assert.AreEqual(1, context.SendInformation.Count);
            Assert.AreEqual("Warning - Running a program on line 2, timeout period not specified using M601, default value of 1000 milliseconds will be used.", context.SendInformation[0]);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgsAndNoReturnCode_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.1;--test\nM631.2\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 0;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);

            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("--test", mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(0, mockRunProgram.ReturnValue);
            Assert.AreEqual(2, context.SendInformation.Count);
            Assert.AreEqual("Warning - Running a program on line 3, return code not found, default return value of zero will be used.", context.SendInformation[0]);
            Assert.AreEqual("Warning - Running a program on line 3, timeout period not specified using M601, default value of 1000 milliseconds will be used.", context.SendInformation[1]);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgsAndReturnCodeAfter_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.1;--test\nM631.2P0\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 0;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);

            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("--test", mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(0, mockRunProgram.ReturnValue);
            Assert.AreEqual(1, context.SendInformation.Count);
            Assert.AreEqual("Warning - Running a program on line 3, timeout period not specified using M601, default value of 1000 milliseconds will be used.", context.SendInformation[0]);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgsAndReturnCodeBefore_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.1;--test\nP0M631.2\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 0;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);

            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("--test", mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(0, mockRunProgram.ReturnValue);
            Assert.AreEqual(1, context.SendInformation.Count);
            Assert.AreEqual("Warning - Running a program on line 3, timeout period not specified using M601, default value of 1000 milliseconds will be used.", context.SendInformation[0]);
        }

        [TestMethod]
        public void Process_M630CodeFound_WithoutArgsAndReturnCodeBefore_TimeoutSpecified_Returns_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.1;--test\nM601P435\nP0M631.2\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 0;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);

            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("--test", mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(0, mockRunProgram.ReturnValue);
            Assert.AreEqual(0, context.SendInformation.Count);
            Assert.AreEqual(435, mockRunProgram.TimeoutMilliseconds);
        }


        [TestMethod]
        public void Process_InvalidReturnCode_AddsError_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M631.1;--test\nM601P1000\nP0M631.2\nM631;myprog.exe");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockRunProgram mockRunProgram = new MockRunProgram();
            mockRunProgram.ReturnValue = 1;
            M631Override sut = new(mockRunProgram);
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);

            Assert.AreEqual("myprog.exe", mockRunProgram.ProgramName);
            Assert.AreEqual("--test", mockRunProgram.Parameters);
            Assert.IsFalse(mockRunProgram.UseShellExecute);
            Assert.IsTrue(mockRunProgram.WaitForFinish);
            Assert.AreEqual(1, mockRunProgram.ReturnValue);
            Assert.AreEqual(1, context.SendInformation.Count);
            Assert.AreEqual(1000, mockRunProgram.TimeoutMilliseconds);
            Assert.AreEqual("Error - Unexpected return code for M631 on line 4.  Expected 0 actual result was 1.", context.SendInformation[0]);
        }
    }
}
