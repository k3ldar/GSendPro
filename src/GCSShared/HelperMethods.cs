using static GSendShared.Constants;

namespace GSendShared
{
    public static class HelperMethods
    {
        public static string ConvertMeasurementForDisplay(FeedbackUnit feedbackUnit, double mmMin)
        {
            switch (feedbackUnit)
            {
                case FeedbackUnit.Mm:
                    return (mmMin).ToString("N4");

                case FeedbackUnit.Inch:
                    return (mmMin / 25.4).ToString("N5");
            }

            return mmMin.ToString("N4");
        }

        public static string ConvertFeedRateForDisplay(FeedRateDisplayUnits displayUnits, double mmMin)
        {
            switch (displayUnits)
            {
                case FeedRateDisplayUnits.MmPerMinute:
                    return mmMin.ToString("N0");

                case FeedRateDisplayUnits.MmPerSecond:
                    return (mmMin / 60.0).ToString("N0");

                case FeedRateDisplayUnits.InchPerMinute:
                    return (mmMin / 25.4).ToString("N5");

                case FeedRateDisplayUnits.InchPerSecond:
                    return (mmMin / 25.4 / 60).ToString("N5");
            }

            return mmMin.ToString("N4");
        }

        public static string TranslateDisplayUnit(FeedRateDisplayUnits displayUnits)
        {
            switch (displayUnits)
            {
                case FeedRateDisplayUnits.MmPerMinute:
                    return GSend.Language.Resources.DisplayMmMinute;

                case FeedRateDisplayUnits.MmPerSecond:
                    return GSend.Language.Resources.DisplayMmSec;

                case FeedRateDisplayUnits.InchPerMinute:
                    return GSend.Language.Resources.DisplayInchMinute;

                case FeedRateDisplayUnits.InchPerSecond:
                    return GSend.Language.Resources.DisplayInchSecond;
            }

            return GSend.Language.Resources.DisplayMmMinute;
        }

        public static string TranslateRapidOverride(RapidsOverride rapidsOverride)
        {
            switch (rapidsOverride)
            {
                case RapidsOverride.Low:
                    return GSend.Language.Resources.RapidRateLow;

                case RapidsOverride.Medium:
                    return GSend.Language.Resources.RapidRateMedium;
            }

            return GSend.Language.Resources.RapidRateHigh;
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
