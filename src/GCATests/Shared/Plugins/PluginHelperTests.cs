using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Plugins;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginManager.Abstractions;
using PluginManager;
using GSendTests.Mocks;
using GSendShared.Interfaces;

namespace GSendTests.Shared.Plugins
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class PluginHelperTests
    {
        delegate void LogMethod(in LogLevel logLevel, in string message);

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            var pluginClassesService = new Mock<IPluginClassesService>();
            var logger = new Mock<ILogger>();
            PluginHelper sut = new(logger.Object, pluginClassesService.Object);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_PluginClassService_Null_Throws_ArgumentNullException()
        {
            var logger = new Mock<ILogger>();
            new PluginHelper(logger.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_Logger_Null_Throws_ArgumentNullException()
        {
            var pluginClassesService = new Mock<IPluginClassesService>();
            new PluginHelper(null, pluginClassesService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeAllPlugins_InvalidParamPluginHost_Null_Throws_ArgumentNullException()
        {
            var logger = new Mock<ILogger>();
            var pluginClassesService = new Mock<IPluginClassesService>();
            PluginHelper sut = new(logger.Object, pluginClassesService.Object);

            Assert.IsNotNull(sut);

            sut.InitializeAllPlugins(null);
        }

        [TestMethod]
        public void InitializeAllPlugins_DifferentUsage_LoggedWithoutException()
        {
            MockLogger logger = new MockLogger();
            var pluginModule = new Mock<IGSendPluginModule>();
            pluginModule.Setup(m => m.Usage).Returns(PluginUsage.Service);
            pluginModule.Setup(m => m.Name).Returns("test plugin");
            pluginModule.Setup(m => m.MenuItems).Throws<InvalidOperationException>();

            var pluginHost = new Mock<ISenderPluginHost>();
            pluginHost.Setup(ph => ph.Usage).Returns(PluginUsage.Sender);

            var pluginClassesService = new Mock<IPluginClassesService>();
            pluginClassesService.Setup(m => m.GetPluginClasses<IGSendPluginModule>()).Returns(new List<IGSendPluginModule>() { pluginModule.Object });

            PluginHelper sut = new(logger, pluginClassesService.Object);
            Assert.IsNotNull(sut);

            sut.InitializeAllPlugins(pluginHost.Object);
            Assert.IsTrue(logger.LogItems.Contains("Warning - Attempt to load invalid plugin: test plugin"));
        }

        [TestMethod]
        public void InitializeAllPlugins_WithMenuItems_MenuCreationCalled_Success()
        {
            List<IPluginMenu> menuItems = new()
            {
                new MockPluginMenu("-", MenuType.Seperator, MenuParent.Tools),
                new MockPluginMenu("New Tool", MenuType.MenuItem, MenuParent.Tools),
            };

            List<IPluginMenu> createdMenuItems = new();

            MockLogger logger = new MockLogger();
            var pluginModule = new Mock<IGSendPluginModule>();
            pluginModule.Setup(m => m.Usage).Returns(PluginUsage.Sender);
            pluginModule.Setup(m => m.Name).Returns("test plugin");
            pluginModule.Setup(m => m.MenuItems).Returns(menuItems);
            pluginModule.Setup(m => m.Shortcuts).Returns(new List<IShortcut>());

            var pluginHost = new Mock<ISenderPluginHost>();
            pluginHost.Setup(ph => ph.Usage).Returns(PluginUsage.Sender);
            pluginHost.Setup(ph => ph.AddMenu(It.IsAny<IPluginMenu>())).Callback((IPluginMenu pm) => createdMenuItems.Add(pm));

            var pluginClassesService = new Mock<IPluginClassesService>();
            pluginClassesService.Setup(m => m.GetPluginClasses<IGSendPluginModule>()).Returns(new List<IGSendPluginModule>() { pluginModule.Object });

            PluginHelper sut = new(logger, pluginClassesService.Object);
            Assert.IsNotNull(sut);

            sut.InitializeAllPlugins(pluginHost.Object);
            Assert.AreEqual(0, logger.LogItems.Count);
            Assert.AreEqual(2, createdMenuItems.Count);
        }
    }
}
