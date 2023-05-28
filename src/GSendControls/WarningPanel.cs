using System;
using System.Drawing;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public partial class WarningPanel : UserControl
    {
        public WarningPanel()
        {
            InitializeComponent();
        }

        public WarningPanel(InformationType informationType, string message)
            : this()
        {
            lblMessage.Text = message;
            InformationType = informationType;

            switch (informationType)
            {
                case InformationType.Warning:
                    panel1.BackColor = Color.Orange;
                    break;
                case InformationType.Alarm:
                case InformationType.Error:
                case InformationType.ErrorKeep:
                    panel1.BackColor = Color.Red;
                    break;
                case InformationType.Information:
                    panel1.BackColor = Color.White;
                    break;
            }

            imageWarning.Image = ImageList.Images[(int)informationType];
        }

        public InformationType InformationType { get; }

        public string InformationText => lblMessage.Text;

        public event EventHandler WarningClose;

        private void imageClose_Click(object sender, System.EventArgs e)
        {
            WarningClose?.Invoke(this, EventArgs.Empty);
        }

        public Image GetImageForInformationType(InformationType informationType)
        {
            return ImageList.Images[(int)informationType];
        }
    }
}
