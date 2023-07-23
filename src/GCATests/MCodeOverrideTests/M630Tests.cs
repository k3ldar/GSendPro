using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSendAnalyser.Internal;
using GSendAnalyser;
using GSendCommon.MCodeOverrides;
using GSendShared.Models;
using GSendShared;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M630Tests
    {
        [TestMethod]
        public void Process_M622CodeNotFound_Returns_False()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M604");
            analyses.Analyse();
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();
            machineStateModel.Overrides.OverridesDisabled = false;

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };

            M630Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }
    }
}
