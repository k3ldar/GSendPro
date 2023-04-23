using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    public partial class WarningContainer : UserControl
    {
        public WarningContainer()
        {
            InitializeComponent();
        }

        public void AddWarningPanel(InformationType informationType, string message)
        {
            WarningPanel warningPanel = new WarningPanel(informationType, message);
            warningPanel.Width = flowLayoutWarningErrors.ClientSize.Width - 10;
            warningPanel.WarningClose += WarningPanel_WarningClose;
            flowLayoutWarningErrors.Controls.Add(warningPanel);
            SortItems();
            Visible = flowLayoutWarningErrors.Controls.Count > 0;
            ResetLayoutWarningErrorSize();
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void Clear(bool forceAll)
        {
            if (forceAll)
            {
                flowLayoutWarningErrors.Controls.Clear();
            }
            else
            {
                for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
                {
                    WarningPanel panel = flowLayoutWarningErrors.Controls[i] as WarningPanel;

                    if (panel != null && panel.InformationType != InformationType.ErrorKeep)
                    {
                        flowLayoutWarningErrors.Controls.Remove(panel);
                    }
                }
            }
        }

        public int WarningCount()
        {
            return 0;
        }

        public int ErrorCount()
        {
            return 0;
        }

        public int InformationCount()
        {
            return 0;
        }

        public int TotalCount()
        {
            return flowLayoutWarningErrors.Controls.Count;
        }

        public bool Contains(InformationType informationType, string message)
        {
            for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
            {
                WarningPanel panel = flowLayoutWarningErrors.Controls[i] as WarningPanel;

                if (panel != null && panel.InformationType.Equals(informationType) && panel.InformationText.Equals(message))
                {
                    return true;
                }
            }

            return false;
        }

        public event EventHandler OnUpdate;

        private static int Comparison(WarningPanel a, WarningPanel b)
        {
            if (a == null)
            {
                if (b == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (b == null)
                    return 1;

                if (a.InformationType.Equals(b.InformationType))
                    return 0;
            }


            return ((int)a.InformationType).CompareTo(((int)b.InformationType));
        }

        private void SortItems()
        {
            List<WarningPanel> items = new();

            foreach (Control control in flowLayoutWarningErrors.Controls)
            {
                if (control.GetType().Equals(typeof(WarningPanel)))
                    items.Add(control as WarningPanel);
            }

            items.Sort(Comparison);

            for (int i = 0; i < items.Count; i++)
            {
                Control control = items[i];
                flowLayoutWarningErrors.Controls.SetChildIndex(control, i);
            }
        }

        private void WarningPanel_WarningClose(object sender, EventArgs e)
        {
            if (sender is WarningPanel warningPanel)
            {
                warningPanel.WarningClose -= WarningPanel_WarningClose;
                flowLayoutWarningErrors.Controls.Remove(warningPanel);
            }

            ResetAfterRemove();
        }

        private void ResetAfterRemove()
        {
            Visible = flowLayoutWarningErrors.Controls.Count > 0;
            ResetLayoutWarningErrorSize();
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        private void ResetLayoutWarningErrorSize()
        {
            if (flowLayoutWarningErrors.Controls.Count < 2)
                Height = 27;
            else
                Height = 48;

            foreach (Control control in flowLayoutWarningErrors.Controls)
            {
                control.Width = flowLayoutWarningErrors.ClientSize.Width;
            }
        }

        internal void ClearAlarm()
        {
            for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutWarningErrors.Controls[i] is WarningPanel warningPanel)
                {
                    if (warningPanel.InformationType.Equals(InformationType.Alarm))
                    {
                        flowLayoutWarningErrors.Controls.Remove(warningPanel);
                        ResetAfterRemove();
                    }
                }
            }
        }

        private void WarningContainer_SizeChanged(object sender, EventArgs e)
        {
            ResetAfterRemove();
        }
    }
}
