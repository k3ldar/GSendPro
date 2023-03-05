namespace GSendShared
{
    public interface IGCodeCommand
    {
        char Command { get; }

        string CommandValueString { get; }

        decimal CommandValue { get; }

        string Comment { get; }

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

        CommandStatus Status { get; set; }

        string GetCommand();
    }
}
