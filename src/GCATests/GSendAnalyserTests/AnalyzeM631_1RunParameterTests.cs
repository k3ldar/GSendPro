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
    public class AnalyzeM631_1RunParameterTests
    {
        [TestMethod]
        public void Analyze_NoValidMCodes_ReturnsFalse()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602\nG0Z40.000\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(6, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_DuplicateCommandsPerLine_AddsError()
        {
            string gCodeWithM602NoComment = "M631.1M631.1;test\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains multiple commands, M631.1 command must be on a line of it's own.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_MultipleCommandsPerLine_AddsError()
        {
            string gCodeWithM602NoComment = "M0M631.1;test\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains multiple commands, M631.1 command must be on a line of it's own.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_NoParameters_AddsError()
        {
            string gCodeWithM602NoComment = "M631.1;\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M631.1 command but no parameters were found, you must specify parameters for an M631 command.", analyses.Errors[0]);
        }

        [TestMethod]
        public void AnalyzeNextCommandNotM631_AddsError()
        {
            string gCodeWithM602NoComment = "M631.1;test\nM631.2P0";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M631.1 command which must be followed by an M631 or M631.2 command.", analyses.Errors[0]);
        }

        [TestMethod]
        public void AnalyzeNextCommandIsM631_NoErrors()
        {
            string gCodeWithM602NoComment = "M631.1;test\nM631.2P0\nM631;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(4, analyses.Commands.Count);

            AnalyzeM631_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }
    }
}
