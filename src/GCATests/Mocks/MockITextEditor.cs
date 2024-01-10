using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

using GSendControls.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockITextEditor : ITextEditor
    {
        public string Text { get; set; } = String.Empty;

        public int LineCount { get; set; } = 23;

        public bool ShowGotoDialogCalled { get; private set; }

        public bool ShowFindDialogCalled { get; private set; }

        public bool ShowReplaceDialogCalled { get; private set; }

        public IWin32Window Parent => throw new NotImplementedException();

        public Rectangle ParentRectangle => throw new NotImplementedException();

        public int SelectionStart => throw new NotImplementedException();

        public int SelectionLength => throw new NotImplementedException();

        public Rectangle Position => throw new NotImplementedException();

        public Control ParentControl => throw new NotImplementedException();

        public Point ParentDesktopLocation => throw new NotImplementedException();

        public void ShowFindDialog()
        {
            ShowFindDialogCalled = true;
        }

        public void ShowGoToDialog()
        {
            ShowGotoDialogCalled = true;
        }

        public void ShowReplaceDialog()
        {
            ShowReplaceDialogCalled = true;
        }
    }
}
