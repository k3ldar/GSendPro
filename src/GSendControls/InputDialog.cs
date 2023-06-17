using System;
using System.Drawing;
using System.Windows.Forms;

namespace GSendControls
{
    public delegate string InputBoxValidation(string errorMessage);

    public static class InputDialog
    {
        public static bool Show(IWin32Window parent, string title, string promptText, ref string value)
        {
            return Show(parent, title, promptText, ref value, null, null);
        }

        public static bool Show(IWin32Window parent, string title, string promptText, ref string value, InputBoxValidation validation, string validationTitle)
        {
            using Form form = new();
            using Label label = new();
            using TextBox textBox = new();
            using Button buttonOk = new();
            using Button buttonCancel = new();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = GSend.Language.Resources.OK;
            buttonCancel.Text = GSend.Language.Resources.Cancel;
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            if (validation != null)
            {
                form.FormClosing += delegate (object sender, FormClosingEventArgs e)
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        string errorText = validation(textBox.Text);

                        if (e.Cancel = (!String.IsNullOrEmpty(errorText)))
                        {
                            MessageBox.Show(form, errorText, validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox.Focus();
                        }
                    }
                };
            }

            DialogResult dialogResult = form.ShowDialog(parent);
            value = textBox.Text;

            return dialogResult == DialogResult.OK;
        }
    }
}