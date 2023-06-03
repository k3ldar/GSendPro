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
    public class AnalyzeVariablesTests
    {
        [TestMethod]
        public void VariableNotDeclared_AddsError_Success()
        {
            string gCodeWithM650NoComment = "M650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new AnalyzeVariables(new MockSubPrograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is referenced on line 1 but has not been declared.", analyses.Errors[0]);
        }

        [TestMethod]
        public void VariableDeclaredAfterFirstUsage_AddsError_Success()
        {
            string gCodeWithM650NoComment = "M650 [#321]\n#321=a value";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new AnalyzeVariables(new MockSubPrograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is used on line 1 but not declared until line 2, variable must be declared before use.", analyses.Errors[0]);
        }

        [TestMethod]
        public void VariableDeclaredButNotUsed_AddsWarnings_Success()
        {
            string gCodeWithM650NoComment = "#200=a\n#201=b\n\n#321=a value\nM650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new AnalyzeVariables(new MockSubPrograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #200 is declared on line 1 but not used.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #201 is declared on line 2 but not used.", analyses.Warnings[1]);
        }

        [TestMethod]
        public void VariableDeclaredObtainedFromSubProgram_AddsWarnings_Success()
        {
            string gCodeWithM650NoComment = "O1000\n#200=a\n#201=b\n\nM650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
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
    }
}
