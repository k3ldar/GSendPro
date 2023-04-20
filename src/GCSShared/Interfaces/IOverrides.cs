namespace GSendShared.Interfaces
{
    public interface IOverrides
    {
        IOverriddenValue AxisX { get; }

        IOverriddenValue AxisY { get; }

        IOverriddenValue AxisZUp { get; }

        IOverriddenValue AxisZDown { get; }

        IOverriddenValue Spindle { get; }

        bool OverrideX { get; }

        bool OverrideY { get; }

        bool OverrideZUp { get; }

        bool OverrideZDown { get; }

        bool OverrideSpindle { get; }

        bool OverridesEnabled { get; }
    }
}
