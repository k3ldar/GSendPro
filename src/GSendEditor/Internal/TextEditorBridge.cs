using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendControls.Abstractions;

namespace GSendEditor.Internal
{
    internal class TextEditorBridge : ITextEditor
    {
        private readonly FastColoredTextBoxNS.FastColoredTextBox _textBox;

        public TextEditorBridge(FastColoredTextBoxNS.FastColoredTextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        #region ITextEditor

        public void ShowGoToDialog()
        {
            _textBox.ShowGoToDialog();
        }

        public int LineCount => _textBox.Lines.Count;

        #endregion ITextEditor
    }
}
