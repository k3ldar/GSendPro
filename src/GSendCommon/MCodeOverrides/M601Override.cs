using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    /// <summary>
    /// Override allowing a pause between 100 and 10000 ms
    /// </summary>
    internal class M601Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand m601Command = overrideContext.GCode.Commands.Find(c => c.Command.Equals('M') && c.CommandValue.Equals(Constants.MCode601Timeout));

            if (m601Command == null)
                return false;

            IGCodeCommand pCode = overrideContext.GCode.Commands.Find(c => c.Command.Equals('P'));

            if (pCode == null)
                return false;

            if (pCode.CommandValue > Constants.MCodeMinTimeoutValue && pCode.CommandValue <= Constants.MCodeMaxTimeoutValue)
            {
                overrideContext.Variables[Constants.SystemVariableTimeout].Value = pCode.CommandValue;
                overrideContext.SendCommand = false;                
                return true;
            }

            return false;
        }
    }
}
