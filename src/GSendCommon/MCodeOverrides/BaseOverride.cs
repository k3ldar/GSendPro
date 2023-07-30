using GSendShared;

namespace GSendCommon.MCodeOverrides
{
    internal class BaseOverride
    {
        protected bool GetAdjoiningGCodeCommandValue(IGCodeCommand mainCommand, char adjoiningCommandCode, out decimal defaultValue)
        {
            defaultValue = 0;
            if (mainCommand == null)
                return false;

            if (mainCommand.NextCommand.Command == adjoiningCommandCode)
            {
                defaultValue = mainCommand.NextCommand.CommandValue;
                return true;
            }
            else if (mainCommand.PreviousCommand.Command == adjoiningCommandCode)
            {
                defaultValue = mainCommand.PreviousCommand.CommandValue;
                return true;
            }

            return false;
        }

        protected List<IGCodeCommand> PreviousCommands(IGCodeCommand command, string[] requiredCommands)
        {
            List<IGCodeCommand> Result = new();

            IGCodeCommand previous = command.PreviousCommand;

            while (true)
            {
                if (previous == null || !CommandAllowed(previous, requiredCommands))
                    break;

                Result.Add(previous);

                previous = previous.PreviousCommand;
            }


            return Result;
        }

        private static bool CommandAllowed(IGCodeCommand command, string[] requiredCommands)
        {
            if (requiredCommands.Contains($"{command.Command}{command.CommandValue}"))
                return true;

            if (requiredCommands.Contains($"{command.Command}"))
                return true;

            return false;
        }
    }
}
