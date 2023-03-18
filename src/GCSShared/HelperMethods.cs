namespace GSendShared
{
    public static class HelperMethods
    {
        public static string ConvertMeasurementForDisplay(DisplayUnits displayUnits, double mmMin)
        {
            switch (displayUnits)
            {
                case DisplayUnits.MmPerSecond:
                    return (mmMin / 60.0).ToString("N2");

                case DisplayUnits.InchPerMinute:
                    return (mmMin / 25.4).ToString("N3");

                case DisplayUnits.InchPerSecond:
                    return (mmMin / 25.4 / 60).ToString("N3");
            }

            return mmMin.ToString("N2");
        }
    }
}
