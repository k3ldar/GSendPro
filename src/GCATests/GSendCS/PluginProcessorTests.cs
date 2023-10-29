using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

using GSendCS.Processors;

using GSendShared;
using GSendShared.Plugins;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0063

namespace GSendTests.GSendCS
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class PluginProcessorTests
    {
        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            PluginProcessor sut = new(Path.GetTempFileName());
            Assert.IsNotNull(sut);
            Assert.AreEqual("Plugin", sut.Name);
            Assert.AreEqual(0, sut.SortOrder);
            Assert.IsTrue(sut.IsEnabled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_NullFileName_Throws_ArgumentNullException()
        {
            PluginProcessor sut = new(null);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_InternalConstructor_NullFileName_Throws_ArgumentNullException()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            PluginProcessor sut = new(mockArgs, mockDisplay, null);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_EmptyStringFileName_Throws_ArgumentNullException()
        {
            PluginProcessor sut = new("");
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_InternalConstructor_EmptyStringFileName_Throws_ArgumentNullException()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            PluginProcessor sut = new(mockArgs, mockDisplay, "");
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void DisplayHelp_DisplaysCorrectMessages_Success()
        {
            string pluginSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile);
            sut.DisplayHelp();

            Assert.AreEqual(0, mockDisplay.Lines.Count);
        }

        [TestMethod]
        public void Execute_Returns_InvalidParameters()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            PluginProcessor sut = new(mockArgs, mockDisplay, Path.GetTempFileName());
            int result = sut.Execute(Array.Empty<string>());
            Assert.AreEqual(-1, result);
            Assert.AreEqual(1, mockDisplay.Lines.Count);
            Assert.AreEqual("Invalid Options!", mockDisplay.Lines[0]);
        }

        [TestMethod]
        public void Add_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string pluginSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(pluginSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
                {
                    string assemblyName = "myass";
                    int result = sut.Add("testPlugin", assemblyName, PluginHosts.Any, MachineType.Unspecified, MachineFirmware.grblv1_1);

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to settings file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Add_CreatesNewPluginOption_StillReadabeWhilstFileStreamOpen_WhenFileDoesNotExists()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                string assemblyName = "myass";
                int result = sut.Add("testPlugin", assemblyName, PluginHosts.Any, MachineType.Unspecified, MachineFirmware.grblv1_1);

                Assert.AreEqual(0, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                // ensure file still loads despite being locked
                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]", fileData);
        }

        [TestMethod]
        public void Add_PluginNameAlreadyExists_ReturnsErrorCodePluginExists()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            File.WriteAllText(pluginSettingsFile, "[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                string assemblyName = "myass";
                int result = sut.Add("testPlugin", assemblyName, PluginHosts.Any, MachineType.Unspecified, MachineFirmware.grblv1_1);

                Assert.AreEqual(-101, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]", fileData);
        }

        [TestMethod]
        public void Update_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string pluginSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(pluginSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
                {
                    string assemblyName = "myass";
                    int result = sut.Update("testPlugin", assemblyName, PluginHosts.Any, MachineType.Unspecified, MachineFirmware.grblv1_1);

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to settings file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Update_UpdatesExistingPluginOption_StillReadabeWhilstFileStreamOpen_WhenFileDoesNotExists()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            File.WriteAllText(pluginSettingsFile, "[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                string assemblyName = "ass";
                int result = sut.Update("testPlugin", assemblyName, PluginHosts.SenderHost, MachineType.CNC, MachineFirmware.grblv1_1, false, true, "a desc");

                Assert.AreEqual(0, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                // ensure file still loads despite being locked
                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[{\"Name\":\"testPlugin\",\"Description\":\"a desc\",\"AssemblyName\":\"ass\",\"Usage\":2,\"MachineType\":1,\"MachineFirmware\":0,\"Enabled\":false,\"ShowToolbarItems\":true}]", fileData);
        }

        [TestMethod]
        public void Update_PluginNameDoesNotExistExists_ReturnsErrorCodePluginExists()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            File.WriteAllText(pluginSettingsFile, "[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                string assemblyName = "ass";
                int result = sut.Update("MyTestPlugin", assemblyName, PluginHosts.SenderHost, MachineType.CNC, MachineFirmware.grblv1_1);

                Assert.AreEqual(-102, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]", fileData);
        }

        [TestMethod]
        public void Delete_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string pluginSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(pluginSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
                {
                    int result = sut.Delete("testPlugin");

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to settings file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Delete_ExistingPluginOption_StillReadabeWhilstFileStreamOpen_WhenFileDoesNotExist()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            File.WriteAllText(pluginSettingsFile, "[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                int result = sut.Delete("testPlugin");

                Assert.AreEqual(0, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                // ensure file still loads despite being locked
                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[]", fileData);
        }

        [TestMethod]
        public void Delete_PluginNameDoesNotExistExist_ReturnsErrorCodePluginNotExists()
        {
            List<GSendPluginSettings> plugins = null;
            string pluginSettingsFile = Path.GetTempFileName();
            File.WriteAllText(pluginSettingsFile, "[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                int result = sut.Delete("MyTestPlugin");

                Assert.AreEqual(-102, result);
                Assert.IsTrue(File.Exists(pluginSettingsFile));

                plugins = HelperMethods.LoadPluginSettings(pluginSettingsFile);
            }

            File.Delete(pluginSettingsFile);
            string fileData = JsonSerializer.Serialize(plugins);
            Assert.AreEqual("[{\"Name\":\"testPlugin\",\"Description\":null,\"AssemblyName\":\"myass\",\"Usage\":15,\"MachineType\":0,\"MachineFirmware\":0,\"Enabled\":true,\"ShowToolbarItems\":false}]", fileData);
        }

        [TestMethod]
        public void Show_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string pluginSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(pluginSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
                {
                    int result = sut.Show();

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to settings file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Show_DisplayAsTable_Success()
        {
            string pluginSettingsFile = Path.GetTempFileName();

            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (PluginProcessor sut = new(mockArgs, mockDisplay, pluginSettingsFile))
            {
                // add test plugins
                sut.Add("test1", "myassembly", PluginHosts.Editor, MachineType.CNC, MachineFirmware.grblv1_1, description: "Test Plugin 1");
                sut.Add("test2", "myassembly", PluginHosts.Sender, MachineType.Printer, MachineFirmware.grblv1_1, description: "Test Plugin 2");
                sut.Add("test3", "myassembly", PluginHosts.SenderHost, MachineType.CNC, MachineFirmware.grblv1_1, description: "Test Plugin 3");
                sut.Add("test4", "myassembly", PluginHosts.Service, MachineType.Laser, MachineFirmware.grblv1_1, description: "Test Plugin 4");
                sut.Add("test5", "myassembly", PluginHosts.Any, MachineType.Unspecified, MachineFirmware.grblv1_1, description: "Test Plugin 5");

                int result = sut.Show();

                Assert.AreEqual(2147483647, result);
                Assert.IsTrue(sut.IsExclusive);
                Assert.AreEqual(6, mockDisplay.Lines.Count);
                Assert.AreEqual("Quiet, Name\tAssembly Name\tEnabled\tUsage\tMachine Type\tFirmware\tShow Toolbar Items\tDescription", mockDisplay.Lines[0]);
            }
        }
    }
}

#pragma warning restore IDE0063