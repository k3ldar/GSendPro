using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendApi;

using GSendControls.Abstractions;
using GSendControls.Threads;

using GSendDesktop.Internal;

using GSendShared;

using Shared.Classes;

namespace GSendControls.Forms
{
    public partial class FrmConfigureServer : BaseForm, IServerAvailability
    {
        private readonly IRunProgram _runProgram;
        private readonly IGSendApiWrapper _gSendApiWrapper;

        public FrmConfigureServer(IRunProgram runProgram, IGSendApiWrapper gSendApiWrapper)
        {
            _runProgram = runProgram ?? throw new ArgumentNullException(nameof(runProgram));
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
            InitializeComponent();
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
            using (MouseControl mc = MouseControl.ShowWaitCursor(this))
            {
                if (GetGSendCS(out string gSendCS))
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
                            lvItem.SubItems.Add(Languages.LanguageStrings.AppValidating);
                            lvItem.Tag = uri;
                            lvServers.Items.Add(lvItem);

                            ServerAvailabilityThread serverAvailability = new(_gSendApiWrapper, this, lvItem);
                            ThreadManager.ThreadStart(serverAvailability, $"Server Availability: {uri}", System.Threading.ThreadPriority.BelowNormal);
                        }
                    }
                }
            }

            Application.DoEvents();
        }

        private static bool GetGSendCS(out string gSendCS)
        {
#if DEBUG
            gSendCS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("-windows", String.Empty), "GSendCS.exe");
#else
            gSendCS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GSendCS.exe");

#endif
            if (File.Exists(gSendCS))
                return true;

            return false;
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
    }
}
