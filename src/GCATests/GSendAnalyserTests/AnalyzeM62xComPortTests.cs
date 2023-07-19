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

        [TestMethod]
        public void Analyze_ComPortNonBlockingCommand_ComPortNotSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM622;\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M622 on line 2 does not specify a COM port.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortNonBlockingCommand_ComPortNotSpecifiedOrClosed_AddsErrors()
        {
            string gCodeWithVariables = "M620;COM9\nM622;";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Command M622 on line 2 does not specify a COM port.", analyses.Errors[0]);
            Assert.AreEqual("COM Port \"COM9\" is opened but never closed.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_ComPortNonBlockingCommand_NoCommandToSendSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM622;COM9\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M622 on line 2 does not contain a value that can be sent to a COM port.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_ComPortNotSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not specify a COM port.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_ComPortNotSpecifiedOrClosed_AddsErrors()
        {
            string gCodeWithVariables = "M620;COM9\nM623;";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not specify a COM port.", analyses.Errors[0]);
            Assert.AreEqual("COM Port \"COM9\" is opened but never closed.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_NoCommandToSendSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9:600:ok\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not contain a value that can be sent to a COM port.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_NoCommandResponseSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9:500\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not contain a response value that can be verified after sending data to a COM port.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_NoTimeoutPeriodSpecified_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not specify the timeout period.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_InvalidTimeoutPeriod_BelowMinimum_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9:-99:0k:data\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 contains an invalid timeout period, it must be a number between 100 and 10000.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_InvalidTimeoutPeriod_AboveMaximum_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9:10001:ok:data\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 contains an invalid timeout period, it must be a number between 100 and 10000.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortBlockingCommand_InvalidTimeoutPeriod_String_AddsError()
        {
            string gCodeWithVariables = "M620;COM9\nM623;COM9:timeout:ok:data\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 contains an invalid timeout period, it must be a number between 100 and 10000.", analyses.Errors[0]);
        }
    }
}
