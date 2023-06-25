using System.Collections.Generic;
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
    public class AnalyzeSubprogramTests
    {
        [TestMethod]
        public void AnalyzeSubProgram_ContainsMultipleSubprogramsPerLine_CreatesError()
        {
            string gCode = "O1000O1001\nO1002\nO1003O1004";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeSubPrograms sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(5, analyses.SubProgramCount);
            Assert.AreEqual(7, analyses.Errors.Count);
            Assert.AreEqual("Subprogram O1000 could not be found", analyses.Errors[0]);
            Assert.AreEqual("Subprogram O1001 could not be found", analyses.Errors[1]);
            Assert.AreEqual("Subprogram O1002 could not be found", analyses.Errors[2]);
            Assert.AreEqual("Subprogram O1003 could not be found", analyses.Errors[3]);
            Assert.AreEqual("Subprogram O1004 could not be found", analyses.Errors[4]);
            Assert.AreEqual("Line 1 contains 2 or more subprograms, subprograms must be on a unique line.", analyses.Errors[5]);
            Assert.AreEqual("Line 3 contains 2 or more subprograms, subprograms must be on a unique line.", analyses.Errors[6]);
            
        }

        [TestMethod]
        public void AllLines_CorrectlySplitsOutSubPrograms_Success()
        {
            MockSubprograms subprograms = new();
            subprograms.Subprograms.Add(new SubprogramModel("O1000", "O1000", "X0\nY0\nX10\nY10") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });
            subprograms.Subprograms.Add(new SubprogramModel("O1001", "O1001", "X0\nY0\nX10\nY10") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });
            subprograms.Subprograms.Add(new SubprogramModel("O1002", "O1002", "X0\nY0\nX10\nY10") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });

            string gCode = "G54\nO1000\nG55\nO1001\nG56\nO1002";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), subprograms);
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);

            Assert.AreEqual(6, analyses.Commands.Count);
            List<IGCodeLine> lines = analyses.Lines(out int lineCount);
            Assert.AreEqual(6, lineCount);

            int line = 0;
            List<IGCodeLine> allLines = analyses.AllLines(out int allLineCount);
            Assert.AreEqual(15, allLineCount);
            Assert.AreEqual("G54", allLines[line++].GetGCode());
            Assert.AreEqual("X0", allLines[line++].GetGCode());
            Assert.AreEqual("Y0", allLines[line++].GetGCode());
            Assert.AreEqual("X10", allLines[line++].GetGCode());
            Assert.AreEqual("Y10", allLines[line++].GetGCode());
            Assert.AreEqual("G55", allLines[line++].GetGCode());
            Assert.AreEqual("X0", allLines[line++].GetGCode());
            Assert.AreEqual("Y0", allLines[line++].GetGCode());
            Assert.AreEqual("X10", allLines[line++].GetGCode());
            Assert.AreEqual("Y10", allLines[line++].GetGCode());
            Assert.AreEqual("G56", allLines[line++].GetGCode());
            Assert.AreEqual("X0", allLines[line++].GetGCode());
            Assert.AreEqual("Y0", allLines[line++].GetGCode());
            Assert.AreEqual("X10", allLines[line++].GetGCode());
            Assert.AreEqual("Y10", allLines[line++].GetGCode());
        }

        [TestMethod]
        public void AllLines_CorrectlySplitsOutRepeatedSubProgram_Success()
        {
            MockSubprograms subprograms = new();
            subprograms.Subprograms.Add(new SubprogramModel("O1000", "O1000", "G1X0Y0F10\r\nG1X100Y100F10\r\nG1X0Y0F10\r\nG1X100Y100F10\r\nG1X0Y0F10\r\nG1X100Y100F10\r\nG1X0Y0F10\r\nG1X100Y100F10") { Variables = new() { new GCodeVariableModel(100, "2", 1), new GCodeVariableModel(121, "3", 2) } });

            string gCode = "G54\nO1000\nG55\nO1000\nG56\nO1000";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), subprograms);
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);

            Assert.AreEqual(6, analyses.Commands.Count);
            List<IGCodeLine> lines = analyses.Lines(out int lineCount);
            Assert.AreEqual(6, lineCount);

            int line = 0;
            List<IGCodeLine> allLines = analyses.AllLines(out int allLineCount);
            Assert.AreEqual(27, allLineCount);
            Assert.AreEqual("G54", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());

            Assert.AreEqual("G55", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());

            Assert.AreEqual("G56", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X0Y0F10", allLines[line++].GetGCode());
            Assert.AreEqual("G1X100Y100F10", allLines[line++].GetGCode());
        }
    }
}
