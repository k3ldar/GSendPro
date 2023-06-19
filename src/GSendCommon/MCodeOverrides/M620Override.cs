using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{

    internal class M620Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {

            return false;
        }
    }
}
