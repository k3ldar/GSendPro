using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{

    internal class M605Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<GSendShared.IGCodeCommand> m605Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals('M') && c.CommandValue.Equals(Constants.MCode605)).ToList();

            if (m605Commands.Count > 0)
            {

            }

            return false;
        }
    }
}
