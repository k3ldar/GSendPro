using GSendControls.Abstractions;

namespace GSendEditor.Internal
{
    internal class TextEditorBridge : ITextEditor
    {
        private readonly Form _parentForm;
        private readonly FastColoredTextBoxNS.FastColoredTextBox _textBox;

        public TextEditorBridge(Form parentForm, FastColoredTextBoxNS.FastColoredTextBox textBox)
        {
            _parentForm = parentForm ?? throw new ArgumentNullException(nameof(parentForm));
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        #region ITextEditor

        public void ShowGoToDialog()
        {
            _textBox.ShowGoToDialog();
            _textBox.Focus();
        }

        public void ShowFindDialog()
        {
            _textBox.ShowFindDialog();
        }

        public void ShowReplaceDialog()
        {
            _textBox.ShowReplaceDialog();
        }

        public int LineCount => _textBox.Lines.Count;

        public string Text { get => _textBox.Text; set => _textBox.Text = value; }

        public int SelectionStart => _textBox.SelectionStart;

        public int SelectionLength => _textBox.SelectionLength;

        public Rectangle Position => new(_textBox.Location, _textBox.Size);

        public IWin32Window Parent => _textBox.Parent;

        public Rectangle ParentRectangle => _parentForm.ClientRectangle;

        public Point ParentDesktopLocation => _parentForm.PointToScreen(_parentForm.DesktopLocation);

        public Control ParentControl => _textBox.Parent;

        #endregion ITextEditor
    }
}
