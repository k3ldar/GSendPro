
using GSendShared;

namespace GSendShared.Interfaces
{
    public interface IGCodeParser
    {
        IGCodeAnalyses Parse(string gCodeCommands);
    }
}