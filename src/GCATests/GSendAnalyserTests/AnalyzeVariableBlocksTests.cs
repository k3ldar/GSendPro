using System.Diagnostics.CodeAnalysis;

using GSendAnalyser.Analysers;
using GSendAnalyser.Internal;

using GSendCommon;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeVariableBlockTests
    {
        [TestMethod]
        public void VariableBlock_StringValueReplaced_Success()
        {
            string gCodeWithM650NoComment = "#321=a value\nM650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual("M650 a value", line);
            Assert.AreEqual("a value", analyses.Commands[0].VariableBlocks[0].Value);
        }

        [TestMethod]
        public void VariableBlock_NumericValueReplaced_Success()
        {
            string gCodeWithM650NoComment = "#321=57\nM[#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual("M57", line);
            Assert.AreEqual("57", analyses.Commands[0].VariableBlocks[0].Value);
        }

        [TestMethod]
        public void VariableDeclaredObtainedFromSubProgram_AddsWarnings_Success()
        {
            MockSubprograms mockSubprograms = new();
            string gCodeWithM650NoComment = "O1000\n#200=a\n#201=b\n\nM650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), mockSubprograms);
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            SubprogramModel subProgram = new("O1000", "debug with vars", "#321=a value\n#322=value")
            {
                Variables = new()
            };
            subProgram.Variables.Add(new GCodeVariableModel(321, "a", 2));
            mockSubprograms.Subprograms.Add(subProgram);
            AnalyzeVariables sut = new(mockSubprograms);
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Subprogram O1000 could not be found", analyses.Errors[0]);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #200 is declared on line 2 but not used.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #201 is declared on line 3 but not used.", analyses.Warnings[1]);
        }

        [TestMethod]
        public void VariableBlock_MultipleStringValueReplaced_Success()
        {
            string gCodeWithM650NoComment = "#321=a value\n#322=45.6 ; speed\n#345=test\n#350=pass\nM650 [#321][#322] [#345 + #350] ; test";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual(3, analyses.Commands[0].VariableBlocks.Count);
            Assert.AreEqual("a value", analyses.Commands[0].VariableBlocks[0].Value);
            Assert.AreEqual("45.6", analyses.Commands[0].VariableBlocks[1].Value);
            Assert.AreEqual("test + pass", analyses.Commands[0].VariableBlocks[2].Value);
            Assert.AreEqual("M650 a value45.6 test + pass", line);
        }

        [TestMethod]
        public void VariableBlock_MultipleStringValueReplaced_InComments_Success()
        {
            string gCodeWithM650NoComment = "#100=C:\\Windows\\Media\\\n#101=Alarm01.wav\n#102=notify.wav\nM605 ; [#100#101]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual(1, analyses.Commands[0].VariableBlocks.Count);
            Assert.AreEqual("C:\\Windows\\Media\\Alarm01.wav", analyses.Commands[0].VariableBlocks[0].Value);
            Assert.AreEqual("M605", line);
            Assert.AreEqual(" C:\\Windows\\Media\\Alarm01.wav", analyses.Commands[0].CommentStripped(true));
        }
    }
}
