using System.Drawing;
using System.Windows.Forms;

namespace GSendControls.Abstractions
{
    /// <summary>
    /// Text Editor interface
    /// </summary>
    public interface ITextEditor
    {
        IWin32Window Parent { get; }

        Control ParentControl { get; }

        Rectangle ParentRectangle { get; }

        Point ParentDesktopLocation { get; }

        void ShowGoToDialog();

        void ShowFindDialog();

        void ShowReplaceDialog();

        int LineCount { get; }

        string Text { get; set; }

        int SelectionStart { get; }

        int SelectionLength { get; }

        Rectangle Position { get; }
    }
}
