using GSendControls;

namespace GrblTuningWizard
{
    internal partial class PageFinish : BaseWizardPage
    {
        private readonly TuningWizardSettings _wizardSettings;

        public PageFinish()
        {
            InitializeComponent();
        }

        public PageFinish(TuningWizardSettings wizardSettings)
            : this()
        {
            _wizardSettings = wizardSettings ?? throw new ArgumentNullException(nameof(wizardSettings));

            lblMachineName.Text = _wizardSettings.Machine.Name;
            lblMaxAccelValueX.Text = FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationX);
            lblMaxAccelValueY.Text = FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationY);
            lblMaxAccelValueZ.Text = FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationZ);
            lblMaxValueX.Text = FormatSpeedValue(_wizardSettings.OriginalMaxFeedX);
            lblMaxValueY.Text = FormatSpeedValue(_wizardSettings.OriginalMaxFeedY);
            lblMaxValueZ.Text = FormatSpeedValue(_wizardSettings.OriginalMaxFeedZ);
        }

        public override void LoadResources()
        {
            lblMachineNameHeader.Text = GSend.Language.Resources.Name;
            lblMaxX.Text = GSend.Language.Resources.GCodeMaxFeedRateX;
            lblMaxY.Text = GSend.Language.Resources.GCodeMaxFeedRateY;
            lblMaxZ.Text = GSend.Language.Resources.GCodeMaxFeedRateZ;
            lblMaxAccelX.Text = GSend.Language.Resources.GCodeMaxAccelerationX;
            lblMaxAccelY.Text = GSend.Language.Resources.GCodeMaxAccelerationY;
            lblMaxAccelZ.Text = GSend.Language.Resources.GCodeMaxAccelerationZ;
        }

        public override void PageShown()
        {

            lblMaxAccelValueNewX.Text = FormatAccelerationValue(_wizardSettings.NewMaxAccelerationX);
            lblMaxAccelValueNewY.Text = FormatAccelerationValue(_wizardSettings.NewMaxAccelerationY);
            lblMaxAccelValueNewZ.Text = FormatAccelerationValue(_wizardSettings.NewMaxAccelerationZ);
            lblMaxValueNewX.Text = FormatSpeedValue(_wizardSettings.NewMaxFeedX);
            lblMaxValueNewY.Text = FormatSpeedValue(_wizardSettings.NewMaxFeedY);
            lblMaxValueNewZ.Text = FormatSpeedValue(_wizardSettings.NewMaxFeedZ);

            lblMaxAccelValueDiffX.Text = FormatPercentDiff(_wizardSettings.OriginalMaxAccelerationX, _wizardSettings.NewMaxAccelerationX);
            lblMaxAccelValueDiffY.Text = FormatPercentDiff(_wizardSettings.OriginalMaxAccelerationY, _wizardSettings.NewMaxAccelerationY);
            lblMaxAccelValueDiffZ.Text = FormatPercentDiff(_wizardSettings.OriginalMaxAccelerationZ, _wizardSettings.NewMaxAccelerationZ);
            lblMaxValueDiffX.Text = FormatPercentDiff(_wizardSettings.OriginalMaxFeedX, _wizardSettings.NewMaxFeedX);
            lblMaxValueDiffY.Text = FormatPercentDiff(_wizardSettings.OriginalMaxFeedY, _wizardSettings.NewMaxFeedY);
            lblMaxValueDiffZ.Text = FormatPercentDiff(_wizardSettings.OriginalMaxFeedZ, _wizardSettings.NewMaxFeedZ);
        }
    }
}
