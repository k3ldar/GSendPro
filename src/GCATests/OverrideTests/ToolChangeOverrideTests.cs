using System.Diagnostics.CodeAnalysis;
using System.Threading;

using GSendAnalyser;
using GSendAnalyser.Internal;

using GSendCommon.OverrideClasses;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.OverrideTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ToolChangeOverrideTests
    {
        [TestMethod]
        public void ToolChangerNotAvailable_RemovesCodeFromSending_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = gCodeParser.Parse("T1M6");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };


            ToolChangeOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
        }

        [TestMethod]
        public void ToolChangerAvailable_M6_AllowsSending_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockGSendApiWrapper());
            IGCodeAnalyses analyses = gCodeParser.Parse("M6");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();

            MockOverrideContext context = new(machineStateModel)
            {
                GCode = gCodeLine
            };
            context.Machine.AddOptions(MachineOptions.ToolChanger);

            ToolChangeOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.IsTrue(context.SendCommand);
        }
    }
}
