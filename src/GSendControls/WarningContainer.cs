﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public partial class WarningContainer : UserControl
    {
        public WarningContainer()
        {
            InitializeComponent();
        }

        public void AddWarningPanel(InformationType informationType, string message)
        {
            if (InvokeRequired)
            {
                Invoke(AddWarningPanel, informationType, message);
                return;
            }

            WarningPanel warningPanel = new(informationType, message)
            {
                Width = flowLayoutWarningErrors.ClientSize.Width - 10
            };
            warningPanel.WarningClose += WarningPanel_WarningClose;
            flowLayoutWarningErrors.Controls.Add(warningPanel);
            SortItems();
            Visible = flowLayoutWarningErrors.Controls.Count > 0;
            ResetLayoutWarningErrorSize();
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        public void Clear(bool forceAll)
        {
            if (InvokeRequired)
            {
                Invoke(Clear, forceAll);
                return;
            }

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

            ResetAfterRemove();
        }

        public int WarningCount()
        {
            return GetCountOfType(InformationType.Warning);
        }

        public int ErrorCount()
        {
            return GetCountOfType(InformationType.Alarm) + GetCountOfType(InformationType.Error) + GetCountOfType(InformationType.ErrorKeep);
        }

        public int InformationCount()
        {
            return GetCountOfType(InformationType.Information);
        }

        private int GetCountOfType(InformationType informationType)
        {
            int Result = 0;

            for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutWarningErrors.Controls[i] is WarningPanel panel &&
                    panel.InformationType.Equals(informationType))
                {
                    Result++;
                }
            }

            return Result;
        }

        public int TotalCount()
        {
            return flowLayoutWarningErrors.Controls.Count;
        }

        public bool Contains(InformationType informationType, string message)
        {
            for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutWarningErrors.Controls[i] is WarningPanel panel &&
                    panel.InformationType.Equals(informationType) &&
                    panel.InformationText.Equals(message))
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


            return ((int)a.InformationType).CompareTo((int)b.InformationType);
        }

        private void SortItems()
        {
            List<WarningPanel> items = [];

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

        public void ResetLayoutWarningErrorSize()
        {
            if (flowLayoutWarningErrors.Controls.Count < 2)
            {
                Height = 28;
                flowLayoutWarningErrors.AutoScroll = false;
                flowLayoutWarningErrors.VerticalScroll.Visible = false;
            }
            else
            {
                Height = 48;
                flowLayoutWarningErrors.AutoScroll = true;
                flowLayoutWarningErrors.VerticalScroll.Visible = false;
            }

            foreach (Control control in flowLayoutWarningErrors.Controls)
            {
                control.Width = flowLayoutWarningErrors.ClientSize.Width - 1;
            }
        }

        public void ClearAlarm()
        {
            for (int i = flowLayoutWarningErrors.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutWarningErrors.Controls[i] is WarningPanel warningPanel &&
                    warningPanel.InformationType.Equals(InformationType.Alarm))
                {
                    flowLayoutWarningErrors.Controls.Remove(warningPanel);
                    ResetAfterRemove();
                }
            }
        }

        private void WarningContainer_SizeChanged(object sender, EventArgs e)
        {
            ResetAfterRemove();
        }
    }
}
