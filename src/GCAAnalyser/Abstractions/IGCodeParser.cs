
namespace GCAAnalyser.Abstractions
{
    public interface IGCodeParser
    {
        IReadOnlyList<GCodeCommand> Commands { get; }

        void Parse(string gCodeCommands);
    }
}