using System.Diagnostics.CodeAnalysis;

using GSendCS.Internal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendCS
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class CommandLineOptionsTests
    {
        [TestMethod]
        public void Validate_CommandLineOptions_ReturnsCorrectValues_Success()
        {
            CommandLineOptions sut = new();
            Assert.IsNotNull(sut);
            Assert.IsTrue(sut.ShowVerbosity);
            Assert.IsTrue(sut.ShowHelpMessage);
            Assert.AreEqual("  ", sut.SubOptionPrefix);
            Assert.AreEqual(20, sut.SubOptionMinimumLength);
            Assert.AreEqual("  ", sut.SubOptionSuffix);
            Assert.AreEqual("   ", sut.ParameterPrefix);
            Assert.AreEqual(18, sut.ParameterMinimumLength);
            Assert.AreEqual("  ", sut.ParameterSuffix);
            Assert.AreEqual(22, sut.InternalOptionsMinimumLength);
            Assert.IsTrue(sut.CaseSensitiveOptionNames);
            Assert.IsFalse(sut.CaseSensitiveSubOptionNames);
            Assert.IsFalse(sut.CaseSensitiveParameterNames);
        }
    }
}
