using GSendShared;

namespace GSendAnalyzer.Analyzers
{
    public class BaseAnalyzer
    {
        protected static bool HasCommandsOnSameLine(IGCodeCommand command, char ignoreCommands)
        {
            var commandsOnLine = CommandsOnSameLine(command);

            return commandsOnLine.Exists(c => c.Command != ignoreCommands);
        }

        protected static List<IGCodeCommand> CommandsOnSameLine(IGCodeCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            List<IGCodeCommand> Result = new();

            LookPrevious(command, Result);
            LookNext(command, Result);


            return Result;
        }

        private static void LookPrevious(IGCodeCommand command, List<IGCodeCommand> commands)
        {
            IGCodeCommand previous = command.PreviousCommand;

            while (true)
            {
                if (previous == null)
                    break;

                if (previous.MasterLineNumber == command.MasterLineNumber)
                    commands.Add(previous);
                else
                    break;

                previous = previous.PreviousCommand;
            }
        }

        private static void LookNext(IGCodeCommand command, List<IGCodeCommand> commands)
        {
            IGCodeCommand next = command.NextCommand;

            while (true)
            {
                if (next == null)
                    break;

                if (next.MasterLineNumber == command.MasterLineNumber)
                    commands.Add(next);
                else
                    break;

                next = next.NextCommand;
            }
        }

        protected static bool ValidatePreviousNextCommand(IGCodeCommand command, char toFindCommand)
        {
            if (command == null)
                return false;

            if (command.PreviousCommand != null && command.PreviousCommand.Command == toFindCommand)
                return true;

            if (command.NextCommand != null && command.NextCommand.Command == toFindCommand)
                return true;

            return false;
        }

        protected static bool ValidateNextCommand(IGCodeCommand command, decimal finalNextTo)
        {
            if (command == null)
                return false;

            if (command.NextCommand != null && command.NextCommand.CommandValue == finalNextTo)
            {
                return true;
            }

            return false;
        }

        protected static bool ValidateNextCommand(IGCodeCommand command, decimal finalNextTo, decimal[] nextTo, char[] ignoreNextCommands, int depth)
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
