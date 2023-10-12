using System;
using System.Windows.Forms;

namespace GSendDesktop.Internal
{
    public sealed record MouseControl : IDisposable
    {
        private Form _parent;
        private Cursor _cursor;

        public void Dispose()
        {
            Cursor.Current = _cursor;
            _parent.Cursor = _cursor;
            GC.SuppressFinalize(this);
        }

        public static MouseControl ShowWaitCursor(Form parent)
        {
            MouseControl Result = new()
            {
                _parent = parent ?? throw new ArgumentNullException(nameof(parent)),
                _cursor = parent.Cursor,
            };

            Result._parent.Cursor = Cursors.WaitCursor;
            Cursor.Current = Cursors.WaitCursor;
            return Result;
        }
    }
}
