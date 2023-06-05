using System;
using System.Collections.Generic;
using System.IO;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockGCodeAnalyses : IGCodeAnalyses
    {
        public IReadOnlyList<IGCodeCommand> Commands => new List<IGCodeCommand>();

        public decimal SafeZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal HomeZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal TotalDistance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan TotalTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UnitOfMeasurement UnitOfMeasurement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Tools { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal FeedX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal FeedZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Layers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal MaxLayerDepth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CommentCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SubProgramCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CoordinateSystems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string JobName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FileInfo FileInformation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FileCRC { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AnalysesOptions AnalysesOptions => throw new NotImplementedException();

        public IReadOnlyDictionary<ushort, IGCodeVariable> Variables => throw new NotImplementedException();

        public string VariablesUsed => throw new NotImplementedException();

        public IReadOnlyList<string> Errors => throw new NotImplementedException();

        public IReadOnlyList<string> Warnings => throw new NotImplementedException();

        public decimal MaxX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal MaxY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IReadOnlyList<IGCodeCommand> AllCommands => throw new NotImplementedException();

        public void AddOptions(AnalysesOptions options)
        {
            throw new NotImplementedException();
        }

        public List<IGCodeLine> AllLines(out int lineCount)
        {
            throw new NotImplementedException();
        }

        public void Analyse()
        {
            throw new NotImplementedException();
        }

        public void Analyse(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<IGCodeLine> Lines(out int lineCount)
        {
            throw new NotImplementedException();
        }
    }
}
