using GSendControls;

using GSendShared;

namespace GrblTuningWizard
{
    internal partial class PageFinish : BaseWizardPage
    {
        private readonly TuningWizardSettings _wizardSettings;
        private readonly ListViewItem _lvAccelX = new($"{GSend.Language.Resources.GCodeMaxAccelerationX} ( mm/sec²)");
        private readonly ListViewItem _lvAccelY = new($"{GSend.Language.Resources.GCodeMaxAccelerationY} ( mm/sec²)");
        private readonly ListViewItem _lvAccelZ = new($"{GSend.Language.Resources.GCodeMaxAccelerationZ} ( mm/sec²)");
        private readonly ListViewItem _lvFeedX = new($"{GSend.Language.Resources.GCodeMaxFeedRateX} ( mm/min)");
        private readonly ListViewItem _lvFeedY = new($"{GSend.Language.Resources.GCodeMaxFeedRateY} ( mm/min)");
        private readonly ListViewItem _lvFeedZ = new($"{GSend.Language.Resources.GCodeMaxFeedRateZ} ( mm/min)");

        public PageFinish()
        {
            InitializeComponent();
            lvDetails.Items.Add(_lvAccelX);
            lvDetails.Items.Add(_lvAccelY);
            lvDetails.Items.Add(_lvAccelZ);
            lvDetails.Items.Add(_lvFeedX);
            lvDetails.Items.Add(_lvFeedY);
            lvDetails.Items.Add(_lvFeedZ);

            foreach (ListViewItem item in lvDetails.Items)
            {
                for (int i = 0; i < 4; i++)
                    item.SubItems.Add(String.Empty);
            }
        }

        public PageFinish(TuningWizardSettings wizardSettings)
            : this()
        {
            _wizardSettings = wizardSettings ?? throw new ArgumentNullException(nameof(wizardSettings));
        }

        public override void LoadResources()
        {
            lblReductionAmount.Text = GSend.Language.Resources.TuneWizardFinalRecuctionPercent;
            lblFinishHeader.Text = GSend.Language.Resources.TuneWizardFinishHeader;
            lblMachineNameHeader.Text = GSend.Language.Resources.Name;
            columnHeaderName.Text = String.Empty;
            columnHeaderOriginal.Text = GSend.Language.Resources.TuneWizardOriginalValue;
            columnHeaderMax.Text = GSend.Language.Resources.TuneWizardMaxValue;
            columnHeaderFinal.Text = GSend.Language.Resources.TuneWizardFinalValue;
            columnHeaderPercent.Text = GSend.Language.Resources.TuneWizardPercent;
        }

        public override void PageShown()
        {
            UpdateFinalValues();
        }

        public override bool BeforeFinish()
        {
            _wizardSettings.UpdateMachineSettings();
            return true;
        }

        private void UpdateFinalValues()
        {
            _lvAccelX.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxAccelerationX);
            _lvAccelX.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxAccelerationX);
            _lvAccelX.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxAccelerationX);
            _lvAccelX.SubItems[4].Text = _wizardSettings.PercentMaxAccelerationX;

            _lvAccelY.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxAccelerationY);
            _lvAccelY.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxAccelerationY);
            _lvAccelY.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxAccelerationY);
            _lvAccelY.SubItems[4].Text = _wizardSettings.PercentMaxAccelerationY;

            _lvAccelZ.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxAccelerationZ);
            _lvAccelZ.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxAccelerationZ);
            _lvAccelZ.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxAccelerationZ);
            _lvAccelZ.SubItems[4].Text = _wizardSettings.PercentMaxAccelerationZ;

            _lvFeedX.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxFeedX);
            _lvFeedX.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxFeedX);
            _lvFeedX.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxFeedX);
            _lvFeedX.SubItems[4].Text = _wizardSettings.PercentMaxFeedX;

            _lvFeedY.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxFeedY);
            _lvFeedY.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxFeedY);
            _lvFeedY.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxFeedY);
            _lvFeedY.SubItems[4].Text = _wizardSettings.PercentMaxFeedY;

            _lvFeedZ.SubItems[1].Text = HelperMethods.FormatSpeed(_wizardSettings.OriginalMaxFeedZ);
            _lvFeedZ.SubItems[2].Text = HelperMethods.FormatSpeed(_wizardSettings.NewMaxFeedZ);
            _lvFeedZ.SubItems[3].Text = HelperMethods.FormatSpeed(_wizardSettings.FinalMaxFeedZ);
            _lvFeedZ.SubItems[4].Text = _wizardSettings.PercentMaxFeedZ;
        }

        private void numericAdjustment_ValueChanged(object sender, EventArgs e)
        {
            _wizardSettings.FinalReductionPercent = numericAdjustment.Value;
            UpdateFinalValues();
        }
    }
}
