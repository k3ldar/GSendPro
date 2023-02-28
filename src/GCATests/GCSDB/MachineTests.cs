using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GSendShared.Models;
using GSendShared;

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
            new MachineModel(1, null, GSendShared.MachineType.Printer, "COM5", MachineOptions.None, 3, new Dictionary<uint, decimal>());
        }
    }
}
