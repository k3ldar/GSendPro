using GSendControls;

using GSendDesktop.Internal;

using GSendShared;

namespace GrblTuningWizard
{
    internal partial class MachineTuneZAxisAcceleration : BaseWizardPage
    {
        private readonly TuningWizardSettings _wizardSettings;
        private decimal _lastGoodValue;
        private bool _testInProgress;
        private DateTime _testStartTime;

        public MachineTuneZAxisAcceleration()
        {
            InitializeComponent();
            lblMachineRunning.Visible = false;
        }

        public MachineTuneZAxisAcceleration(TuningWizardSettings wizardSettings)
            : this()
        {
            _wizardSettings = wizardSettings ?? throw new ArgumentNullException(nameof(wizardSettings));
            _lastGoodValue = _wizardSettings.OriginalMaxAccelerationZ;
            lblOriginalValue.Text = HelperMethods.FormatSpeedValue(_wizardSettings.OriginalMaxAccelerationX);
            btnIncrease_Click(null, EventArgs.Empty);
        }

        public override void LoadResources()
        {
            lblAxisHeader.Text = String.Format(GSend.Language.Resources.TuneWizardZAxis, GSend.Language.Resources.TuneWizardAcceleration.ToLower());
            lblTuneAxis.Text = String.Format(GSend.Language.Resources.TuneWizardDescription, "Z", GSend.Language.Resources.TuneWizardAcceleration.ToLower());
            lblOriginalValueHeader.Text = GSend.Language.Resources.TuneWizardOriginalValue;
            lblIncrements.Text = GSend.Language.Resources.TuneWizardIncrements;
            btnRunTest.Text = GSend.Language.Resources.TuneWizardRunTest;
            btnIncrease.Text = GSend.Language.Resources.TuneWizardIncrease;
            btnDecrease.Text = GSend.Language.Resources.TuneWizardDecrease;
            lblCurrentTestValueHeader.Text = GSend.Language.Resources.TuneWizardNextTestValue;
            lblMachineRunning.Text = GSend.Language.Resources.TuneWizardTestInProgress;
        }

        public override bool CanCancel()
        {
            return !TestInProgress;
        }

        public override bool NextClicked()
        {
            if (!TestInProgress)
            {
                _wizardSettings.NewMaxAccelerationZ = _lastGoodValue;
            }

            return true;
        }

        public override bool CanGoNext()
        {
            return !TestInProgress;
        }

        public override bool CanGoPrevious()
        {
            return !TestInProgress;
        }

        public override bool CanGoFinish()
        {
            return false;
        }

        private bool TestInProgress
        {
            get => _testInProgress;

            set
            {
                _testInProgress = value;

                btnDecrease.Enabled = !value;
                btnIncrease.Enabled = !value;
                btnRunTest.Enabled = !value;
                numericIncrements.Enabled = !value;
            }
        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            try
            {
                _testStartTime = DateTime.UtcNow;
                _wizardSettings.SafeToContinue(_testStartTime);
                InternalRunTest().ConfigureAwait(false).GetAwaiter();
            }
            catch
            {
                ShowError(GSend.Language.Resources.TuneWizard, GSend.Language.Resources.TuneWizardErrorNoContinue);
            }
        }

        private async Task InternalRunTest()
        {
            TestInProgress = true;
            try
            {
                using (MouseControl mc = MouseControl.ShowWaitCursor(MainWizardForm))
                {
                    MainWizardForm.UpdateUI();
                    string newValueMessage = String.Format(Constants.MessageMachineUpdateSetting,
                        _wizardSettings.Machine.Id,
                        $"$120={_wizardSettings.NewMaxAccelerationZ}");
                    _wizardSettings.SenderPluginHost.SendMessage(newValueMessage);

                    double currentPos = _wizardSettings.CurrentZ;
                    _wizardSettings.SenderPluginHost.SendMessage(String.Format(Constants.MessageMachineWriteLine, _wizardSettings.Machine.Id, $"G1Z{currentPos - 50}F20000"));
                    _wizardSettings.SenderPluginHost.SendMessage(String.Format(Constants.MessageMachineWriteLine, _wizardSettings.Machine.Id, $"G1Z{currentPos}F20000"));
                    lblMachineRunning.Visible = true;

                    while (_wizardSettings.CurrentState != MachineState.Run)
                    {
                        _wizardSettings.SafeToContinue(_testStartTime);
                        await Task.Delay(20);
                    }

                    while (_wizardSettings.CurrentState == MachineState.Run)
                    {
                        _wizardSettings.SafeToContinue(_testStartTime);
                        await Task.Delay(20);
                    }
                }

                DialogResult testResult = MessageBox.Show(this,
                    GSend.Language.Resources.TuneWizardTestSuccess,
                    GSend.Language.Resources.TuneWizardTestSuccessHeader,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (testResult == DialogResult.Yes)
                {
                    _lastGoodValue = _wizardSettings.NewMaxAccelerationZ;
                    btnIncrease_Click(this, EventArgs.Empty);
                }
                else
                {
                    btnDecrease_Click(this, EventArgs.Empty);
                }
            }
            finally
            {
                lblMachineRunning.Visible = false;
                TestInProgress = false;
                MainWizardForm.UpdateUI();
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            _wizardSettings.NewMaxAccelerationZ += numericIncrements.Value;
            lblCurrentTestValue.Text = HelperMethods.FormatSpeedValue(_wizardSettings.NewMaxAccelerationZ);
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            _wizardSettings.NewMaxAccelerationZ -= numericIncrements.Value;
            lblCurrentTestValue.Text = HelperMethods.FormatSpeedValue(_wizardSettings.NewMaxAccelerationZ);
        }
    }
}
