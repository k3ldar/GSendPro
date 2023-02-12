using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GCAAnalyser;
using GCAAnalyser.Abstractions;
using GCAAnalyser.Internal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GCATests.Analyser
{
    [TestClass]
    public class GCodeParserTests
    {
        private const string TestCategoryAnalyser = "Analyser";

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_Success()
        {
            GCodeParser sut = new();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseZProbeCommand()
        {
            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG0X139.948Y37.136Z40.000";
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(ZProbeCommand);

            Assert.AreEqual(13, analyses.Commands.Count);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile_Start()
        {
            string finish = "G17\nG21\nG0Z40.000\nG0X0.000Y0.000S8000M3\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(13, analyses.Commands.Count);

        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalWithComments()
        {
            string finish = "G17\nG21;second\nG0Z40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(16, analyses.Commands.Count);
            Assert.IsFalse(String.IsNullOrEmpty(analyses.Commands[10].Comment));
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Parse_WithDuplicates_DuplicatesFound()
        {
            string finish = "G17\nG21;second\nG0Z40.000\nZ40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(17, analyses.Commands.Count);
            Assert.IsTrue(analyses.ContainsDuplicates);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile()
        {
            string finish = Encoding.UTF8.GetString(Properties.Resources.test_toolpath2_Machine_Relief_Ball_Nose_3_175_mm_Roughing_Roughing);
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(2260, analyses.Commands.Count);
            Assert.IsFalse(analyses.ContainsDuplicates);

            List<GCodeCommand> movesToSafeZ = analyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.HomeZ)).ToList();
            Assert.AreEqual(2, movesToSafeZ.Count);
            Assert.AreEqual(9765310005, analyses.TotalTime.Ticks);
            Assert.AreEqual(14758.7094m, analyses.TotalDistance);
        }
    }
}
