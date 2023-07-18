using System.Diagnostics.CodeAnalysis;

using GSendAnalyser.Analysers;
using GSendAnalyser.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeM62xComPortTests
    {
        [TestMethod]
        public void Analyze_ComPortDoesNotExist_AddsError()
        {
            string gCodeWithVariables = "M620;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("The COM port \"COM1\" on line 1 does not exist.", analyses.Errors[0]);
        }
        [TestMethod]
        public void Analyze_ComPortIsNotOnALineOfItsOwn_PreviousCommand_AddsError()
        {
            string gCodeWithVariables = "M5M620;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("M620 command on line 1 must appear on it's own line without any other commands.", analyses.Errors[0]);
            Assert.AreEqual("COM Port \"COM9\" is opened but never closed.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_ComPortIsNotOnALineOfItsOwn_NextCommand_success()
        {
            string gCodeWithVariables = "M620;COM9M5\nM621;COM9M5";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider("COM9M5"));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_AttemptToOpenDuplicateComPort_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM620;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("M620 command on line 2 attempts to open a COM port \"COM9\" when it is already open.", analyses.Errors[0]);
            Assert.AreEqual("COM Port \"COM9\" is opened but never closed.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_ComPortNeverOpened_AddsError()
        {
            string gCodeWithVariables = "M621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("M620 command on line 1 attempts to close a COM port \"COM9\" that was never opened.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortNotClosed_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM621;COM9\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("M620 command on line 3 attempts to close a COM port \"COM9\" that is already closed.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortOpenedAndNotClosed_AddsError()
        {
            string gCodeWithVariables = "M620;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("COM Port \"COM9\" is opened but never closed.", analyses.Errors[0]);
        }
    }
}
