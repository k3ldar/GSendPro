using System;

using GCAAnalyser;
using GCAAnalyser.Internal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GCATests.Analyzer
{
    [TestClass]
    public class GCodeCommandTests
    {
        private const string TestCategoryAnalyser = "Analyser";


        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Construct_InvalidInstance_CodeIsNotInRange_Throws()
        {
            new GCodeCommand(2, '#', 0, "0", String.Empty, new CurrentCommandValues());
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_Success()
        {
            CurrentCommandValues currentCommandValues = new CurrentCommandValues()
            {
                X = 1.1m,
                Y = 2.2m,
                Z = 3.3m,
            };

            GCodeCommand sut = new GCodeCommand(0, 'G', 0, "0", String.Empty, currentCommandValues);

            Assert.IsNotNull(sut);
            Assert.AreEqual("G0", sut.ToString());
            Assert.AreEqual(1.1m, sut.X);
            Assert.AreEqual(2.2m, sut.Y);
            Assert.AreEqual(3.3m, sut.Z);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_MCode_Success()
        {
            GCodeCommand sut = new GCodeCommand(1, 'M', 3, "3", String.Empty, new CurrentCommandValues());

            Assert.IsNotNull(sut);
            Assert.AreEqual("M3", sut.ToString());
            Assert.AreEqual(0m, sut.X);
            Assert.AreEqual(0m, sut.Y);
            Assert.AreEqual(0m, sut.Z);
        }
    }
}