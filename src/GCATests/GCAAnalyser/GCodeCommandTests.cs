using System;
using System.Diagnostics.CodeAnalysis;

using GSendAnalyser;
using GSendAnalyser.Internal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyser
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GCodeCommandTests
    {
        private const string TestCategoryAnalyser = "Analyser";


        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Construct_InvalidInstance_CodeIsNotInRange_Throws()
        {
            new GCodeCommand(2, '#', 0, "0", string.Empty, new CurrentCommandValues(), 1);
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

            GCodeCommand sut = new GCodeCommand(0, 'G', 0, "0", string.Empty, currentCommandValues, 5);

            Assert.IsNotNull(sut);
            Assert.AreEqual("G0", sut.ToString());
            Assert.AreEqual(1.1m, sut.CurrentX);
            Assert.AreEqual(2.2m, sut.CurrentY);
            Assert.AreEqual(3.3m, sut.CurrentZ);
            Assert.AreEqual(5, sut.LineNumber);
        }

        [TestMethod]
        [TestCategory(TestCategoryAnalyser)]
        public void Construct_ValidInstance_MCode_Success()
        {
            GCodeCommand sut = new GCodeCommand(1, 'M', 3, "3", string.Empty, new CurrentCommandValues(), 1);
            Assert.IsNotNull(sut);
            Assert.AreEqual("M3", sut.ToString());
            Assert.AreEqual(0m, sut.CurrentX);
            Assert.AreEqual(0m, sut.CurrentY);
            Assert.AreEqual(0m, sut.CurrentZ);
        }
    }
}