using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSendAnalyzer.Internal;
using GSendAnalyzer;
using GSendCommon.MCodeOverrides;
using GSendShared.Models;
using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Ports;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M621Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParameter_Null_ThrowsException()
        {
            new M621Override(null);
        }

        [TestMethod]
        public void Process_M621CodeNotFound_Returns_False()
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

            M621Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_M621InvalidComPort_Throws_ArgumentException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M621;");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M621Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_M621ComPortNotOpen_Throws_InvalidOperationException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M621;COM8");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M621Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M623ValidParameters_ReturnsTrue()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M621;COM9");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockComPort mockComPort = new(new ComPortModel("COM9", 100, 115200, Parity.Odd, 8, StopBits.One));
            mockComPort.Open();
            M621Override sut = new(new MockComPortFactory(mockComPort));
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
            Assert.AreEqual(0, mockComPort.Commands.Count);

            Assert.AreEqual(1, context.SendInformation.Count);
            Assert.AreEqual("Information - COM9 has been closed.", context.SendInformation[0]);
        }
    }
}
