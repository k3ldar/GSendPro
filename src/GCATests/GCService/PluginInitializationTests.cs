using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PluginInitializationTests
    {
        //private const string TestsCategory = "Initialization Tests";

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void ExtendsIPluginAndIInitialiseEvents()
        //{
        //    PluginInitialization sut = new PluginInitialization();

        //    Assert.IsInstanceOfType(sut, typeof(IPlugin));
        //    Assert.IsInstanceOfType(sut, typeof(IInitialiseEvents));
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void GetVersion_ReturnsCurrentVersion_Success()
        //{
        //    PluginInitialization sut = new PluginInitialization();

        //    Assert.AreEqual((ushort)1, sut.GetVersion());
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void Initialize_DoesNotAddItemsToLogger()
        //{
        //    PluginInitialization sut = new PluginInitialization();
        //    MockLogger testLogger = new MockLogger();

        //    sut.Initialise(testLogger);

        //    Assert.AreEqual(0, testLogger.Logs.Count);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void AfterConfigure_DoesNotConfigurePipeline_Success()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();

        //    sut.AfterConfigure(testApplicationBuilder);

        //    Assert.IsFalse(testApplicationBuilder.UseCalled);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void Configure_DoesNotConfigurePipeline_Success()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();

        //    sut.Configure(testApplicationBuilder);

        //    Assert.IsFalse(testApplicationBuilder.UseCalled);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void BeforeConfigure_DoesRegisterApplicationServices()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();

        //    sut.BeforeConfigure(testApplicationBuilder);

        //    Assert.IsTrue(testApplicationBuilder.UseCalled);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void Configure_DoesNotRegisterApplicationServices()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();

        //    sut.Configure(testApplicationBuilder);

        //    Assert.IsFalse(testApplicationBuilder.UseCalled);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void Finalise_DoesNotThrowException()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();
        //    Assert.IsNotNull(sut);

        //    sut.Finalise();
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void BeforeConfigureServices_DoesNotThrowException()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();
        //    MockServiceCollection mockServiceCollection = new MockServiceCollection();

        //    sut.BeforeConfigureServices(mockServiceCollection);

        //    Assert.AreEqual(0, mockServiceCollection.ServicesRegistered);
        //}

        //[TestMethod]
        //[TestCategory(TestsCategory)]
        //public void ConfigureServices_DoesNotThrowException()
        //{
        //    MockApplicationBuilder testApplicationBuilder = new MockApplicationBuilder();
        //    PluginInitialization sut = new PluginInitialization();
        //    MockServiceCollection mockServiceCollection = new MockServiceCollection();

        //    sut.ConfigureServices(mockServiceCollection);

        //    Assert.AreEqual(0, mockServiceCollection.ServicesRegistered);
        //}
    }
}
