namespace GSendShared
{
    public interface IGCodeLine
    {
        LineStatus Status { get; set; }

        List<IGCodeCommand> Commands { get; }

        string GetGCode();

    }
}
