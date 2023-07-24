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
    public class AnalyzeM602JobNameTests
    {
        [TestMethod]
        public void AnalyzeJobName_NoComment_AddsOptionForInvalidJobName()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM602JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName));
        }

        [TestMethod]
        public void AnalyzeJobName_JobNameSpecified_CommentStyle1_CorrectlySetsAnalysesJobName()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602 ;(Invalid Job)\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM602JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName));

            Assert.AreEqual("(Invalid Job)", analyses.JobName);
        }

        [TestMethod]
        public void AnalyzeJobName_JobNameSpecified_CommentStyle2_CorrectlySetsAnalysesJobName()
        {
            string gCodeWithM602NoComment = "G17\nG21\nM602 (Invalid Job)\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeM602JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName));

            Assert.AreEqual("Invalid Job", analyses.JobName);
        }

        [TestMethod]
        public void AnalyzeJobName_MultipleJobNameSpecified_AddsOptionForMultipleJobNames()
        {
            string gCodeWithM602NoComment = "M602M602\nG17\nG21\nM602 ;(Invalid Job)\nG0Z40.000\n";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM602NoComment);

            Assert.AreEqual(7, analyses.Commands.Count);

            AnalyzeM602JobName sut = new();
            sut.Analyze("", analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.MultipleJobNames));
            Assert.IsTrue(String.IsNullOrEmpty(analyses.JobName));
        }
    }
}
