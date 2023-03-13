
using GSendShared;

namespace GSendAnalyser.Abstractions
{
    public interface IGCodeParser
    {
        IGCodeAnalyses Parse(string gCodeCommands);
    }
}