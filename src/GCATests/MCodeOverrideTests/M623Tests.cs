using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;
using System.Threading;

using GSendAnalyzer;
using GSendAnalyzer.Internal;

using GSendCommon.MCodeOverrides;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M623Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParameter_Null_ThrowsException()
        {
            M623Override sut = new M623Override(null);
        }

        [TestMethod]
        public void Process_M623CodeNotFound_Returns_False()
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

            M623Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_M623InvalidComPort_Throws_ArgumentException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            M623Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_M623InvalidResponse_Throws_ArgumentException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;COM9");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            M623Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_M623InvalidCommandNotFound_Throws_ArgumentException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;COM9:ok");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            M623Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M623ValidParameters_ReturnsTrue()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M601P500\nM623;COM9:ok:somedata");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            MockComPort mockComPort = new(new ComPortModel("COM9", 100, 115200, Parity.Odd, 8, StopBits.One));
            mockComPort.Open();
            M623Override sut = new(new MockComPortFactory(mockComPort));
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(1, mockComPort.Commands.Count);
            Assert.AreEqual("somedata", mockComPort.Commands[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void Process_M623ValidParametersValid_ReturnsTrue()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;COM9:bad response:somedata");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            MockComPort mockComPort = new(new ComPortModel("COM9", 100, 115200, Parity.Odd, 8, StopBits.One));
            mockComPort.Open();
            M623Override sut = new(new MockComPortFactory(mockComPort));
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(1, mockComPort.Commands.Count);
            Assert.AreEqual("somedata", mockComPort.Commands[0]);
        }

        [TestMethod]
        public void Process_M623ValidParametersCommandHasColon_ReturnsTrue()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;COM9:ok:somedata:1:2:3:4:5");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            MockComPort mockComPort = new(new ComPortModel("COM9", 100, 115200, Parity.Odd, 8, StopBits.One));
            mockComPort.Open();
            M623Override sut = new(new MockComPortFactory(mockComPort));
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(1, mockComPort.Commands.Count);
            Assert.AreEqual("somedata:1:2:3:4:5", mockComPort.Commands[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_M623ValidParametersComPortClosed_Throws_InvalidOperationException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M623;COM9:ok:somedata");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine,
                Variables = analyses.Variables,
            };

            MockComPort mockComPort = new(new ComPortModel("COM9", 100, 115200, Parity.Odd, 8, StopBits.One));
            M623Override sut = new(new MockComPortFactory(mockComPort));
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(1, mockComPort.Commands.Count);
            Assert.AreEqual("somedata", mockComPort.Commands[0]);
        }
    }
}
