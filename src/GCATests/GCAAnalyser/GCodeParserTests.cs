using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

using GSendAnalyser;
using GSendAnalyser.Abstractions;
using GSendAnalyser.Internal;

using GSendShared;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyser
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
            Assert.IsFalse(string.IsNullOrEmpty(analyses.Commands[10].Comment));
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
        public void ParseLocalFile_FromSingleTextBlock_Success()
        {
            string finish = Encoding.UTF8.GetString(Properties.Resources.test_toolpath2_Machine_Relief_Ball_Nose_3_175_mm_Roughing_Roughing);
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(2260, analyses.Commands.Count);
            Assert.IsFalse(analyses.ContainsDuplicates);

            List<IGCodeCommand> movesToSafeZ = analyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.HomeZ)).ToList();
            Assert.AreEqual(2, movesToSafeZ.Count);
            Assert.AreEqual(9765310005, analyses.TotalTime.Ticks);
            Assert.AreEqual(14758.7094m, analyses.TotalDistance);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Parse_SingleOutG1Commands_SetFeedRate_MarkFeedRateOnlyCommandsAsNoneCommand_Success()
        {
            string finish = "G0Z51.800\r\nG0X0.000Y0.000S8000M3\r\nG0X-14.414Y-109.500Z32.000\r\nG1Z17.109F400.0\r\nG1X-14.300F2000.0\r\nX18.134\r\n" + 
                "X18.951Y-109.400\r\nX-15.424\r\nX-16.434Y-109.300\r\nX19.767\r\nX20.584Y-109.200\r\nX7.403Y109.396\r\nX4.767\r\nX6.442Y109.496\r\n" + 
                "X6.544\r\nG0Z32.000\r\nG0X0.000Y0.000Z51.800\r\nG0Z51.800\r\nG0X0Y0\r\nM30\r\n";
            GCodeParser sut = new();
            IGCodeAnalyses analyses = sut.Parse(finish);
            analyses.Analyse();
            Assert.AreEqual(44, analyses.Commands.Count);
            Assert.IsFalse(analyses.ContainsDuplicates);

            List<IGCodeCommand> movesToSafeZ = analyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.HomeZ)).ToList();
            Assert.AreEqual(2, movesToSafeZ.Count);
            Assert.AreEqual(9765310005, analyses.TotalTime.Ticks);
            Assert.AreEqual(14758.7094m, analyses.TotalDistance);
        }
    }
}
