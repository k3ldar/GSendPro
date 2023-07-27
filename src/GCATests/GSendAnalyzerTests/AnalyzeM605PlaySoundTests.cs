using System.Diagnostics.CodeAnalysis;

using GSendAnalyzer;
using GSendAnalyzer.Analyzers;
using GSendAnalyzer.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeM605PlaySoundTests
    {
        [TestMethod]
        public void Analyze_M605CodeNotFound_Returns_WithNoErrorsOrWarnings()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M604");

            AnalyzeM605PlaySounds sut = new();
            Assert.IsNotNull(sut);
            Assert.AreEqual(int.MinValue, sut.Order);

            sut.Analyze(null, analyses);

            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_M605CodMultipleItemsFoundOnSameLine_Adds_Error()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M605 M605;");

            AnalyzeM605PlaySounds sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M605 command but no sound file was found, you must specify a sound file.", analyses.Errors[0]);
            Assert.AreEqual("Line 1 contains multiple M605 commands, only 1 M605 command is allowed per line.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_M605Code_SoundFileNotFound_Adds_Warning()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M605;e:\\sounds\\mysound.wav");

            AnalyzeM605PlaySounds sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound));
            Assert.AreEqual(1, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual("Sound file \"e:\\sounds\\mysound.wav\" on line 1 could not be found.", analyses.Warnings[0]);
        }

        [TestMethod]
        public void Process_M605CodeFound_SoundFileExists_ContainsNoErrorAndNoWarnings()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M605;c:\\windows\\media\\alarm01.wav");

            AnalyzeM605PlaySounds sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }
    }
}
