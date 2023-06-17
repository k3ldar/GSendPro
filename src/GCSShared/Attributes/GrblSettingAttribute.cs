namespace GSendShared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GrblSettingAttribute : Attribute
    {
        public GrblSettingAttribute(string dollarValue)
        {
            DollarValue = dollarValue;

            if (Int32.TryParse(dollarValue[1..], out int intValue))
                IntValue = intValue;
        }

        public string DollarValue { get; }

        public int IntValue { get; } = -1;
    }
}
