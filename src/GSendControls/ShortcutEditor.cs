using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendShared;
using GSendShared.Interfaces;


namespace GSendControls
{
    public partial class ShortcutEditor : BaseForm
    {
        private readonly ShortcutHandler _shortcutHandler;
        private IShortcut _selectedShortcut;
        private readonly List<int> _selectedKeys = new();
        private ListViewItem _selectedItem;

        public ShortcutEditor()
        {
            InitializeComponent();

            _shortcutHandler = new ShortcutHandler
            {
                RegisterKeyCombo = true
            };
            _shortcutHandler.OnKeyComboUp += ShortcutHandler_OnKeyComboUp;
            _shortcutHandler.OnKeyComboDown += ShortcutHandler_OnKeyComboDown;
        }

        public ShortcutEditor(List<IShortcut> shortcutImplementation)
            : this()
        {
            ShortcutImplementation = shortcutImplementation ?? throw new ArgumentNullException(nameof(shortcutImplementation));
            lvShortcuts.Items.Clear();

            foreach (IShortcut shortcut in ShortcutImplementation)
            {
                _shortcutHandler.AddKeyCombo(shortcut.Name, shortcut.DefaultKeys);

                ListViewItem item = new(shortcut.GroupName);
                item.SubItems.Add(shortcut.Name);
                item.SubItems.Add(GetSelectedKeys(shortcut.DefaultKeys, false));
                item.Tag = shortcut;
                lvShortcuts.Items.Add(item);
            }
        }

        public static bool ShowDialog(Form parent, ref List<IShortcut> shortcutImplementation)
        {
            using (ShortcutEditor scEditor = new(shortcutImplementation))
            {
                if (scEditor.ShowDialog(parent) == DialogResult.OK)
                {
                    shortcutImplementation = scEditor.ShortcutImplementation;
                    return true;
                }
            }

            return false;
        }

        public List<IShortcut> ShortcutImplementation { get; }

        protected override string SectionName => nameof(ShortcutEditor);

        protected override void UpdateEnabledState()
        {
            base.UpdateEnabledState();

        }

        protected override void LoadResources()
        {
            Text = GSend.Language.Resources.ShortcutEditor;
            columnHeaderGroupName.Text = GSend.Language.Resources.ShortcutGroupName;
            columnHeaderShortcutName.Text = GSend.Language.Resources.ShortcutName;
            columnHeaderKeyCombo.Text = GSend.Language.Resources.ShortcutKeyCombination;
            btnClose.Text = GSend.Language.Resources.Close;
            btnRemove.Text = GSend.Language.Resources.ShortcutRemove;
            btnUpdate.Text = GSend.Language.Resources.ShortcutUpdate;
            lblShortcut.Text = GSend.Language.Resources.ShortcutCombination;
            lblInUse.Text = GSend.Language.Resources.ShortcutInUse;
        }

        private void ShortcutEditor_KeyDown(object sender, KeyEventArgs e)
        {
            _shortcutHandler.KeyDown(e);
        }

        private void ShortcutEditor_KeyUp(object sender, KeyEventArgs e)
        {
            _shortcutHandler.KeyUp(e);
        }

        private static string GetSelectedKeys(List<int> keys, bool addSelectMessage)
        {
            StringBuilder Result = new(200);

            for (int i = 0; i < keys.Count; i++)
            {
                Keys key = (Keys)keys[i];

                Result.Append(key.ToString());

                if (i < keys.Count - 1)
                    Result.Append(" + ");
            }

            if (addSelectMessage && Result.Length == 0)
            {
                Result.Append(GSend.Language.Resources.ShortcutPressCombination);
            }

            return Result.ToString();
        }

        private void lvShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedItem = lvShortcuts.SelectedItems == null || lvShortcuts.SelectedItems.Count == 0 ? null : lvShortcuts.SelectedItems[0];

            if (_selectedItem != null && _selectedItem.Tag is IShortcut shortcut)
            {
                _selectedShortcut = shortcut;
                txtKeyCombination.Text = GetSelectedKeys(_selectedShortcut.DefaultKeys, true);
                _selectedKeys.AddRange(shortcut.DefaultKeys);
                txtKeyCombination.Focus();
            }
            else
            {
                txtKeyCombination.Text = String.Empty;
                _selectedShortcut = null;
                _selectedKeys.Clear();
            }
                
            lblInUse.Visible = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = txtKeyCombination.Text != GSend.Language.Resources.ShortcutPressCombination;
            UpdateEnabledState();
        }


        private void ShortcutHandler_OnKeyComboDown(object sender, ShortcutArgs e)
        {

        }

        private void ShortcutHandler_OnKeyComboUp(object sender, ShortcutArgs e)
        {
            txtKeyCombination.Text = GetSelectedKeys(e.Keys, true);
            btnUpdate.Enabled = !_shortcutHandler.IsKeyComboRegistered(e.Keys) && txtKeyCombination.Text != GSend.Language.Resources.ShortcutPressCombination;
            lblInUse.Visible = !btnUpdate.Enabled && txtKeyCombination.Text != _selectedItem.SubItems[2].Text;
            _selectedKeys.Clear();
            _selectedKeys.AddRange(e.Keys);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedKeys != null && _selectedShortcut != null)
            {
                _selectedShortcut.DefaultKeys.Clear();
                _selectedShortcut.DefaultKeys.AddRange(_selectedKeys);
                _selectedItem.SubItems[2].Text = GetSelectedKeys(_selectedKeys, false);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (_selectedShortcut != null)
            {
                _selectedShortcut.DefaultKeys.Clear();
                txtKeyCombination.Text = GSend.Language.Resources.ShortcutPressCombination;
                _selectedItem.SubItems[2].Text = String.Empty;
                lblInUse.Visible = false;
            }
        }
    }
}
