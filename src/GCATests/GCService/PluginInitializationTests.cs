using System.Diagnostics.CodeAnalysis;

using GSendService;

using GSendTests.Mocks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PluginInitializationTests
    {
        private const string TestsCategory = "Initialization Tests";

        [TestMethod]
        [TestCategory(TestsCategory)]
        public void ExtendsIPluginAndIInitialiseEvents()
        {
            PluginInitialization sut = new PluginInitialization();

            Assert.IsInstanceOfType(sut, typeof(IPlugin));
            Assert.IsInstanceOfType(sut, typeof(IInitialiseEvents));
        }

        [TestMethod]
        [TestCategory(TestsCategory)]
        public void GetVersion_ReturnsCurrentVersion_Success()
        {
            PluginInitialization sut = new PluginInitialization();

            Assert.AreEqual((ushort)1, sut.GetVersion());
        }

        [TestMethod]
        [TestCategory(TestsCategory)]
        public void Initialize_DoesNotAddItemsToLogger()
        {
            PluginInitialization sut = new PluginInitialization();
            MockLogger testLogger = new MockLogger();

            sut.Initialise(testLogger);

            Assert.AreEqual(0, testLogger.LogItems.Count);
        }

        [TestMethod]
        [TestCategory(TestsCategory)]
        public void Finalise_DoesNotThrowException()
        {
            PluginInitialization sut = new PluginInitialization();
            Assert.IsNotNull(sut);

            sut.Finalise();
        }
    }
}
