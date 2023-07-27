using GSendShared;

namespace GSendAnalyzer.Analyzers
{
    public class BaseAnalyzer
    {
        protected bool ValidateNextCommand(IGCodeCommand command, decimal finalNextTo)
        {
            if (command == null)
                return false;

            if (command.NextCommand != null && command.NextCommand.CommandValue == finalNextTo)
            {
                return true;
            }

            return false;
        }

        protected bool ValidateNextCommand(IGCodeCommand command, decimal finalNextTo, decimal[] nextTo, char[] ignoreNextCommands, int depth)
        {
            if (command == null || command.NextCommand == null || depth == 10)
                return false;

            if (command.NextCommand != null && command.NextCommand.CommandValue == finalNextTo)
            {
                return true;
            }

            for (int i = 0; i < nextTo.Length; i++)
            {
                for (int j = 0; j < ignoreNextCommands.Length; j++)
                {
                    if (command.NextCommand.Command == ignoreNextCommands[j])
                        return ValidateNextCommand(command.NextCommand, finalNextTo, nextTo, ignoreNextCommands, depth + 1);
                }

                if (command.NextCommand.CommandValue == nextTo[i])
                {
                    return ValidateNextCommand(command.NextCommand, finalNextTo, nextTo, ignoreNextCommands, depth + 1);
                }
            }

            return false;
        }
    }
}
