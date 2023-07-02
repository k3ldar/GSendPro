namespace GSendShared
{
    public interface IGCodeCommand
    {
        int LineNumber { get; }

        int MasterLineNumber { get; }

        char Command { get; }

        string CommandValueString { get; }

        decimal CommandValue { get; }

        string Comment { get; }

        List<IGCodeVariableBlock> VariableBlocks { get; }

        int Index { get; }

        decimal CurrentX { get; }

        decimal CurrentY { get; }

        decimal CurrentZ { get; }

        decimal CurrentFeedRate { get; }

        decimal FeedRate { get; }

        decimal Distance { get; }

        TimeSpan Time { get; }

        decimal SpindleSpeed { get; }

        bool SpindleOn { get; }

        bool CoolantEnabled { get; }

        CommandAttributes Attributes { get; }

        /// <summary>
        /// Subprograms will produce their own sub analyses
        /// </summary>
        IGCodeAnalyses SubAnalyses { get; }

        /// <summary>
        /// returns the comment without the comment identifiers
        /// </summary>
        string CommentStripped(bool replaceVariables);

        string GetCommand();

        IGCodeCommand PreviousCommand { get; }

        IGCodeCommand NextCommand { get; }
    }
}
