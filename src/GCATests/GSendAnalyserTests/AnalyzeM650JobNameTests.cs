using System;
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
    public class AnalyzeM650JobNameTests
    {
        [TestMethod]
        public void AnalyzeJobName_NoComment_AddsOptionForInvalidJobName()
        {
            string gCodeWithM650NoComment = "G17\nG21\nM650\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM650JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName));
        }

        [TestMethod]
        public void AnalyzeJobName_JobNameSpecified_CorrectlySetsAnalysesJobName()
        {
            string gCodeWithM650NoComment = "G17\nG21\nM650 ;(Invalid Job)\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM650JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName));

            Assert.AreEqual("Invalid Job", analyses.JobName);
        }

        [TestMethod]
        public void AnalyzeJobName_MultipleJobNameSpecified_AddsOptionForMultipleJobNames()
        {
            string gCodeWithM650NoComment = "M650M650\nG17\nG21\nM650 ;(Invalid Job)\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(7, analyses.Commands.Count);

            AnalyzeM650JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.MultipleJobNames));
            Assert.IsTrue(String.IsNullOrEmpty(analyses.JobName));
        }
    }
}
