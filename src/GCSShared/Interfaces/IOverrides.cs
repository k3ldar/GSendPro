namespace GSendShared.Interfaces
{
    public interface IOverrides
    {
        IOverriddenValue Rapids { get; }

        IOverriddenValue AxisXY { get; }

        IOverriddenValue AxisZUp { get; }

        IOverriddenValue AxisZDown { get; }

        IOverriddenValue Spindle { get; }

        bool OverrideRapids { get; }

        bool OverrideXY { get; }

        bool OverrideZUp { get; }

        bool OverrideZDown { get; }

        bool OverrideSpindle { get; }

        bool OverridesEnabled { get; }
    }
}
