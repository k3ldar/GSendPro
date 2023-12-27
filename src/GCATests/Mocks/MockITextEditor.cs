using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendControls.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockITextEditor : ITextEditor
    {
        public int LineCount { get; set; } = 23;

        public bool ShowGotoDialogCalled { get; private set; }

        public void ShowGoToDialog()
        {
            ShowGotoDialogCalled = true;
        }
    }
}
