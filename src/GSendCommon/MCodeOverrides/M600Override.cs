using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    /// <summary>
    /// Override allowing a pause between 1 and 2000 ms
    /// </summary>
    internal class M600Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand m600Command = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('M') && c.CommandValue.Equals(600));

            if (m600Command == null)
                return false;

            IGCodeCommand pCode = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('P'));

            if (pCode == null)
                return false;

            if (pCode.CommandValue > 0 && pCode.CommandValue <= 2000)
            {
                TimeSpan sleepTime = TimeSpan.FromSeconds((double)pCode.CommandValue);
                Thread.Sleep(sleepTime.Milliseconds);
                return true;
            }

            return false;
        }
    }
}
