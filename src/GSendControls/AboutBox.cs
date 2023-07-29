using System.Diagnostics;
using System.Drawing;
using System.Reflection;

using GSendShared;

namespace GSendControls
{
    public partial class AboutBox : BaseForm
    {
        public AboutBox()
        {
            InitializeComponent();

            lblVerNo.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        protected override void LoadResources()
        {
            btnOK.Text = GSend.Language.Resources.OK;
            lblVersion.Text = Languages.LanguageStrings.Version;
            lblCopyright.Text = GSend.Language.Resources.Copyright;
        }

        public static void ShowAboutBox(string name, Icon icon)
        {
            using (AboutBox aboutBox = new())
            {
                aboutBox.Text = name;
                aboutBox.Icon = icon;
                aboutBox.imageIcon.Image = icon.ToBitmap();
                aboutBox.lblProgName.Text = name;
                aboutBox.ShowDialog();
            }
        }

        private void lnkHomePage_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psi = new()
            {
                FileName = Constants.HomeWebsite,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}
