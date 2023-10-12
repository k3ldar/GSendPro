using GSendControls;

using GSendShared;

namespace GrblTuningWizard
{
    internal partial class PageWelcome : BaseWizardPage
    {
        private readonly TuningWizardSettings _wizardSettings;

        public PageWelcome()
        {
            InitializeComponent();
        }

        public PageWelcome(TuningWizardSettings wizardSettings)
            : this()
        {
            _wizardSettings = wizardSettings ?? throw new ArgumentNullException(nameof(wizardSettings));

            lblMachineName.Text = _wizardSettings.Machine.Name;
            lblMaxAccelValueX.Text = HelperMethods.FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationX);
            lblMaxAccelValueY.Text = HelperMethods.FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationY);
            lblMaxAccelValueZ.Text = HelperMethods.FormatAccelerationValue(_wizardSettings.OriginalMaxAccelerationZ);
            lblMaxValueX.Text = HelperMethods.FormatSpeedValue(_wizardSettings.OriginalMaxFeedX);
            lblMaxValueY.Text = HelperMethods.FormatSpeedValue(_wizardSettings.OriginalMaxFeedY);
            lblMaxValueZ.Text = HelperMethods.FormatSpeedValue(_wizardSettings.OriginalMaxFeedZ);
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
    }
}
