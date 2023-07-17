using System;
using System.Collections.Generic;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockGCodeLine : IGCodeLine
    {

        public LineStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<IGCodeCommand> Commands => throw new NotImplementedException();

        public bool IsCommentOnly => false;

        public string GetGCode()
        {
            throw new NotImplementedException();
        }

        public IGCodeLine GetGCode(int feedRate)
        {
            throw new NotImplementedException();
        }

        public IGCodeLineInfo GetGCodeInfo()
        {
            throw new NotImplementedException();
        }

        public int LineNumber { get; }

        public int MasterLineNumber { get; }

    }
}
