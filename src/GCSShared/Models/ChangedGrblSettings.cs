namespace GSendShared.Models
{
    public sealed class ChangedGrblSettings
    {
        public ChangedGrblSettings(string propertyName, int dollarValue, string oldValue, string newValue)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            DollarValue = dollarValue;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string PropertyName { get; }

        public int DollarValue { get; }

        public string OldValue { get; }

        public string NewValue { get; }
    }
}
