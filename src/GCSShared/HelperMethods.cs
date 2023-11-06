using System.Text.Json;

using GSendShared.Plugins;

using static GSendShared.Constants;

namespace GSendShared
{
    public static class HelperMethods
    {
        public static List<GSendPluginSettings> LoadPluginSettings(string pluginConfig)
        {
            if (File.Exists(pluginConfig))
            {
                using FileStream fileStream = new(pluginConfig, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return JsonSerializer.Deserialize<List<GSendPluginSettings>>(fileStream);
            }

            return new();
        }

        public static string TimeSpanToTime(TimeSpan time)
        {
            const int HoursInDay = 24;
            const int MinutesInHour = 60;
            const int SecondsInHour = 60;
            const string NoTime = "-";

            if (time.TotalHours >= HoursInDay)
                return String.Format(GSend.Language.Resources.TimeFormatDay, time.Days, time.Hours, time.Minutes, time.Seconds);
            else if (time.TotalMinutes >= MinutesInHour)
                return String.Format(GSend.Language.Resources.TimeFormatHour, time.Hours, time.Minutes, time.Seconds);
            else if (time.TotalSeconds >= SecondsInHour)
                return String.Format(GSend.Language.Resources.TimeFormatMinute, time.Minutes, time.Seconds);
            else if (time.TotalMilliseconds <= TimeSpan.Zero.TotalMilliseconds)
                return NoTime;

            return String.Format(GSend.Language.Resources.TimeFormatSecond, time.Seconds);
        }

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
            if (mmMin == 0)
                return mmMin.ToString("N0");

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

        public static string FormatPercent(decimal val1, decimal val2)
        {
            if (val1 == 0 || val2 == 0)
                return "0%";

            decimal increase = val2 - val1;
            decimal diff = increase / val1;

            return $"{diff:P}";
        }

        public static string FormatSpeed(decimal speed)
        {
            return $"{speed:0.##}";
        }

        public static string FormatSpeedValue(decimal speed)
        {
            return $"{speed:0.##} mm/min";
        }

        public static string FormatAccelerationValue(decimal speed)
        {
            return $"{speed:0.##} mm/sec²";
        }

        public static decimal ReduceValueByPercentage(decimal newValue, decimal originalValue, decimal percent)
        {
            if (newValue == originalValue)
                return originalValue;

            decimal percentValue = newValue / 100m;
            return newValue - (percent * percentValue);
        }
    }
}
