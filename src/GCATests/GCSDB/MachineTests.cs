using System;
using System.Diagnostics.CodeAnalysis;

using GSendShared;
using GSendShared.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GCATests.GSendDB
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MachineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_InvalidParam_NameNull_Throws_ArgumentNullException()
        {
            new MachineModel(1, null, GSendShared.MachineType.Printer, MachineFirmware.grblv1_1, "COM5", MachineOptions.None, 3, new GrblSettings(),
                FeedRateDisplayUnits.MmPerMinute, FeedbackUnit.Mm, 50, 0, 10, 10, DateTime.UtcNow, 1, String.Empty, 10, 20, 10, 10, SpindleType.Integrated,
                0, 0, 0);
        }
    }
}
