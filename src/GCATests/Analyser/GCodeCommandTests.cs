using System;

using GCAAnalyser;

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
            //new GCodeCommand("Y0", 1.1m, 2.2m, 3.3m);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_Success()
        {
            //GCodeCommand sut = new GCodeCommand("G0", 1.1m, 2.2m, 3.3m);

            //Assert.IsNotNull(sut);
            //Assert.AreEqual("G0", sut.Code);
            //Assert.AreEqual(1.1m, sut.X);
            //Assert.AreEqual(2.2m, sut.Y);
            //Assert.AreEqual(3.3m, sut.Z);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_MCode_Success()
        {
            //GCodeCommand sut = new GCodeCommand("M3", 10000);

            //Assert.IsNotNull(sut);
            //Assert.AreEqual("G0", sut.Code);
            //Assert.AreEqual(0m, sut.X);
            //Assert.AreEqual(0m, sut.Y);
            //Assert.AreEqual(0m, sut.Z);
            //Assert.AreEqual(0, sut.Speed);
        }
    }
}