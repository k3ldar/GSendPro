﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace GSendControls
{
    public class BaseForm : Form
    {
        public BaseForm()
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadResources();
            CreateContextMenu();
            LoadSettings();
            base.OnLoad(e);
            UpdateEnabledState();
        }

        protected void ShowInformation(string title, string message)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowError(string title, string message)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected bool ShowWarningQuestion(string title, string message)
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        protected bool ShowQuestion(string title, string message)
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        protected virtual void CreateContextMenu()
        {

        }

        protected virtual string SectionName { get; }

        protected virtual void UpdateEnabledState()
        {

        }

        protected virtual void LoadResources()
        {

        }

        protected virtual void LoadSettings()
        {
            string screenName = DesktopSettings.ReadValue<string>(SectionName, "Screen", Screen.FromControl(this).DeviceName);

            Screen screen = Screen.AllScreens.FirstOrDefault(s => s.DeviceName.Equals(screenName));

            if (screen != null)
            {
                this.Location = screen.WorkingArea.Location;
            }

            Width = DesktopSettings.ReadValue<int>(SectionName, nameof(Width), Width);
            Height = DesktopSettings.ReadValue<int>(SectionName, nameof(Height), Height);
            Top = DesktopSettings.ReadValue<int>(SectionName, nameof(Top), Top);
            Left = DesktopSettings.ReadValue<int>(SectionName, nameof(Left), Left);
        }

        protected void LoadSettings(SplitContainer splitContainer)
        {
            splitContainer.SplitterDistance = DesktopSettings.ReadValue<int>(SectionName, splitContainer.Name, splitContainer.SplitterDistance);
        }

        protected void LoadSettings(ListView listView)
        {
            foreach (ColumnHeader column in listView.Columns)
            {
                column.Width = DesktopSettings.ReadValue<int>(SectionName, $"Column{column.Text}", column.Width);
            }
        }

        protected void LoadSettings(TabControl tabControl)
        {
            tabControl.SelectedIndex = DesktopSettings.ReadValue<int>(SectionName, $"{tabControl.Name}{nameof(tabControl.SelectedIndex)}", tabControl.SelectedIndex);
        }

        protected virtual void SaveSettings()
        {
            if (WindowState == FormWindowState.Normal)
            {
                DesktopSettings.WriteValue(SectionName, nameof(Screen), Screen.FromControl(this).DeviceName);
                DesktopSettings.WriteValue(SectionName, nameof(Width), Width);
                DesktopSettings.WriteValue(SectionName, nameof(Height), Height);
                DesktopSettings.WriteValue(SectionName, nameof(Top), Top);
                DesktopSettings.WriteValue(SectionName, nameof(Left), Left);
            }
        }

        protected void SaveSettings(SplitContainer splitContainer)
        {
            DesktopSettings.WriteValue(SectionName, splitContainer.Name, splitContainer.SplitterDistance);
        }

        protected void SaveSettings(ListView listView)
        {
            foreach (ColumnHeader column in listView.Columns)
            {
                DesktopSettings.WriteValue(SectionName, $"Column{column.Text}", column.Width);
            }
        }

        protected void SaveSettings(TabControl tabControl)
        {
            DesktopSettings.WriteValue(SectionName, $"{tabControl.Name}{nameof(tabControl.SelectedIndex)}", tabControl.SelectedIndex);
        }

        protected void RaiseServerConnected()
        {
            OnServerConnected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnServerConnected;
    }
}
