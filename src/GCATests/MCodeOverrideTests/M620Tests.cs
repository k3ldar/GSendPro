using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GSendAnalyser.Internal;
using GSendAnalyser;

using GSendCommon.MCodeOverrides;

using GSendShared.Models;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using GSendShared.Helpers;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M620Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParameter_Null_ThrowsException()
        {
            M620Override sut = new M620Override(null);
        }

        [TestMethod]
        public void Process_M620CodeNotFound_Returns_False()
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

            M620Override sut = new(new MockComPortFactory());
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_M620InvlaidParameters_Throws_ArgumentException()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M620;");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

           M620Override sut = new(new MockComPortFactory());
           sut.Process(context, CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Process_M620UnableToOpenComPort_Throws_FileNotFoundException()
        {
            string comArgs = "COM65";
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse($"M620;{comArgs}");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockComPort mockComPort = new(ValidateParameters.ExtractComPortProperties(comArgs.Split(new char[] { '\n' })));
            mockComPort.ThrowFileNotFoundException = true;
            MockComPortFactory mockComPortFactory = new MockComPortFactory(mockComPort);
            M620Override sut = new(mockComPortFactory);
            sut.Process(context, CancellationToken.None);
        }

        [TestMethod]
        public void Process_M620OpenComPort_PreventsSendingOfCommand_ReturnsTrue()
        {
            string comArgs = "COM65";
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse($"M620;{comArgs}");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            MockComPort mockComPort = new(ValidateParameters.ExtractComPortProperties(comArgs.Split(new char[] { '\n' })));
            MockComPortFactory mockComPortFactory = new MockComPortFactory(mockComPort);
            M620Override sut = new(mockComPortFactory);
            bool result = sut.Process(context, CancellationToken.None);
            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
        }
    }
}
