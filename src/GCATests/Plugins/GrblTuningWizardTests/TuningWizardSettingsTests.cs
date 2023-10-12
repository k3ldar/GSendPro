using System;
using System.Diagnostics.CodeAnalysis;

using GrblTuningWizard;

using GSendTests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.Plugins.GrblTuningWizardTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TuningWizardSettingsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_InvalidParam_Null_Throws_ArgumentNullException()
        {
            new TuningWizardSettings(null);
        }

        [TestMethod]
        public void UpdateMaxFeedRateX_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedX = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedX);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxFeedX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxFeedRateX_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedX = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedX);
            Assert.AreEqual("12.50%", sut.PercentMaxFeedX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedX);
            Assert.AreEqual("0.00%", sut.PercentMaxFeedX);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxFeedX);
            Assert.AreEqual("5.00%", sut.PercentMaxFeedX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$110=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxFeedRateX_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedX = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxFeedX);
            Assert.AreEqual("-32.50%", sut.PercentMaxFeedX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxFeedX);
            Assert.AreEqual("-40.00%", sut.PercentMaxFeedX);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxFeedX);
            Assert.AreEqual("-37.00%", sut.PercentMaxFeedX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$110=1260", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxFeedRateY_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedY = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedY);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxFeedY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxFeedRateY_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedY = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedY);
            Assert.AreEqual("12.50%", sut.PercentMaxFeedY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedY);
            Assert.AreEqual("0.00%", sut.PercentMaxFeedY);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxFeedY);
            Assert.AreEqual("5.00%", sut.PercentMaxFeedY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$111=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxFeedRateY_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedY = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxFeedY);
            Assert.AreEqual("-32.50%", sut.PercentMaxFeedY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxFeedY);
            Assert.AreEqual("-40.00%", sut.PercentMaxFeedY);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxFeedY);
            Assert.AreEqual("-37.00%", sut.PercentMaxFeedY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$111=1260", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxFeedRateZ_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedZ = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedZ);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxFeedZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxFeedRateZ_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedZ = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxFeedZ);
            Assert.AreEqual("12.50%", sut.PercentMaxFeedZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxFeedZ);
            Assert.AreEqual("0.00%", sut.PercentMaxFeedZ);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxFeedZ);
            Assert.AreEqual("5.00%", sut.PercentMaxFeedZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$112=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxFeedRateZ_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxFeedRateZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxFeedZ = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxFeedZ);
            Assert.AreEqual("-32.50%", sut.PercentMaxFeedZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxFeedZ);
            Assert.AreEqual("-40.00%", sut.PercentMaxFeedZ);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxFeedZ);
            Assert.AreEqual("-37.00%", sut.PercentMaxFeedZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$112=1260", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateX_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationX = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationX);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxAccelerationX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateX_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationX = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationX);
            Assert.AreEqual("12.50%", sut.PercentMaxAccelerationX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationX);
            Assert.AreEqual("0.00%", sut.PercentMaxAccelerationX);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxAccelerationX);
            Assert.AreEqual("5.00%", sut.PercentMaxAccelerationX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$120=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateX_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationX = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationX = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxAccelerationX);
            Assert.AreEqual("-32.50%", sut.PercentMaxAccelerationX);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxAccelerationX);
            Assert.AreEqual("-40.00%", sut.PercentMaxAccelerationX);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxAccelerationX);
            Assert.AreEqual("-37.00%", sut.PercentMaxAccelerationX);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$120=1260", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateY_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationY = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationY);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxAccelerationY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateY_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationY = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationY);
            Assert.AreEqual("12.50%", sut.PercentMaxAccelerationY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationY);
            Assert.AreEqual("0.00%", sut.PercentMaxAccelerationY);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxAccelerationY);
            Assert.AreEqual("5.00%", sut.PercentMaxAccelerationY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$121=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateY_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationY = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationY = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxAccelerationY);
            Assert.AreEqual("-32.50%", sut.PercentMaxAccelerationY);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxAccelerationY);
            Assert.AreEqual("-40.00%", sut.PercentMaxAccelerationY);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxAccelerationY);
            Assert.AreEqual("-37.00%", sut.PercentMaxAccelerationY);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$121=1260", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateZ_SameValueAsOriginal_NoCommandSent_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationZ = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationZ);

            sut.FinalReductionPercent = 15;
            Assert.AreEqual(2125, sut.FinalMaxAccelerationZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(0, mockSenderPluginHost.Messages.Count);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateZ_PositiveValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationZ = 2500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(2250, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("12.50%", sut.PercentMaxAccelerationZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(2000, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("0.00%", sut.PercentMaxAccelerationZ);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(2100, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("5.00%", sut.PercentMaxAccelerationZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$122=2100", mockSenderPluginHost.Messages[0]);
        }

        [TestMethod]
        public void UpdateMaxAccelerationRateZ_NegativeValue_CorrectSpeedSentToMachine_Success()
        {
            MockMachine mockMachine = new MockMachine();
            mockMachine.Settings.MaxAccelerationZ = 2000;
            MockSenderPluginHost mockSenderPluginHost = new();
            mockSenderPluginHost.Machine = mockMachine;

            TuningWizardSettings sut = new TuningWizardSettings(mockSenderPluginHost);
            Assert.IsNotNull(sut);

            sut.NewMaxAccelerationZ = 1500;

            sut.FinalReductionPercent = 10;
            Assert.AreEqual(1350, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("-32.50%", sut.PercentMaxAccelerationZ);

            sut.FinalReductionPercent = 20;
            Assert.AreEqual(1200, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("-40.00%", sut.PercentMaxAccelerationZ);

            sut.FinalReductionPercent = 16;
            Assert.AreEqual(1260, sut.FinalMaxAccelerationZ);
            Assert.AreEqual("-37.00%", sut.PercentMaxAccelerationZ);

            sut.UpdateMachineSettings();
            Assert.AreEqual(1, mockSenderPluginHost.Messages.Count);
            Assert.AreEqual("mUpdateSetting:231:$122=1260", mockSenderPluginHost.Messages[0]);
        }
    }
}
