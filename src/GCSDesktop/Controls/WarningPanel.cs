using System;
using System.Drawing;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    public partial class WarningPanel : UserControl
    {
        public WarningPanel()
        {
            InitializeComponent();
        }

        public InformationType InformationType { get; }

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
                    panel1.BackColor = Color.Red;
                    break;
                case InformationType.Information:
                    panel1.BackColor = Color.White;
                    break;
            }

            imageWarning.Image = imageList1.Images[(int)informationType];
        }

        public event EventHandler WarningClose;

        private void imageClose_Click(object sender, System.EventArgs e)
        {
            WarningClose?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
