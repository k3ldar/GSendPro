using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer.Analyzers;
using GSendAnalyzer.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeM631_2RunResponseTests
    {
        [TestMethod]
        public void Analyze_NoValidMCodes_ReturnsFalse()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602\nG0Z40.000\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(6, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_MissingReturnCode_AddsError()
        {
            string gCodeWithM602NoComment = "M631.2\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Invalid M631.2 on line 1.  M631.2 must be followed by a P-code specifying the expected return value", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_MultipleCommandsPerLine_AddsError()
        {
            string gCodeWithM602NoComment = "M0M631.2P0;test\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(4, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("P631.2 on line 1 can not have any other commands on the same line except a P command indicating the return result.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_NextCommandNotM631_2_AddsError()
        {
            string gCodeWithM602NoComment = "M631.1;test\nM631.2P0";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 2 contains M631.2 command which must be followed by an M631 or M631.1 command.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_NextCommandIsM631_2_NoErrors()
        {
            string gCodeWithM602NoComment = "M631.1P0\nM631.2P0\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_PreviousCommandIsM631_2_NoErrors()
        {
            string gCodeWithM602NoComment = "M631.1P0\nP0M631.2\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM631_2ReturnCode sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }
    }
}
