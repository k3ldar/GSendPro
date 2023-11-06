using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

using GSendCS.Processors;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0063

namespace GSendTests.GSendCS
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public sealed class ServerAddressProcessorTests
    {
        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            ServerAddressProcessor sut = new(Path.GetTempFileName());
            Assert.IsNotNull(sut);
            Assert.AreEqual("Server", sut.Name);
            Assert.AreEqual(0, sut.SortOrder);
            Assert.IsTrue(sut.IsEnabled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_NullFileName_Throws_ArgumentNullException()
        {
            ServerAddressProcessor sut = new(null);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_InternalConstructor_NullFileName_Throws_ArgumentNullException()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            ServerAddressProcessor sut = new(mockArgs, mockDisplay, null);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_EmptyStringFileName_Throws_ArgumentNullException()
        {
            ServerAddressProcessor sut = new("");
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidInstance_InternalConstructor_EmptyStringFileName_Throws_ArgumentNullException()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            ServerAddressProcessor sut = new(mockArgs, mockDisplay, "");
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void DisplayHelp_DisplaysCorrectMessages_Success()
        {
            string serverSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile);
            sut.DisplayHelp();

            Assert.AreEqual(0, mockDisplay.Lines.Count);
        }

        [TestMethod]
        public void Execute_Returns_InvalidParameters()
        {
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();
            ServerAddressProcessor sut = new(mockArgs, mockDisplay, Path.GetTempFileName());
            int result = sut.Execute(Array.Empty<string>());
            Assert.AreEqual(-1, result);
            Assert.AreEqual(1, mockDisplay.Lines.Count);
            Assert.AreEqual("Invalid Options!", mockDisplay.Lines[0]);
        }

        [TestMethod]
        public void Add_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string serverSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(serverSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
                {
                    int result = sut.Add("127.0.0.1", 7150);

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to server address file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Add_NewServerAddress_StillReadabeWhilstFileStreamOpen_WhenFileDoesNotExists()
        {
            string serverSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Add("127.0.0.1", 7150);

                Assert.AreEqual(0, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));

                // ensure file still loads despite being locked
                using FileStream fileStream = new(serverSettingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                List<string> serverAddresses = JsonSerializer.Deserialize<List<string>>(fileStream);
                Assert.AreEqual(1, serverAddresses.Count);
                Assert.AreEqual("http://127.0.0.1:7150/", serverAddresses[0]);
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Add_ServerAddressAlreadyExists_ReturnsErrorCodeExists()
        {
            string serverSettingsFile = Path.GetTempFileName();
            File.WriteAllText(serverSettingsFile, "[\"http://127.0.0.1:7150/\"]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Add("127.0.0.1", 7150);

                Assert.AreEqual(-101, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Add_ServerAddressInvalidUri_Null_ReturnsErrorCodeInvalidUri()
        {
            string serverSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Add(null, 0);

                Assert.AreEqual(-103, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Add_ServerAddressInvalidUri_EmptyString_ReturnsErrorCodeInvalidUri()
        {
            string serverSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Add("", 0);

                Assert.AreEqual(-103, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Add_ServerAddressInvalidUri_PartialUri_ReturnsErrorCodeInvalidUri()
        {
            string serverSettingsFile = Path.GetTempFileName();
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Add("/myUri", 7365);

                Assert.AreEqual(-103, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Delete_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string serverSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(serverSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
                {
                    int result = sut.Delete("http://127.0.0.1:7150");

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to server address file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Delete_ExistingServerAddress_StillReadableWhilstFileStreamOpen_WhenFileDoesNotExist()
        {
            string serverSettingsFile = Path.GetTempFileName();
            File.WriteAllText(serverSettingsFile, "[\"http://127.0.0.1:7150\"]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Delete("http://127.0.0.1:7150");

                Assert.AreEqual(0, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));

                // ensure file still loads despite being locked
                using FileStream fileStream = new(serverSettingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Delete_ServerAddressDoesNotExistExist_ReturnsErrorCodeNotExist()
        {
            string serverSettingsFile = Path.GetTempFileName();
            File.WriteAllText(serverSettingsFile, "[]");
            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Delete("http://127.0.0.1:7150");

                Assert.AreEqual(-102, result);
                Assert.IsTrue(File.Exists(serverSettingsFile));
            }

            File.Delete(serverSettingsFile);
        }

        [TestMethod]
        public void Show_ExclusiveAccessNotAvailable_ReturnsErrorCodeExclusiveAccessDenied()
        {
            string serverSettingsFile = Path.GetTempFileName();

            using (FileStream fileStream = new(serverSettingsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                MockCommandLineArgs mockArgs = new();
                MockDisplay mockDisplay = new();

                using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
                {
                    int result = sut.Show();

                    Assert.AreEqual(-100, result);
                    Assert.IsFalse(sut.IsExclusive);
                    Assert.AreEqual(1, mockDisplay.Lines.Count);
                    Assert.AreEqual("Unable to gain access to server address file!", mockDisplay.Lines[0]);
                }
            }
        }

        [TestMethod]
        public void Show_DisplayAsTable_Success()
        {
            string serverSettingsFile = Path.GetTempFileName();

            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                // add test addresses
                sut.Add("127.0.0.1", 7150);
                sut.Add("127.0.0.1", 7151);
                sut.Add("127.0.0.1", 7152);
                sut.Add("127.0.0.1", 7153);
                sut.Add("127.0.0.1", 7154);

                int result = sut.Show();

                Assert.AreEqual(2147483647, result);
                Assert.IsTrue(sut.IsExclusive);
                Assert.AreEqual(11, mockDisplay.Lines.Count);
                Assert.AreEqual("Quiet, Address", mockDisplay.Lines[5]);
            }
        }

        [TestMethod]
        public void Show_NoAddressesFound_DisplaysMessage_Success()
        {
            string serverSettingsFile = Path.GetTempFileName();


            MockCommandLineArgs mockArgs = new();
            MockDisplay mockDisplay = new();

            using (ServerAddressProcessor sut = new(mockArgs, mockDisplay, serverSettingsFile))
            {
                int result = sut.Show();

                Assert.AreEqual(-104, result);
                Assert.IsTrue(sut.IsExclusive);
                Assert.AreEqual(1, mockDisplay.Lines.Count);
                Assert.AreEqual("Quiet, No server addresses found", mockDisplay.Lines[0]);
            }
        }
    }
}

#pragma warning restore IDE0063