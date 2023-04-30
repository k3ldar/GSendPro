namespace GSendShared
{
    public interface IGCodeLineInfo
    {
        string GCode { get; }

        decimal FeedRate { get; }

        decimal SpindleSpeed { get; }

        bool SpindleActive { get; }

        CommandAttributes Attributes { get; }

        string Comments { get; }
    }
}
