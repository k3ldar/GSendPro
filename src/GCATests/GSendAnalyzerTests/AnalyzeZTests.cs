using System;
using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer.Analyzers;
using GSendAnalyzer.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyzerTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public sealed class AnalyzeZTests
    {
        private const string GCodeZTop = "G0Z30.000\r\nG1Z-2.000F250.0\r\nG1Z-4.000F250.0\r\nG1Z-6.000F250.0\r\nG1Z-8.000F250.0\r\nG1Z-10.000F250.0\r\nG1Z-12.000F250.0\r\nG1Z-14.000F250.0\r\nG1Z-16.000F250.0\r\nG1Z-18.000F250.0\r\nG0Z22.000";
        private const string GCodeZBottom = "G0Z30.000\r\nG1Z14.300F250.0\r\nG1Z12.300F250.0\r\nG1Z10.300F250.0\r\nG1Z8.300F250.0\r\nG1Z6.300F250.0\r\nG1Z4.300F250.0\r\nG1Z2.300F250.0\r\nG1Z0.300F250.0\r\nG1Z-1.700F250.0";
        private const string GCodeZUndetermined = "G0Z30.000\r\nG1Z-2.000F250.0\r\nG1Z-4.000F250.0";

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            AnalyzeZ sut = new();
            Assert.IsNotNull(sut);
            Assert.AreEqual(Int32.MinValue, sut.Order);
        }

        [TestMethod]
        public void Validate_ZTop_Success()
        {
            AnalyzeZ sut = new();
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(GCodeZTop);

            sut.Analyze(String.Empty, analyses);

            Assert.IsFalse(analyses.ZBottom);
        }

        [TestMethod]
        public void Validate_ZBottom_Success()
        {
            AnalyzeZ sut = new();
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(GCodeZBottom);

            sut.Analyze(String.Empty, analyses);

            Assert.IsTrue(analyses.ZBottom);
        }

        [TestMethod]
        public void Validate_ZUndetermined_Success()
        {
            AnalyzeZ sut = new();
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(GCodeZUndetermined);

            sut.Analyze(String.Empty, analyses);

            Assert.IsNull(analyses.ZBottom);
        }

        [TestMethod]
        public void Validate_ZBottom_GoesBelowBed_CreatesWarning_Success()
        {
            AnalyzeZ sut = new();
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse(GCodeZBottom);

            sut.Analyze(String.Empty, analyses);

            Assert.IsTrue(analyses.ZBottom);
            Assert.AreEqual(1, analyses.Warnings.Count);
            Assert.AreEqual("Validate the bit does not go below the spoilboard when using Z Bottom and having a negative Z value", analyses.Warnings[0]);
        }
    }
}
