using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
