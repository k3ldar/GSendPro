using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

using GSendAnalyser.Internal;

using GSendCommon;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GCodeParserTests
    {
        private const string TestCategoryAnalyser = "Analyser";

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_Success()
        {
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseZProbeCommand()
        {
            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG0X139.948Y37.136Z40.000";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(ZProbeCommand);

            Assert.AreEqual(13, analyses.Commands.Count);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile_Start()
        {
            string finish = "G17\nG21\nG0Z40.000\nG0X0.000Y0.000S8000M3\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(13, analyses.Commands.Count);

        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalWithComments()
        {
            string finish = "G17\nG21;second\nG0Z40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\n;a comment on it;s own\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(14, analyses.Commands.Count);
            Assert.IsFalse(string.IsNullOrEmpty(analyses.Commands[8].Comment));
            Assert.IsFalse(string.IsNullOrEmpty(analyses.Commands[9].Comment));
            Assert.AreEqual(";a comment on it;s own", analyses.Commands[9].Comment);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Parse_WithDuplicates_DuplicatesFound()
        {
            string finish = "G17\nG21;second\nG0Z40.000\nZ40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(14, analyses.Commands.Count);
            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsDuplicates));
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile_FromSingleTextBlock_Success()
        {
            string finish = Encoding.UTF8.GetString(Properties.Resources.test_toolpath2_Machine_Relief_Ball_Nose_3_175_mm_Roughing_Roughing);
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(2260, analyses.Commands.Count);
            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsDuplicates));

            List<IGCodeCommand> movesToSafeZ = analyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.HomeZ)).ToList();
            Assert.AreEqual(14, movesToSafeZ.Count);
            Assert.AreEqual(9765310005, analyses.TotalTime.Ticks);
            Assert.AreEqual(14758.7094m, analyses.TotalDistance);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Parse_SingleOutG1Commands_SetFeedRate_MarkFeedRateOnlyCommandsAsNoneCommand_Success()
        {
            string finish = "G0Z51.800\r\nG0X0.000Y0.000S8000M3\r\nG0X-14.414Y-109.500Z32.000\r\nG1Z17.109F400.0\r\nG1X-14.300F2000.0\r\nX18.134\r\n" +
                "X18.951Y-109.400\r\nX-15.424\r\nX-16.434Y-109.300\r\nX19.767\r\nX20.584Y-109.200\r\nX7.403Y109.396\r\nX4.767\r\nX6.442Y109.496\r\n" +
                "X6.544\r\nG0Z32.000\r\nG0X0.000Y0.000Z51.800\r\nG0Z51.800\r\nG0X0Y0\r\nM30\r\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(44, analyses.Commands.Count);
            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsDuplicates));

            List<IGCodeCommand> movesToSafeZ = analyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.HomeZ)).ToList();
            Assert.AreEqual(16, movesToSafeZ.Count);
            Assert.AreEqual(148530164, analyses.TotalTime.Ticks);
            Assert.AreEqual(703.494m, analyses.TotalDistance);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalToolChanges()
        {
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(Encoding.UTF8.GetString(Properties.Resources.test_toolpath5));
            List<IGCodeCommand> toolChanges = analyses.Commands.Where(c => c.Command.Equals('T')).ToList();
            Assert.AreEqual(2, toolChanges.Count);
            Assert.AreEqual(18305, analyses.Commands.Count);

        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseVariablesFromGCodeFile_Success()
        {
            string gCodeWithVariables = "#100=15.2\n#101=This is a string value=multiple=equals=signs\n#12=123\n#15=\nG17\nG21;second\nG0Z40.000\nZ40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Variables.Count);
            Assert.IsTrue(analyses.Variables.ContainsKey(100));
            Assert.AreEqual(15.2m, analyses.Variables[100].Value);
            Assert.IsTrue(analyses.Variables[100].IsDecimal);

            Assert.IsTrue(analyses.Variables.ContainsKey(101));
            Assert.AreEqual("This is a string value=multiple=equals=signs", analyses.Variables[101].Value);

            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Invalid variable on line 3, variable name must be a number between 100 and 65535.", analyses.Errors[0]);
            Assert.AreEqual("Invalid variable on line 4, must contain a name and value, i.e. #101=value", analyses.Errors[1]);
            Assert.AreEqual(14, analyses.Commands.Count);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseMCodeWithVariablesFromGCodeFile_Success()
        {
            string gCodeWithVariables = "#100=15.2\n#101=This is a string\nM600 [ #100 #101] \n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Variables.Count);
            Assert.IsTrue(analyses.Variables.ContainsKey(100));
            Assert.AreEqual(15.2m, analyses.Variables[100].Value);
            Assert.IsTrue(analyses.Variables[100].IsDecimal);

            Assert.IsTrue(analyses.Variables.ContainsKey(101));
            Assert.AreEqual("This is a string", analyses.Variables[101].Value);

            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual(1, analyses.Commands.Count);

            Assert.AreEqual('M', analyses.Commands[0].Command);
            Assert.AreEqual(600, analyses.Commands[0].CommandValue);
            Assert.AreEqual(1, analyses.Commands[0].VariableBlocks.Count);
            Assert.AreEqual("[ #100 #101]", analyses.Commands[0].VariableBlocks[0].VariableBlock);
            Assert.AreEqual(2, analyses.Commands[0].VariableBlocks[0].Variables.Count);
            Assert.AreEqual("#100", analyses.Commands[0].VariableBlocks[0].Variables[0]);
            Assert.AreEqual("#101", analyses.Commands[0].VariableBlocks[0].Variables[1]);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseMCodeWithVariables_NoEndingBrace_ReportsError()
        {
            string gCodeWithVariables = "#100=15.2\n#101=This is a string\nM600[ #100 #101 \n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Variables.Count);
            Assert.IsTrue(analyses.Variables.ContainsKey(100));
            Assert.AreEqual(15.2m, analyses.Variables[100].Value);
            Assert.IsTrue(analyses.Variables[100].IsDecimal);

            Assert.IsTrue(analyses.Variables.ContainsKey(101));
            Assert.AreEqual("This is a string", analyses.Variables[101].Value);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid variable block on line 3, variable block must start with [ and end with ]", analyses.Errors[0]);

            Assert.AreEqual(1, analyses.Commands.Count);

            Assert.AreEqual('M', analyses.Commands[0].Command);
            Assert.AreEqual(600, analyses.Commands[0].CommandValue);
            Assert.AreEqual(0, analyses.Commands[0].VariableBlocks.Count);

        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseMCodeWithMultipleVariableBlocksFromGCodeFile_Success()
        {
            string gCodeWithVariables = "#100=15.2\n#101=This is a string\n#103=2000\nG1X[#100 + 1]Y[#101]Z[-200 + 1]F[#103]\n";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(3, analyses.Variables.Count);
            Assert.IsTrue(analyses.Variables.ContainsKey(100));
            Assert.AreEqual(15.2m, analyses.Variables[100].Value);
            Assert.IsTrue(analyses.Variables[100].IsDecimal);

            Assert.IsTrue(analyses.Variables.ContainsKey(101));
            Assert.AreEqual("This is a string", analyses.Variables[101].Value);

            Assert.IsTrue(analyses.Variables.ContainsKey(103));
            Assert.AreEqual(2000m, analyses.Variables[103].Value);

            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual(5, analyses.Commands.Count);

            Assert.AreEqual('G', analyses.Commands[0].Command);
            Assert.AreEqual(1, analyses.Commands[0].CommandValue);
            Assert.AreEqual(0, analyses.Commands[0].VariableBlocks.Count);

            Assert.AreEqual('X', analyses.Commands[1].Command);
            Assert.AreEqual(Decimal.MinValue, analyses.Commands[1].CommandValue);
            Assert.AreEqual(1, analyses.Commands[1].VariableBlocks.Count);
            Assert.AreEqual("[#100 + 1]", analyses.Commands[1].VariableBlocks[0].VariableBlock);

            Assert.AreEqual('Y', analyses.Commands[2].Command);
            Assert.AreEqual(Decimal.MinValue, analyses.Commands[2].CommandValue);
            Assert.AreEqual(1, analyses.Commands[2].VariableBlocks.Count);
            Assert.AreEqual("[#101]", analyses.Commands[2].VariableBlocks[0].VariableBlock);

            Assert.AreEqual('Z', analyses.Commands[3].Command);
            Assert.AreEqual(Decimal.MinValue, analyses.Commands[3].CommandValue);
            Assert.AreEqual(1, analyses.Commands[3].VariableBlocks.Count);
            Assert.AreEqual("[-200 + 1]", analyses.Commands[3].VariableBlocks[0].VariableBlock);

            Assert.AreEqual('F', analyses.Commands[4].Command);
            Assert.AreEqual(Decimal.MinValue, analyses.Commands[4].CommandValue);
            Assert.AreEqual(1, analyses.Commands[4].VariableBlocks.Count);
            Assert.AreEqual("[#103]", analyses.Commands[4].VariableBlocks[0].VariableBlock);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseCodeWithVariableNotSurroundedBySquareBrackets_CreatesError()
        {
            string gCodeWithVariables = "S#100M3";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(0, analyses.Variables.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.IsTrue(analyses.Commands[0].Attributes.HasFlag(CommandAttributes.SpindleSpeedError));
            Assert.AreEqual("Invalid variable on line 1, variable must be enclosed by square brackets (i.e. [#234])", analyses.Errors[0]);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseCodeWithTwoVariables_Success()
        {
            string gCodeWithVariables = "G0;comment\n#101=123\n#102=45";
            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(2, analyses.Variables.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseCodeWithSubProgram_SubProgramVariablesAndValuesRecognised_Success()
        {
            string subProgramContents = "#100=8000 ;Spindle Speed\nT1\nG17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000\nS[#100]M3\n";
            SubprogramModel subProgram = new("O1001", "mock", subProgramContents);

            MockGSendApiWrapper mockApiWrapper = new();
            mockApiWrapper.Subprograms.Add(subProgram);

            string gCodeWithVariables = "O1001 ;sub start\n";
            GCodeParser sut = new(new MockPluginClassesService(), mockApiWrapper);
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);
            Assert.AreEqual(CommandAttributes.SpindleSpeedError | CommandAttributes.MovementZUp | CommandAttributes.UseRapidRate | CommandAttributes.ToolChange | CommandAttributes.SubProgram, analyses.Commands[0].Attributes);
            Assert.IsNotNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Variables.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseCodeWithSubProgram_VariableCreatedByGCode_RedeclaredInSubProgram_CreatesError()
        {
            string subProgramContents = "#100=8000 ;Spindle Speed\nT1\nG17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000\nS[#100]M3\n";
            SubprogramModel subProgram = new("O1001", "mock", subProgramContents);

            MockGSendApiWrapper mockApiWrapper = new();
            mockApiWrapper.Subprograms.Add(subProgram);

            string gCodeWithVariables = "#100=23\nO1001 ;sub start\n";
            GCodeParser sut = new(new MockPluginClassesService(), mockApiWrapper);
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);
            Assert.AreEqual(CommandAttributes.SpindleSpeedError | CommandAttributes.MovementZUp | CommandAttributes.UseRapidRate | CommandAttributes.ToolChange | CommandAttributes.SubProgram, analyses.Commands[0].Attributes);
            Assert.IsNotNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Variables.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid variable on line 2, duplicate variable #100 declared.", analyses.Errors[0]);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseCodeWithSubProgram_MaximumRecursionLimitReached_CreatesError()
        {
            MockGSendApiWrapper mockApiWrapper = new();

            string subProgramContents = "G17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000\nS[#100]M3\nO1001\n";
            mockApiWrapper.Subprograms.Add(new SubprogramModel("O1001", "mock", subProgramContents));


            string gCodeWithVariables = "O1001 ;recursion test\n";
            GCodeParser sut = new(new MockPluginClassesService(), mockApiWrapper);
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(1, analyses.Commands.Count);
            Assert.IsNotNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Maximum recursion depth has been reached when retrieving subprograms.", analyses.Errors[0]);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void AllCommands_SetsCorrectLineNumber_Success()
        {
            MockGSendApiWrapper mockApiWrapper = new();

            string subProgramContents = "G17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000\nS200M3\n";
            mockApiWrapper.Subprograms.Add(new SubprogramModel("O1001", "mock", subProgramContents));
            subProgramContents = "X34.2Y15\nX56\nX15";
            mockApiWrapper.Subprograms.Add(new SubprogramModel("O1002", "mock", subProgramContents));
            subProgramContents = "X34.2Y15\nX56\nX15Y10";
            mockApiWrapper.Subprograms.Add(new SubprogramModel("O1003", "mock", subProgramContents));


            string gCodeWithVariables = "O1001\nO1002\nO1003";
            GCodeParser sut = new(new MockPluginClassesService(), mockApiWrapper);
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            IReadOnlyList<IGCodeCommand> allCommands = analyses.AllCommands;
            Assert.AreEqual(19, allCommands.Count);

            int lineNumber = 0;
            Assert.AreEqual(++lineNumber, allCommands[0].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[1].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[2].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[3].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[4].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[5].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[6].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[7].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[8].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[9].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[10].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[11].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[12].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[13].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[14].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[15].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[16].MasterLineNumber);
            Assert.AreEqual(++lineNumber, allCommands[17].MasterLineNumber);
            Assert.AreEqual(lineNumber, allCommands[18].MasterLineNumber);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Parse_CommentLineTooLong_CreatesWarning()
        {
            string gCodeWithVariables = ";This will be a very long comment that the parser will pick up and add an enum to, the enum that it will add will be CommandAttribute.InvalidCommentTooLong after which a warning will be added to indicate that this line will be ignored in it's entirety. OfGcoure to get the warning we need to make sure the string is longer than the permitted of two hundred and fifty six characters\nG17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000";

            GCodeParser sut = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = sut.Parse(gCodeWithVariables);

            Assert.AreEqual(9, analyses.Commands.Count);
            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual('\0', analyses.Commands[0].Command);
            Assert.AreEqual(decimal.MinValue, analyses.Commands[0].CommandValue);
            Assert.AreEqual(";This will be a very long comment that the parser will pick up and add an enum to, the enum that it will add will be CommandAttribute.InvalidCommentTooLong after which a warning will be added to indicate that this line will be ignored in it's entirety. OfG", analyses.Commands[0].Comment);
            Assert.AreEqual("", analyses.Commands[0].CommandValueString);
            Assert.AreEqual(1, analyses.Commands[0].LineNumber);
            Assert.AreEqual(CommandAttributes.InvalidCommentTooLong, analyses.Commands[0].Attributes);
            Assert.IsFalse(analyses.Commands[1].Attributes.HasFlag(CommandAttributes.InvalidCommentTooLong));
            Assert.AreEqual(2, analyses.Commands[1].LineNumber);
            Assert.AreEqual('G', analyses.Commands[1].Command);
            Assert.AreEqual("17", analyses.Commands[1].CommandValueString);
        }
    }
}
