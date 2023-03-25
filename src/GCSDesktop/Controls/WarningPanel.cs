using System;
using System.Windows.Forms;

namespace GSendDesktop.Controls
{
    public partial class WarningPanel : UserControl
    {
        public WarningPanel()
        {
            InitializeComponent();
        }

        public WarningPanel(string message)
            : this()
        {
            lblMessage.Text = message;
        }

        public event EventHandler WarningClose;

        private void imageClose_Click(object sender, System.EventArgs e)
        {
            WarningClose?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
