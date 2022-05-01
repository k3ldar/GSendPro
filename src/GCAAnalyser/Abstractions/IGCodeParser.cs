
namespace GCAAnalyser.Abstractions
{
    public interface IGCodeParser
    {
        IGCodeAnalyses Parse(string gCodeCommands);
    }
}