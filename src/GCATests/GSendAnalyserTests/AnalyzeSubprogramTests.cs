using System.Diagnostics.CodeAnalysis;

using GSendAnalyser.Analysers;
using GSendAnalyser.Internal;

using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeSubprogramTests
    {
        [TestMethod]
        public void AnalyzeSubProgram_ContainsMultipleSubprogramsPerLine_CreatesError()
        {
            string gCode = "O1000O1001\nO1002\nO1003O1004";
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCode);

            Assert.AreEqual(5, analyses.Commands.Count);

            AnalyzeSubPrograms sut = new();
            sut.Analyze("", analyses);

            Assert.AreEqual(5, analyses.SubProgramCount);
            Assert.AreEqual(2, analyses.Errors.Count);
            Assert.AreEqual("Line 1 contains 2 or more subprograms, subprograms must be on a unique line.", analyses.Errors[0]);
            Assert.AreEqual("Line 3 contains 2 or more subprograms, subprograms must be on a unique line.", analyses.Errors[1]);
        }
    }
}
