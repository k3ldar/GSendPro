namespace GSendShared.Abstractions
{
    public interface IGCodeParser
    {
        IGCodeAnalyses Parse(string gCodeCommands);
    }
}