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
            _parent.Cursor = _cursor;
        }

        public static MouseControl ShowWaitCursor(Form parent)
        {
            MouseControl Result = new MouseControl()
            {
                _parent = parent ?? throw new ArgumentNullException(nameof(parent)),
                _cursor = parent.Cursor,
            };

            Result._parent.Cursor = Cursors.WaitCursor;
            return Result;
        }
    }
}
