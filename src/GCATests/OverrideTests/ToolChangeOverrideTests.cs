using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GSendAnalyser.Internal;
using GSendAnalyser;
using GSendCommon.OverrideClasses;
using GSendShared.Models;
using GSendShared;

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
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("T1M6");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;
            

            ToolChangeOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsFalse(context.SendCommand);
        }

        [TestMethod]
        public void ToolChangerAvailable_M6_AllowsSending_True()
        {
            IGCodeLine gCodeLine = new GCodeLine(new MockGCodeAnalyses());
            GCodeParser gCodeParser = new(new MockPluginClassesService(), new MockSubPrograms());
            IGCodeAnalyses analyses = gCodeParser.Parse("M6");
            gCodeLine.Commands.AddRange(analyses.Commands);

            MachineStateModel machineStateModel = new();

            MockOverrideContext context = new(machineStateModel);
            context.GCode = gCodeLine;
            context.Machine.AddOptions(MachineOptions.ToolChanger);

            ToolChangeOverride sut = new();
            bool result = sut.Process(context, CancellationToken.None);

            Assert.IsFalse(result);
            Assert.IsTrue(context.SendCommand);
        }
    }
}
