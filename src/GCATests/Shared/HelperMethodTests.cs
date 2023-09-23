using System.Diagnostics.CodeAnalysis;

using GSendShared;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Shared
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HelperMethodTests
    {
        [TestMethod]
        public void ReduceValueByPercentage_30Percent_ReturnsCorrectValue_Success()
        {
            decimal newValue = HelperMethods.ReduceValueByPercentage(100, 130, 30);
            Assert.AreEqual(70m, newValue);
        }

        [TestMethod]
        public void ReduceValueByPercentage_12Percent_ReturnsCorrectValue_Success()
        {
            decimal newValue = HelperMethods.ReduceValueByPercentage(156, 1, 13);
            Assert.AreEqual(135.72m, newValue);
        }

        [TestMethod]
        public void ReduceValueByPercentage_12PercentSameValue_ReturnsCorrectValue_Success()
        {
            decimal newValue = HelperMethods.ReduceValueByPercentage(156, 156, 13);
            Assert.AreEqual(156m, newValue);
        }

        [TestMethod]
        public void FormatSpeedValue_ReturnsCorrectValue_Success()
        {
            string formatted = HelperMethods.FormatSpeedValue(126.8736m);
            Assert.AreEqual("126.87 mm/min", formatted);
        }

        [TestMethod]
        public void FormatSpeed_ReturnsCorrectValue_Success()
        {
            string formatted = HelperMethods.FormatSpeed(126.8736m);
            Assert.AreEqual("126.87", formatted);
        }

        [TestMethod]
        public void FormatPercentDiff_Val1Zero_ReturnsZero()
        {
            string formatted = HelperMethods.FormatPercent(0, 123m);
            Assert.AreEqual("0%", formatted);
        }

        [TestMethod]
        public void FormatPercentDiff_Val2Zero_ReturnsZero()
        {
            string formatted = HelperMethods.FormatPercent(2013475.4m, 0);
            Assert.AreEqual("0%", formatted);
        }

        [TestMethod]
        public void FormatPercentDiff_ReturnsCorrectValue_Success()
        {
            string formatted = HelperMethods.FormatPercent(2000, 3000);
            Assert.AreEqual("50.00%", formatted);
        }

        [TestMethod]
        public void FormatPercentDiff_MinusValue_ReturnsCorrectValue_Success()
        {
            string formatted = HelperMethods.FormatPercent(500, 450);
            Assert.AreEqual("-10.00%", formatted);
        }

        [TestMethod]
        public void TranslateRapidOverride_Low_Success()
        {
            string value = HelperMethods.TranslateRapidOverride(RapidsOverride.Low);
            Assert.AreEqual("Low", value);
        }

        [TestMethod]
        public void TranslateRapidOverride_Medium_Success()
        {
            string value = HelperMethods.TranslateRapidOverride(RapidsOverride.Medium);
            Assert.AreEqual("Medium", value);
        }

        [TestMethod]
        public void TranslateRapidOverride_High_Success()
        {
            string value = HelperMethods.TranslateRapidOverride(RapidsOverride.High);
            Assert.AreEqual("High", value);
        }
    }
}
