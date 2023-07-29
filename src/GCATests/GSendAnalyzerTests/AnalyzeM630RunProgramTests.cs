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
    public class AnalyzeM630RunProgramTests
    {
        [TestMethod]
        public void Analyze_M630CodeNotFound_Returns_WithNoErrorsOrWarnings()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M60");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);
            Assert.AreEqual(int.MinValue, sut.Order);

            sut.Analyze(null, analyses);

            Assert.IsFalse(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }

        [TestMethod]
        public void Analyze_M630_MultipleItemsFoundOnSameLine_Adds_Error()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630 M630;");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains M630 command but no executable file was found, you must specify an executable file.", analyses.Errors[0]);
            Assert.AreEqual("Line 1 contains multiple M630 commands, only 1 M630 command is allowed per line.", analyses.Errors[1]);
        }

        [TestMethod]
        public void Analyze_M630Code_EXE_ExecutableFileNotFound_Adds_Warning()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;e:\\tools\\myfile.exe");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(1, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual("Invalid M630 on line 1 executable not found: e:\\tools\\myfile.exe", analyses.Warnings[0]);
        }

        [TestMethod]
        public void Analyze_M630Code_COM_ExecutableFileNotFound_Adds_Warning()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;e:\\tools\\myfile.com");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(1, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual("Invalid M630 on line 1 executable not found: e:\\tools\\myfile.com", analyses.Warnings[0]);
        }

        [TestMethod]
        public void Analyze_M630Code_BAT_ExecutableFileNotFound_Adds_Warning()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;e:\\tools\\myfile.bat");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(1, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual("Invalid M630 on line 1 executable not found: e:\\tools\\myfile.bat", analyses.Warnings[0]);
        }

        [TestMethod]
        public void Analyze_M630Code_DAT_ExecutableFileNotFound_Adds_Warning()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;e:\\tools\\myfile.dat");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("The only file extensions valid for running a program are .exe, .com and .bat", analyses.Errors[0]);
        }

        [TestMethod]
        public void Process_M630CodeFound_ExecutableFileExists_ContainsNoErrorAndNoWarnings()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M630;c:\\windows\\explorer.exe");

            AnalyzeM630RunProgram sut = new();
            Assert.IsNotNull(sut);

            sut.Analyze(null, analyses);

            Assert.IsTrue(analyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram));
            Assert.AreEqual(0, analyses.Warnings.Count);
            Assert.AreEqual(0, analyses.Errors.Count);
        }
    }
}
