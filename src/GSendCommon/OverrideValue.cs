using GSendShared.Interfaces;

namespace GSendCommon
{
    public sealed class OverrideValue : IOverriddenValue
    {
        public int OriginalValue { get; set; }

        public int NewValue { get; set; }
    }
}
