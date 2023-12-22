using System;
using System.Windows.Forms;

using GSendApi;

using GSendControls.Abstractions;
using GSendControls.Threads;

using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Interfaces;

using Shared.Classes;

namespace GSendControls.Forms
{
    public partial class FrmConfigureServer : BaseForm, IServerAvailability
    {
        private readonly IRunProgram _runProgram;
        private readonly IGSendApiWrapper _gSendApiWrapper;
        private readonly IServerConfigurationUpdated _serverConfigurationUpdated;
        private readonly ICommonUtils _commonUtils;

        public FrmConfigureServer(IRunProgram runProgram, IGSendApiWrapper gSendApiWrapper,
            ICommonUtils commonUtils, IServerConfigurationUpdated serverConfigurationUpdated)
        {
            _runProgram = runProgram ?? throw new ArgumentNullException(nameof(runProgram));
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
            _commonUtils = commonUtils ?? throw new ArgumentNullException(nameof(commonUtils));
            _serverConfigurationUpdated = serverConfigurationUpdated ?? throw new ArgumentNullException(nameof(serverConfigurationUpdated));
            InitializeComponent();
            UpdateUI();
        }

        protected override string SectionName => "ServerConfig";

        protected override void LoadResources()
        {
            Text = GSend.Language.Resources.ServerConfiguration;
            columnHeaderServer.Text = Languages.LanguageStrings.Server;
            columnHeaderPort.Text = GSend.Language.Resources.Port;
            columnHeaderProtocol.Text = GSend.Language.Resources.Protocol;
            columnHeaderActive.Text = GSend.Language.Resources.IsActive;
            lblServer.Text = Languages.LanguageStrings.Server;
            lblPort.Text = Languages.LanguageStrings.Port;
            btnAdd.Text = Languages.LanguageStrings.Add;
            btnDelete.Text = Languages.LanguageStrings.Delete;
            btnClose.Text = Languages.LanguageStrings.AppClose;
            base.LoadResources();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Application.DoEvents();

            LoadAllServers();
        }

        #region IServerAvailability

        public void UpdateServerAvailability(bool isAvailable, ListViewItem item)
        {
            if (lvServers.InvokeRequired)
            {
                if (!this.IsDisposed)
                    Invoke(() => UpdateServerAvailability(isAvailable, item));

                return;
            }

            item.SubItems[3].Text = isAvailable ? Languages.LanguageStrings.Yes : Languages.LanguageStrings.No;
        }

        #endregion IServerAvailability

        private void UpdateUI()
        {
            btnAdd.Enabled = !String.IsNullOrEmpty(txtServer.Text) &&
                !String.IsNullOrEmpty(txtPort.Text);
            btnDelete.Enabled = lvServers.SelectedItems.Count > 0;
        }

        private void lvServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();

            if (lvServers.SelectedItems.Count != 1)
            {
                txtPort.Text = String.Empty;
                txtServer.Text = String.Empty;
                return;
            }

            ListViewItem item = lvServers.SelectedItems[0];

            Uri uri = item.Tag as Uri;

            if (uri == null)
                return;

            txtServer.Text = uri.Host;
            txtPort.Text = uri.Port.ToString();

            rbHttp.Checked = uri.Scheme == Uri.UriSchemeHttp;
            rbHttps.Checked = uri.Scheme == Uri.UriSchemeHttps;
            btnDelete.Enabled = item != null;
            UpdateUI();
        }

        private void LoadAllServers()
        {
            using (MouseControl mc = MouseControl.ShowWaitCursor(this))
            {
                lvServers.BeginUpdate();
                try
                {
                    lvServers.Items.Clear();

                    if (_commonUtils.GetGSendCS(out string gSendCS))
                    {
                        string[] servers = _runProgram.Run(gSendCS, "Server Show").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                        for (int i = 2; i < servers.Length; i++)
                        {
                            string line = servers[i];

                            if (Uri.TryCreate(line, UriKind.Absolute, out Uri uri))
                            {
                                ListViewItem lvItem = new(uri.Host);
                                lvItem.SubItems.Add(uri.Port.ToString());
                                lvItem.SubItems.Add(uri.Scheme);
                                lvItem.SubItems.Add(GSend.Language.Resources.ServerValidating);
                                lvItem.Tag = uri;
                                lvServers.Items.Add(lvItem);

                                ServerAvailabilityThread serverAvailability = new(_gSendApiWrapper, this, lvItem);
                                ThreadManager.ThreadStart(serverAvailability, $"Server Availability: {uri}", System.Threading.ThreadPriority.BelowNormal);
                            }
                        }
                    }
                }
                finally
                {
                    lvServers.EndUpdate();
                }
            }

            UpdateUI();
            Application.DoEvents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ushort.TryParse(txtPort.Text, out ushort port))
            {
                ShowError(GSend.Language.Resources.InvalidServerPort, GSend.Language.Resources.InvalidServerPortDesc);
                return;
            }

            bool isSecure = rbHttps.Checked;
            string server = txtServer.Text;
            UriBuilder uriBuilder = new()
            {
                Scheme = isSecure ? Uri.UriSchemeHttp : Uri.UriSchemeHttps,
                Port = port,
                Host = server
            };

            if (!_gSendApiWrapper.CanConnect(uriBuilder.Uri) &&
                !ShowWarningQuestion(GSend.Language.Resources.ServerAdd, GSend.Language.Resources.ServerConnectFailContinue))
            {
                return;
            }

            string cmdLine = $"Server Add -a:{uriBuilder.Host} -p:{port} -s:{isSecure.ToString().ToLowerInvariant()}";
            RunGSendCSAndReloadServers(cmdLine);
            _serverConfigurationUpdated.ServerConfigurationUpdated();
        }

        private void RunGSendCSAndReloadServers(string cmdLine)
        {
            if (_commonUtils.GetGSendCS(out string gSendCs))
            {
                string responseStrings = _runProgram.Run(gSendCs, cmdLine);

                if (responseStrings.Length == 0)
                {
                    return;
                }

                LoadAllServers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvServers.SelectedItems.Count == 0)
                return;

            ListViewItem item = lvServers.SelectedItems[0];

            Uri uri = item.Tag as Uri;

            if (uri == null)
                return;

            if (ShowQuestion(GSend.Language.Resources.ServerDelete, String.Format(GSend.Language.Resources.ServerDeleteQuestion, uri.Host, uri.Port)))
            {
                bool isSecure = rbHttps.Checked;
                string cmdLine = $"Server Delete -a:{uri.Host} -p:{uri.Port} -s:{isSecure.ToString().ToLowerInvariant()}";
                RunGSendCSAndReloadServers(cmdLine);
                lvServers_SelectedIndexChanged(sender, e);
                _serverConfigurationUpdated.ServerConfigurationUpdated();
            }
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
