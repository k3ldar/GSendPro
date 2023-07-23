using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    /// <summary>
    /// These commands are essentially ignored and never sent on to the grbl processor as they have no
    /// meaning there and are used by other custom m codes
    /// </summary>
    internal class MCodeIgnores : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> mCommands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && IgnoreCode(c.CommandValue)).ToList();

            if (mCommands.Count > 0)
            {
                overrideContext.SendCommand = false;
                return true;
            }

            return false;
        }

        private static bool IgnoreCode(decimal value)
        {
            switch (value)
            {
                case 630.1m:
                    return true;

                default:
                    return false;
            }
        }
    }
}
