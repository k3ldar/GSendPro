namespace GSendShared.Models
{
    /// <summary>
    /// Model for variables
    /// </summary>
    public sealed class VariableModel
    {
        public VariableModel(ushort variableId, string value)
        {
            VariableId = variableId;

            if (Decimal.TryParse(value, out decimal decimalValue))
            {
                IsDecimal = true;
                Value = decimalValue;
            }
            else if (Boolean.TryParse(value, out bool boolValue))
            {
                IsBoolean = true;
                Value = boolValue;
            }
            else
            {
                Value = value;
            }
        }

        public ushort VariableId { get; }

        public object Value { get; }

        public bool IsBoolean { get; }

        public bool IsDecimal { get; }
    }
}
