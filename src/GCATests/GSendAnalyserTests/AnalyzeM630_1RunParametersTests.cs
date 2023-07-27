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
    public class AnalyzeM630_1RunParametersTests
    {
        [TestMethod]
        public void Analyze_NoValidMCodes_ReturnsFalse()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602\nG0Z40.000\nM630;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(6, analyses.Commands.Count);

            AnalyzeM630_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_DuplicateCommandsPerLine_AddsError()
        {
            string gCodeWithM602NoComment = "M630.1M630.1;test\nM630;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM630_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains multiple commands, M630.1 command must be on a line of it's own.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_MultipleCommandsPerLine_AddsError()
        {
            string gCodeWithM602NoComment = "M0M630.1;test\nM630;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(3, analyses.Commands.Count);

            AnalyzeM630_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains multiple commands, M630.1 command must be on a line of it's own.", analyses.Errors[0]);
        }

        [TestMethod]
        public void Analyze_NoParameters_AddsError()
        {
            string gCodeWithM602NoComment = "M630.1;\nM630;prog";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(2, analyses.Commands.Count);

            AnalyzeM630_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M630.1 command but no parameters were found, you must specify parameters for an M630 command.", analyses.Errors[0]);
        }

        [TestMethod]
        public void AnalyzeNextCommandNotM630_AddsError()
        {
            string gCodeWithM602NoComment = "M630.1;test";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeM630_1RunParameters sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M630.1 command which must be followed by an M630 command.", analyses.Errors[0]);
        }
    }
}
