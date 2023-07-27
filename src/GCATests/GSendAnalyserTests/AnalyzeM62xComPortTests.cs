using System.Diagnostics.CodeAnalysis;

using GSendAnalyser.Analyzers;
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
        public void Analyze_ComPortParameterTimeOutValidMax_Success()
        {
            string gCodeWithVariables = "M620;COM1\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterTimeOutValidMin_Success()
        {
            string gCodeWithVariables = "M620;COM1:100\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterBaudRateIsString_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:asdf";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, Baud Rate must be a Number value", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterBaudRateBelowMinimum_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:0";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, baud rate must be greater than zero", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityNotRecognised_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:9200:string";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, Parity must be None, Odd, Even, Mark or Space", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityValid_None_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityValid_Odd_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:odd\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityValid_Even_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:EvEn\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityValid_Mark_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:Mark\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterParityValid_Space_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:sPaCe\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBitsInvalid_String_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:string";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, Data bits must be 5, 6, 7, or 8", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBitsInvalidNumber_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:4";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, Data bits must be 5, 6, 7, or 8", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBits_ValidNumber5_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:5\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBits_ValidNumber6_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:6\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBits_ValidNumber7_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:7\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterDataBits_ValidNumber8_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:8\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterStopBitsInvalid_String_AddsError()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:8:asdf";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid COM port parameter, Stop bits must be One, Two or OnePointFive", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_ComPortParameterStopBitsValid_One_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:8:oNe\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterStopBitsValid_Two_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:8:two\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_ComPortParameterStopBitsValid_OnePointFive_Success()
        {
            string gCodeWithVariables = "M620;COM1:9200:None:8:OnePointFive\nM621;COM1";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 1 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
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
            string gCodeWithVariables = "M620;COM9\nM623;COM9:ok\nM621;COM9";

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
            string gCodeWithVariables = "M620;COM9\nM623;COM9\nM621;COM9";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Commands.Count);


            AnalyzeM62XComPorts sut = new(new MockComPortProvider(new byte[] { 9 }));
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Command M623 on line 2 does not contain a response value that can be verified after sending data to a COM port.", analyses.Errors[0]);
        }
    }
}
