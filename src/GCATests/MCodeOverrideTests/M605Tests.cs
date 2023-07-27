using System.Diagnostics.CodeAnalysis;
using System.Threading;

using GSendAnalyzer;
using GSendAnalyzer.Internal;

using GSendCommon.MCodeOverrides;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shared.Classes;

namespace GSendTests.MCodeOverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class M605Tests
    {
        [TestMethod]
        public void Process_M605CodeNotFound_Returns_False()
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

            M605Override sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Process_M605CodeFound_SoundFileExists_FromVariables_Returns_True()
        {
            ThreadManager.Initialise();
            try
            {
                IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
                GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
                IGCodeAnalyses analyses = gCodeParser.Parse("#100=C:\\Windows\\Media\\\n#101=Alarm01.wav\n#102=notify.wav\nM605 ; [#100#101]");
                analyses.Analyse();
                gCodeLine.Commands.AddRange(analyses.Commands);

                MachineStateModel machineStateModel = new();
                machineStateModel.Overrides.OverridesDisabled = false;

                MockOverrideContext context = new(machineStateModel)
                {
                    GCode = gCodeLine
                };

                M605Override sut = new();
                bool result = sut.Process(context, CancellationToken.None);

                Assert.IsTrue(result);
                Assert.IsFalse(context.SendCommand);
            }
            finally
            {
                ThreadManager.Finalise();
            }
        }

        [TestMethod]
        public void Process_M605CodeFound_SoundFileExists_FromString_Returns_True()
        {
            ThreadManager.Initialise();
            try
            {
                IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
                GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubprograms());
                IGCodeAnalyses analyses = gCodeParser.Parse("M605 ; C:\\Windows\\Media\\Alarm01.wav");
                analyses.Analyse();
                gCodeLine.Commands.AddRange(analyses.Commands);

                MachineStateModel machineStateModel = new();
                machineStateModel.Overrides.OverridesDisabled = false;

                MockOverrideContext context = new(machineStateModel)
                {
                    GCode = gCodeLine
                };

                M605Override sut = new();
                bool result = sut.Process(context, CancellationToken.None);

                Assert.IsTrue(result);
                Assert.IsFalse(context.SendCommand);
            }
            finally
            {
                ThreadManager.Finalise();
            }
        }
    }
}
