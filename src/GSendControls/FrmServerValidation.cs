using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendApi;

using GSendDesktop.Internal;
using GSendCommon.Settings;
using GSendShared;
using System.Diagnostics;

namespace GSendControls
{
    public partial class FrmServerValidation : BaseForm
    {

        private DateTime _nextLicenseCheck;
        private readonly IGSendApiWrapper _apiWrapper;
        private readonly bool _isLicensed;
        private bool _uriChanged = false;

        protected FrmServerValidation()
        {
            InitializeComponent();
        }

        public FrmServerValidation(IGSendApiWrapper apiWrapper, bool isLicensed)
            : this()
        {
            _apiWrapper = apiWrapper ?? throw new ArgumentNullException(nameof(apiWrapper));
            _isLicensed = isLicensed;

            _nextLicenseCheck = DateTime.UtcNow.AddSeconds(30);
            lblNextLicenseCheck.Text = String.Format(GSend.Language.Resources.LicenseNextCheck, 30);
            pnlLicenseCheck.Visible = !_isLicensed;
            tmrLicenseCheck.Enabled = !_isLicensed;
        }

        public static bool ValidateServer(Form parent, IGSendApiWrapper apiWrapper)
        {
            bool isLicensed = ValidateLicense(apiWrapper, out bool isError);

            if (isError || !isLicensed)
            {
                using FrmServerValidation frmServerValidation = new(apiWrapper, isLicensed);

                if (frmServerValidation.ShowDialog(parent) != DialogResult.OK)
                {
                    Application.Exit();
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateLicense(IGSendApiWrapper apiWrapper, out bool isError)
        {
            isError = false;
            bool isLicensed = false;
            try
            {
                isLicensed = apiWrapper.IsLicenseValid();
            }
            catch (GSendApiException)
            {
                isError = true;
            }

            return isLicensed;
        }

        protected override string SectionName => "ServerValidation";

        protected override void LoadResources()
        {
            Text = GSend.Language.Resources.ServerValidation;
            btnCancel.Text = GSend.Language.Resources.Cancel;
            btnOK.Text = GSend.Language.Resources.OK;
            lblServerUri.Text = GSend.Language.Resources.ServerUri;
            lblLicenseValidation.Text = _isLicensed ?
                GSend.Language.Resources.LicenseValid :
                GSend.Language.Resources.LicenseInvalid;
            btnCheckNow.Text = GSend.Language.Resources.CheckNow;
            btnViewLicense.Text = GSend.Language.Resources.ViewLicense;
        }

        private void FrmLicenseValidation_Shown(object sender, EventArgs e)
        {
            txtServerAddress.Text = _apiWrapper.ServerAddress.ToString();
        }

        private void tmrLicenseCheck_Tick(object sender, EventArgs e)
        {
            TimeSpan nextCheck = _nextLicenseCheck - DateTime.UtcNow;

            if (nextCheck.TotalSeconds > 0)
            {
                string label = String.Format(GSend.Language.Resources.LicenseNextCheck, (int)nextCheck.TotalSeconds);

                if (lblNextLicenseCheck.Text != label)
                {
                    lblNextLicenseCheck.Text = String.Empty;
                    lblNextLicenseCheck.Text = label;
                    Invalidate();
                }
            }
            else
            {
                btnCheckNow.Enabled = false;
                lblNextLicenseCheck.Text = GSend.Language.Resources.LicenseCheckValidating;
                btnOK.Enabled = false;

                using (MouseControl mc = MouseControl.ShowWaitCursor(this))
                {
                    bool isLicensed = ValidateLicense(_apiWrapper, out bool _);

                    if (isLicensed)
                    {
                        DialogResult = DialogResult.OK;
                    }

                    _nextLicenseCheck = DateTime.UtcNow.AddSeconds(30);
                    btnOK.Enabled = true;
                    btnCheckNow.Enabled = true;
                }
            }            
        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            _nextLicenseCheck = DateTime.UtcNow.AddMinutes(-30);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Enabled = false;
            btnCheckNow.Enabled = false;
            bool isLicensed = false;
            bool isError = false;

            using (MouseControl mc = MouseControl.ShowWaitCursor(this))
                isLicensed = ValidateLicense(_apiWrapper, out isError);

            if (!isError && isLicensed)
            {
                if (_uriChanged)
                {
                    SaveUriChanges();
                }

                DialogResult = DialogResult.OK;
            }
            else
            {
                btnOK.Enabled = true;
                btnCheckNow.Enabled = true;
            }
        }

        private void SaveUriChanges()
        {
            try
            {
                Uri https = new(txtServerAddress.Text, UriKind.Absolute);

                UriBuilder uriBuilder = new(txtServerAddress.Text)
                {
                    Port = https.Port - 1
                };
                Uri http = uriBuilder.Uri;
                GSendCommon.Settings.AppSettings appSettings = GSendCommon.Settings.AppSettings.Load();
                appSettings.Kestrel.Endpoints.HTTPS.Url = https;
                appSettings.ApiSettings.RootAddress = https;
                appSettings.Kestrel.Endpoints.HTTP.Url = http;
                appSettings.Save();
            }
            catch(Exception err)
            {
                MessageBox.Show(this, String.Format(GSend.Language.Resources.ErrorSavingServerUri, err.Message), GSend.Language.Resources.SaveError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtServerAddress_TextChanged(object sender, EventArgs e)
        {
            _uriChanged = true;
        }

        private void btnViewLicense_Click(object sender, EventArgs e)
        {
            UriBuilder uriBuilder = new(_apiWrapper.ServerAddress)
            {
                Path = "/Home/ViewLicense"
            };

            Uri licenseUri = uriBuilder.Uri;
            Process.Start(new ProcessStartInfo(licenseUri.ToString()) {  UseShellExecute = true });
        }
    }
}
