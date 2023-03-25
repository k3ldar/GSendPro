using System.Drawing.Drawing2D;

using static GSendShared.Constants;

namespace GSendShared
{
    public static class HelperMethods
    {
        public static string ConvertMeasurementForDisplay(DisplayUnits displayUnits, double mmMin)
        {
            switch (displayUnits)
            {
                case DisplayUnits.MmPerSecond:
                    return (mmMin / 60.0).ToString("N4");

                case DisplayUnits.InchPerMinute:
                    return (mmMin / 25.4).ToString("N5");

                case DisplayUnits.InchPerSecond:
                    return (mmMin / 25.4 / 60).ToString("N5");
            }

            return mmMin.ToString("N4");
        }

        public static string TranslateState(string state)
        {
            switch (state)
            {
                case StateUndefined:
                    return GSend.Language.Resources.StatePortOpen;
                case StateIdle:
                    return GSend.Language.Resources.StateIdle;
                case StateRun:
                    return GSend.Language.Resources.StateRun;
                case StateJog:
                    return GSend.Language.Resources.StateJog;
                case StateAlarm:
                    return GSend.Language.Resources.StateAlarm;
                case StateDoor:
                    return GSend.Language.Resources.StateDoor;
                case StateCheck:
                    return GSend.Language.Resources.StateCheck;
                case StateHome:
                    return GSend.Language.Resources.StateHome;
                case StateSleep:
                    return GSend.Language.Resources.StateSleep;
                case StateHold:
                    return GSend.Language.Resources.StateHold;
                default:
                    return GSend.Language.Resources.StateUnknown;
            }
        }

        public static string TranslateState(MachineState state)
        {
            switch (state)
            {
                case MachineState.Undefined:
                    return GSend.Language.Resources.StatePortOpen;
                case MachineState.Idle:
                    return GSend.Language.Resources.StateIdle;
                case MachineState.Run:
                    return GSend.Language.Resources.StateRun;
                case MachineState.Jog:
                    return GSend.Language.Resources.StateJog;
                case MachineState.Alarm:
                    return GSend.Language.Resources.StateAlarm;
                case MachineState.Door:
                    return GSend.Language.Resources.StateDoor;
                case MachineState.Check:
                    return GSend.Language.Resources.StateCheck;
                case MachineState.Home:
                    return GSend.Language.Resources.StateHome;
                case MachineState.Sleep:
                    return GSend.Language.Resources.StateSleep;
                case MachineState.Hold:
                    return GSend.Language.Resources.StateHold;
                default:
                    return GSend.Language.Resources.StateUnknown;
            }
        }
    }
}
