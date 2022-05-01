using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GCodeParser sut = new GCodeParser();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseZProbeCommand()
        {
            const string ZProbeCommand = "G17G21G0Z40.000 G0X0.000Y0.000S8000M3\tG0X139.948Y37.136Z40.000";
            GCodeParser sut = new GCodeParser();
            IGCodeAnalyses analyses = sut.Parse(ZProbeCommand);

            Assert.AreEqual(5, analyses.Commands.Count);

            Assert.AreEqual(17, analyses.Commands[0].Code);
            Assert.AreEqual(0, analyses.Commands[0].Speed);
            Assert.AreEqual(0, analyses.Commands[0].FeedRate);
            Assert.AreEqual(0, analyses.Commands[0].M);
            Assert.AreEqual(0, analyses.Commands[0].Y);
            Assert.AreEqual(0, analyses.Commands[0].X);
            Assert.AreEqual(0, analyses.Commands[0].Z);

            Assert.AreEqual(21, analyses.Commands[1].Code);
            Assert.AreEqual(0, analyses.Commands[1].Speed);
            Assert.AreEqual(0, analyses.Commands[1].FeedRate);
            Assert.AreEqual(0, analyses.Commands[1].M);
            Assert.AreEqual(0, analyses.Commands[1].Y);
            Assert.AreEqual(0, analyses.Commands[1].X);
            Assert.AreEqual(0, analyses.Commands[1].Z);

            Assert.AreEqual(0, analyses.Commands[2].Code);
            Assert.AreEqual(0, analyses.Commands[2].Speed);
            Assert.AreEqual(0, analyses.Commands[2].FeedRate);
            Assert.AreEqual(0, analyses.Commands[2].M);
            Assert.AreEqual(0, analyses.Commands[2].Y);
            Assert.AreEqual(0, analyses.Commands[2].X);
            Assert.AreEqual(40.00m, analyses.Commands[2].Z);

            Assert.AreEqual(0, analyses.Commands[3].Code);
            Assert.AreEqual(8000, analyses.Commands[3].Speed);
            Assert.AreEqual(0, analyses.Commands[3].FeedRate);
            Assert.AreEqual(3, analyses.Commands[3].M);
            Assert.AreEqual(0, analyses.Commands[3].Y);
            Assert.AreEqual(0, analyses.Commands[3].X);
            Assert.AreEqual(0, analyses.Commands[3].Z);

            Assert.AreEqual(0, analyses.Commands[4].Code);
            Assert.AreEqual(0, analyses.Commands[4].Speed);
            Assert.AreEqual(0, analyses.Commands[4].FeedRate);
            Assert.AreEqual(0, analyses.Commands[4].M);
            Assert.AreEqual(37.136m, analyses.Commands[4].Y);
            Assert.AreEqual(139.948m, analyses.Commands[4].X);
            Assert.AreEqual(40, analyses.Commands[4].Z);

        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile_Start()
        {
            string finish = "G17\nG21\nG0Z40.000\nG0X0.000Y0.000S8000M3\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new GCodeParser();
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(5, analyses.Commands.Count);

            Assert.AreEqual(17, analyses.Commands[0].Code);
            Assert.AreEqual(0, analyses.Commands[0].Speed);
            Assert.AreEqual(0, analyses.Commands[0].FeedRate);
            Assert.AreEqual(0, analyses.Commands[0].M);
            Assert.AreEqual(0, analyses.Commands[0].Y);
            Assert.AreEqual(0, analyses.Commands[0].X);
            Assert.AreEqual(0, analyses.Commands[0].Z);

            Assert.AreEqual(21, analyses.Commands[1].Code);
            Assert.AreEqual(0, analyses.Commands[1].Speed);
            Assert.AreEqual(0, analyses.Commands[1].FeedRate);
            Assert.AreEqual(0, analyses.Commands[1].M);
            Assert.AreEqual(0, analyses.Commands[1].Y);
            Assert.AreEqual(0, analyses.Commands[1].X);
            Assert.AreEqual(0, analyses.Commands[1].Z);

            Assert.AreEqual(0, analyses.Commands[2].Code);
            Assert.AreEqual(0, analyses.Commands[2].Speed);
            Assert.AreEqual(0, analyses.Commands[2].FeedRate);
            Assert.AreEqual(0, analyses.Commands[2].M);
            Assert.AreEqual(0, analyses.Commands[2].Y);
            Assert.AreEqual(0, analyses.Commands[2].X);
            Assert.AreEqual(40.00m, analyses.Commands[2].Z);

            Assert.AreEqual(0, analyses.Commands[3].Code);
            Assert.AreEqual(8000, analyses.Commands[3].Speed);
            Assert.AreEqual(0, analyses.Commands[3].FeedRate);
            Assert.AreEqual(3, analyses.Commands[3].M);
            Assert.AreEqual(0, analyses.Commands[3].Y);
            Assert.AreEqual(0, analyses.Commands[3].X);
            Assert.AreEqual(0, analyses.Commands[3].Z);

            Assert.AreEqual(0, analyses.Commands[4].Code);
            Assert.AreEqual(0, analyses.Commands[4].Speed);
            Assert.AreEqual(0, analyses.Commands[4].FeedRate);
            Assert.AreEqual(0, analyses.Commands[4].M);
            Assert.AreEqual(37.136m, analyses.Commands[4].Y);
            Assert.AreEqual(139.948m, analyses.Commands[4].X);
            Assert.AreEqual(40, analyses.Commands[4].Z);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalWithComments()
        {
            string finish = "G17\nG21;second\nG0Z40.000\nG0X0.000Y0.000S8000M3(fourth; with colon)\nG0X139.948Y37.136Z40.000\n";
            GCodeParser sut = new GCodeParser();
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(5, analyses.Commands.Count);

            Assert.AreEqual(17, analyses.Commands[0].Code);
            Assert.AreEqual(0, analyses.Commands[0].Speed);
            Assert.AreEqual(0, analyses.Commands[0].FeedRate);
            Assert.AreEqual(0, analyses.Commands[0].M);
            Assert.AreEqual(0, analyses.Commands[0].Y);
            Assert.AreEqual(0, analyses.Commands[0].X);
            Assert.AreEqual(0, analyses.Commands[0].Z);

            Assert.AreEqual(21, analyses.Commands[1].Code);
            Assert.AreEqual(0, analyses.Commands[1].Speed);
            Assert.AreEqual(0, analyses.Commands[1].FeedRate);
            Assert.AreEqual(0, analyses.Commands[1].M);
            Assert.AreEqual(0, analyses.Commands[1].Y);
            Assert.AreEqual(0, analyses.Commands[1].X);
            Assert.AreEqual(0, analyses.Commands[1].Z);
            Assert.AreEqual("second", analyses.Commands[1].Comment);

            Assert.AreEqual(0, analyses.Commands[2].Code);
            Assert.AreEqual(0, analyses.Commands[2].Speed);
            Assert.AreEqual(0, analyses.Commands[2].FeedRate);
            Assert.AreEqual(0, analyses.Commands[2].M);
            Assert.AreEqual(0, analyses.Commands[2].Y);
            Assert.AreEqual(0, analyses.Commands[2].X);
            Assert.AreEqual(40.00m, analyses.Commands[2].Z);

            Assert.AreEqual(0, analyses.Commands[3].Code);
            Assert.AreEqual(8000, analyses.Commands[3].Speed);
            Assert.AreEqual(0, analyses.Commands[3].FeedRate);
            Assert.AreEqual(3, analyses.Commands[3].M);
            Assert.AreEqual(0, analyses.Commands[3].Y);
            Assert.AreEqual(0, analyses.Commands[3].X);
            Assert.AreEqual(0, analyses.Commands[3].Z);
            Assert.AreEqual("fourth; with colon", analyses.Commands[3].Comment);

            Assert.AreEqual(0, analyses.Commands[4].Code);
            Assert.AreEqual(0, analyses.Commands[4].Speed);
            Assert.AreEqual(0, analyses.Commands[4].FeedRate);
            Assert.AreEqual(0, analyses.Commands[4].M);
            Assert.AreEqual(37.136m, analyses.Commands[4].Y);
            Assert.AreEqual(139.948m, analyses.Commands[4].X);
            Assert.AreEqual(40, analyses.Commands[4].Z);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void ParseLocalFile()
        {
            string finish = System.IO.File.ReadAllText(@"C:\Carveco Projects\Candle Files\Birch Bird_toolpath2_3d relief_Ball Nose 0.75 Finishing.tap");
            GCodeParser sut = new GCodeParser();
            IGCodeAnalyses analyses = sut.Parse(finish);

            Assert.AreEqual(165617, analyses.Commands.Count);
        }
    }
}
