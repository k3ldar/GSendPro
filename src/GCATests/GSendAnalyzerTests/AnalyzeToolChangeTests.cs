﻿using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer.Analyzers;
using GSendAnalyzer.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeToolChangeTests
    {
        [TestMethod]
        public void Analyze_M6WithoutToolSpecified_AddsError()
        {
            string gCodeWithVariables = "M6\nG17\nG21\nG90\nG0Z51.8000\nG0X0.0000Y0.0000";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(9, analyses.Commands.Count);

            AnalyzeToolChange sut = new();
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("M6 Automatic tool change found, but no tool specified.", analyses.Errors[0]);
        }
    }
}
