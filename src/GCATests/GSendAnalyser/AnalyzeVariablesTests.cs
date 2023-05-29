using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendAnalyser.Analysers;
using GSendAnalyser.Internal;
using GSendShared;
using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendAnalyserTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AnalyzeVariablesTests
    {
        [TestMethod]
        public void VariableNotDeclared_AddsError_Success()
        {
            string gCodeWithM650NoComment = "M650 [#321]";
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new AnalyzeVariables();
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is referenced on line 1 but has not been declared.", analyses.Errors[0]);
        }

        [TestMethod]
        public void VariableDeclaredAfterFirstUsage_AddsError_Success()
        {
            string gCodeWithM650NoComment = "M650 [#321]\n#321=a value";
            GCodeParser gCodeParser = new(new MockPluginClassesService());
            IGCodeAnalyses analyses = gCodeParser.Parse(gCodeWithM650NoComment);

            Assert.AreEqual(1, analyses.Commands.Count);

            AnalyzeVariables sut = new AnalyzeVariables();
            sut.Analyze("", analyses);

            Assert.AreEqual(1, analyses.Errors.Count);
            Assert.AreEqual("Variable #321 is used on line 1 but not declared until line 2, variable must be declared before use.", analyses.Errors[0]);
        }



        // variable declared in sub program
        // variable declared after usage
        // variable declared but not used
    }
}
