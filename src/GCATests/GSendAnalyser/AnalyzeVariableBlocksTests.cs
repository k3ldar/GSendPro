using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new AnalyzeVariableBlocks();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual("M650 [#321]", line);
            Assert.AreEqual("a value", analyses.Commands[0].VariableBlocks[0].Value);
        }

        [TestMethod]
        public void VariableDeclaredObtainedFromSubProgram_AddsWarnings_Success()
        {
            string gCodeWithM650NoComment = "O1000\n#200=a\n#201=b\n\nM650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            MockSubPrograms mockSubPrograms = new MockSubPrograms();
            SubProgramModel subProgram = new SubProgramModel("O1000", "debug with vars", "#321=a value\n#322=value");
            subProgram.Variables = new();
            subProgram.Variables.Add(new GCodeVariableModel(321, "a", 2));
            mockSubPrograms.SubPrograms.Add(subProgram);
            AnalyzeVariables sut = new AnalyzeVariables(mockSubPrograms);
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #200 is declared on line 2 but not used.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #201 is declared on line 3 but not used.", analyses.Warnings[1]);
        }

        [TestMethod]
        public void VariableBlock_MultipleStringValueReplaced_Success()
        {
            string gCodeWithM650NoComment = "#321=a value\n#322=45.6 ; speed\n#345=test\n#350=pass\nM650 [#321][#322] [#345 + #350] ; test";
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariableBlocks sut = new AnalyzeVariableBlocks();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(0, analyses.Warnings.Count);

            string line = analyses.Lines(out int _)[0].GetGCode();
            Assert.AreEqual("M650 [#321][#322] [#345 + #350]", line);
            Assert.AreEqual(3, analyses.Commands[0].VariableBlocks.Count);
            Assert.AreEqual("a value", analyses.Commands[0].VariableBlocks[0].Value);
            Assert.AreEqual("45.6", analyses.Commands[0].VariableBlocks[1].Value);
            Assert.AreEqual("test + pass", analyses.Commands[0].VariableBlocks[2].Value);
        }
    }
}
