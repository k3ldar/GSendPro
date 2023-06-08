using System.Drawing;

namespace GSendControls
{
    public partial class AboutBox : BaseForm
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        public static void ShowAboutBox(string name, Icon icon)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.Text = name;
                aboutBox.Icon = icon;

                aboutBox.ShowDialog();
            }
        }
    }
}
