using System.ComponentModel;
using System.Windows.Forms;

namespace GSendDesktop.Controls
{
    public partial class JogControl : UserControl
    {
        public JogControl()
        {
            InitializeComponent();
            LoadResources();
        }

        public void LoadResources()
        {
            selectionSteps.GroupName = GSend.Language.Resources.Steps;
            selectionFeed.GroupName = GSend.Language.Resources.FeedRate;
        }

        [Browsable(true)]
        public int FeedMaximum
        {
            get => selectionFeed.Maximum;
            set => selectionFeed.Maximum = value;
        }

        [Browsable(true)]
        public int FeedMinimum
        {
            get => selectionFeed.Minimum;
            set => selectionFeed.Minimum = value;
        }

        [Browsable(true)]
        public int StepsMaximum
        {
            get => selectionSteps.Maximum;
            set => selectionSteps.Maximum = value;
        }

        [Browsable(true)]
        public int StepsMinimum
        {
            get => selectionSteps.Minimum;
            set => selectionSteps.Minimum = value;
        }
    }
}
