namespace GSendShared.Models
{
    public sealed class LineStatusUpdateModel
    {
        public LineStatusUpdateModel(int lineNumber, int masterLineNumber, LineStatus status)
        {
            LineNumber = lineNumber;
            MasterLineNumber = masterLineNumber;
            Status = status;
        }

        public int LineNumber { get; }

        public int MasterLineNumber { get; }

        public LineStatus Status { get; }
    }
}
