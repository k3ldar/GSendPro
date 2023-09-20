using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendControls;

using GSendShared;

namespace GrblTuningWizard
{

    internal partial class MachineMoveLocation : BaseWizardPage
    {
        private readonly TuningWizardSettings _wizardSettings;

        public MachineMoveLocation()
        {
            InitializeComponent();
        }

        public MachineMoveLocation(TuningWizardSettings wizardSettings)
            : this()
        {
            _wizardSettings = wizardSettings ?? throw new ArgumentNullException(nameof(wizardSettings));

            jogControl1.FeedMaximum = (int)_wizardSettings.Machine.Settings.MaxFeedRateX;
            jogControl1.FeedMinimum = 0;
            jogControl1.FeedRate = jogControl1.FeedMaximum / 2;
            jogControl1.StepValue = 7;

            if (!_wizardSettings.Machine.Settings.HomingCycle)
            {
                lblAuto.Visible = false;
                btnMoveAuto.Visible = false;
            }
        }

        public override void LoadResources()
        {
            lblHeader.Text = GSend.Language.Resources.TuneWizardMoveMachineHeader;
            lblAuto.Text = GSend.Language.Resources.TuneWizardAutoMove;
            btnMoveAuto.Text = GSend.Language.Resources.TuneWizardMoveToCenter;
        }

        private void JogControl1_OnJogStart(GSendShared.JogDirection jogDirection, double stepSize, double feedRate)
        {
            _wizardSettings.SenderPluginHost.SendMessage(String.Format(GSendShared.Constants.MessageMachineJogStart, _wizardSettings.Machine.Id, jogDirection, stepSize, feedRate));
        }

        private void JogControl1_OnJogStop(object sender, EventArgs e)
        {
            _wizardSettings.SenderPluginHost.SendMessage(String.Format(GSendShared.Constants.MessageMachineJogStop, _wizardSettings.Machine.Id));
        }

        private void btnMoveAuto_Click(object sender, EventArgs e)
        {
            _wizardSettings.SenderPluginHost.SendMessage(String.Format(GSendShared.Constants.MessageMachineHome, _wizardSettings.Machine.Id));

            string moveToCenter = String.Format("G0X{0}Y{1}", _wizardSettings.MaxTravelX / 2, _wizardSettings.MaxTravelY / 2);
            _wizardSettings.SenderPluginHost.SendMessage(String.Format(GSendShared.Constants.MessageMachineWriteLine, _wizardSettings.Machine.Id, moveToCenter));
        }
    }
}
