﻿using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer.Analyzers;
using GSendAnalyzer.Internal;

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
            string gCodeWithM602NoComment = "M602 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new(new MockSubprograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is referenced on line 1 but has not been declared.", analyses.Errors[0]);
        }

        [TestMethod]
        public void VariableDeclaredAfterFirstUsage_AddsError_Success()
        {
            string gCodeWithM602NoComment = "M602 [#321]\n#321=a value";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new(new MockSubprograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is used on line 1 but not declared until line 2, variable must be declared before use.", analyses.Errors[0]);
        }

        [TestMethod]
        public void VariableDeclaredButNotUsed_AddsWarnings_Success()
        {
            string gCodeWithM602NoComment = "#200=a\n#201=b\n\n#321=a value\nM602 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new(new MockSubprograms());
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Errors.Count);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #200 is declared on line 1 but not used.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #201 is declared on line 2 but not used.", analyses.Warnings[1]);
        }

        [TestMethod]
        public void VariableDeclaredInSubProgram_NotUsedInCurrentGCode_AddsWarnings_Success()
        {
            MockSubprograms subprograms = new();
            string gCodeWithM602NoComment = "O1000\n#200=a\n#201=b\n\nM602 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), subprograms);
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            SubprogramModel subProgram = new("O1000", "debug with vars", "#321=a value\n#322=value")
            {
                Variables = new()
            };
            subProgram.Variables.Add(new GCodeVariableModel(321, "a", 2));
            subprograms.Subprograms.Add(subProgram);
            AnalyzeVariables sut = new(subprograms);
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Subprogram O1000 could not be found", analyses.Errors[0]);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #200 is declared on line 2 but not used.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #201 is declared on line 3 but not used.", analyses.Warnings[1]);
        }

        [TestMethod]
        public void VariableDeclaredInMultipleSubProgram_AddsError_Success()
        {
            MockSubprograms subprograms = new();
            subprograms.Subprograms.Add(new SubprogramModel("O1000", "O1000", "#100=2\n#121=3") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });
            subprograms.Subprograms.Add(new SubprogramModel("O1001", "O1001", "#100=2\n#121=3") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });
            string gCodeWithM602NoComment = "O1000\nO1001";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), subprograms);
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            AnalyzeVariables sut = new(subprograms);
            sut.Analyze("", analyses);

            Assert.AreEqual(4, analyses.Errors.Count);
            Assert.AreEqual("Invalid variable on line 2, duplicate variable #100 declared.", analyses.Errors[0]);
            Assert.AreEqual("Invalid variable on line 2, duplicate variable #121 declared.", analyses.Errors[1]);
            Assert.AreEqual("Variable #100 is declared in subprogram O1000 on line 1 and redeclared in subprogram O1001 on line 2", analyses.Errors[2]);
            Assert.AreEqual("Variable #121 is declared in subprogram O1000 on line 1 and redeclared in subprogram O1001 on line 2", analyses.Errors[3]);

            Assert.AreEqual(2, analyses.Warnings.Count);
            Assert.AreEqual("Variable #100 is declared in subprogram but not used in the current gcode.", analyses.Warnings[0]);
            Assert.AreEqual("Variable #121 is declared in subprogram but not used in the current gcode.", analyses.Warnings[1]);
        }
    }
}
