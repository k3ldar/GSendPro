using GSendShared.Abstractions;

namespace GSendShared
{
    public interface IGCodeLine
    {
        LineStatus Status { get; set; }

        List<IGCodeCommand> Commands { get; }

        string GetGCode();

        IGCodeLine GetGCode(int feedRate);

        IGCodeLineInfo GetGCodeInfo();

        bool IsCommentOnly { get; }
    }
}
