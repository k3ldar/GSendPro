using System.Diagnostics;
using System.Text;

using GSendShared;

namespace GSendAnalyser
{
    public sealed class GCodeLine : IGCodeLine
    {
        public LineStatus Status { get; set; }

        public List<IGCodeCommand> Commands { get; } = new();

        public string GetGCode()
        {
            StringBuilder Result = new();

            foreach (IGCodeCommand command in Commands)
            {
                Result.Append($"{command.Command}{command.CommandValue}");
            }

            return Result.ToString();
        }
    }
}
