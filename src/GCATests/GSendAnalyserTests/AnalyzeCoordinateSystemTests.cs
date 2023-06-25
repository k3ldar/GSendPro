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
    public class AnalyzeCoordinateSystemTests
    {
        [TestMethod]
        public void Analyze_CoordinatesUsed_RetrievesCorrectList_success()
        {
            string gCodeWithVariables = "G54\r\nG0X50 ; V Carve heart\r\nG55 ; move to second position/piece\r\nG0X50 ; repeat heart\r\nG56 ; move to third position\r\nG0X50 ; Repeat heart\r\nG57 ; move to fourth position\r\nG0X50 ; repeat heart\r\nG58 ; move to fifth position\r\nG0X50 ; repeat heart\r\nG59 ; move \r\nG0X50 ; repeat heart";

            GCodeParser subprograms = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = subprograms.Parse(gCodeWithVariables);

            Assert.AreEqual(18, analyses.Commands.Count);


            Assert.IsTrue(analyses.Commands[0].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));
            Assert.IsTrue(analyses.Commands[3].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));
            Assert.IsTrue(analyses.Commands[6].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));
            Assert.IsTrue(analyses.Commands[9].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));
            Assert.IsTrue(analyses.Commands[12].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));
            Assert.IsTrue(analyses.Commands[15].Attributes.HasFlag(CommandAttributes.ChangeCoordinates));

            AnalyzeCoordinateSystemsUsed sut = new();
            sut.Analyze("", analyses);

            Assert.IsNull(analyses.Commands[0].SubAnalyses);
            Assert.AreEqual(0, analyses.Errors.Count);
            Assert.AreEqual("G54,G55,G56,G57,G58,G59", analyses.CoordinateSystems);  
        }
    }
}
