using System;
using System.Windows.Forms;

namespace GSendControls
{
    public delegate void ToolTipEventHandler(object sender, ToolTipEventArgs e);

    public class ToolTipEventArgs : EventArgs
    {
        public bool ShowBaloon { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public ToolTipIcon Icon { get; set; }

        public bool AllowShow { get; set; }

        public ListViewItem ListViewItem { get; set; }

        public ToolTipEventArgs(ListViewItem listViewItem)
            : this()
        {
            ListViewItem = listViewItem;
        }

        public ToolTipEventArgs()
        {
            AllowShow = true;
            ShowBaloon = false;
            Icon = ToolTipIcon.None;
            Title = string.Empty;
            Text = string.Empty;
            ListViewItem = null;
        }
    }
}
