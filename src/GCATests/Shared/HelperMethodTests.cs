using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

using GSendShared;
using GSendShared.Plugins;

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

        [TestMethod]
        public void FormatAccelerationValue_ReturnsCorrectFormattedValue_Success()
        {
            string value = HelperMethods.FormatAccelerationValue(523.456789m);
            Assert.AreEqual("523.46 mm/sec²", value);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void LoadPluginSettings_FileNotFound_ReturnsEmptyClass()
        {
            List<GSendPluginSettings> sut = HelperMethods.LoadPluginSettings(Path.GetTempFileName());
            Assert.IsNotNull(sut);
            Assert.AreEqual(0, sut.Count);
        }

        [TestMethod]
        public void TimeSpanToTime_ZeroTimeSpan_Returns_NoTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.Zero);
            Assert.AreEqual("-", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_OneDayTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromHours(24));
            Assert.AreEqual("1d 0h 0m 0s", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_OneDayTwoHoursTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromHours(25));
            Assert.AreEqual("1d 1h 0m 0s", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_OneHourTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromMinutes(60));
            Assert.AreEqual("1h 0m 0s", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_OneHourFiveMinsTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromMinutes(65));
            Assert.AreEqual("1h 5m 0s", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_ThirtyEightMinutesTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromMinutes(38));
            Assert.AreEqual("38m 0s", sut);
        }

        [TestMethod]
        public void TimeSpanToTime_FourtyFiveSecondsTimeSpan_Returns_FormattedTime()
        {
            string sut = HelperMethods.TimeSpanToTime(TimeSpan.FromSeconds(45));
            Assert.AreEqual("45s", sut);
        }

        [TestMethod]
        public void ConvertMeasurementForDisplay_MM_ReturnsCorrectFormattedValue()
        {
            string sut = HelperMethods.ConvertMeasurementForDisplay(FeedbackUnit.Mm, 415.6134521d);
            Assert.AreEqual("415.6135", sut);
        }

        [TestMethod]
        public void ConvertMeasurementForDisplay_Inch_ReturnsCorrectFormattedValue()
        {
            string sut = HelperMethods.ConvertMeasurementForDisplay(FeedbackUnit.Inch, 415.6134521d);
            Assert.AreEqual("16.36273", sut);
        }

        [TestMethod]
        public void ConvertFeedRateForDisplay_MmPerMin_ReturnsFormattedText()
        {
            string sut = HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplayUnits.MmPerMinute, 2000);
            Assert.AreEqual("2,000", sut);
        }

        [TestMethod]
        public void ConvertFeedRateForDisplay_MmPerSec_ReturnsFormattedText()
        {
            string sut = HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplayUnits.MmPerSecond, 2000);
            Assert.AreEqual("33", sut);
        }

        [TestMethod]
        public void ConvertFeedRateForDisplay_InchPerSec_ReturnsFormattedText()
        {
            string sut = HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplayUnits.InchPerSecond, 2000);
            Assert.AreEqual("1.31234", sut);
        }

        [TestMethod]
        public void ConvertFeedRateForDisplay_InchPerMin_ReturnsFormattedText()
        {
            string sut = HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplayUnits.InchPerMinute, 2000);
            Assert.AreEqual("78.74016", sut);
        }
    }
}
