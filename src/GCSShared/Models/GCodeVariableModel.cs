using GSendShared.Abstractions;

namespace GSendShared.Models
{
    /// <summary>
    /// Model for variables
    /// </summary>
    public sealed class GCodeVariableModel : IGCodeVariable
    {
        public GCodeVariableModel()
        {

        }

        public GCodeVariableModel(ushort variableId, object value)
        {
            VariableId = variableId;

            if (Decimal.TryParse(value.ToString(), out decimal decimalValue))
            {
                IsDecimal = true;
                Value = decimalValue;
            }
            else if (Boolean.TryParse(value.ToString(), out bool boolValue))
            {
                IsBoolean = true;
                Value = boolValue;
            }
            else
            {
                Value = value;
            }

        }

        public GCodeVariableModel(ushort variableId, string value, int lineNumber)
            : this (variableId, value)
        {

            LineNumber = lineNumber;
        }

        public ushort VariableId { get; set; }

        public object Value { get; set; }

        public decimal DecimalValue
        {
            get
            {
                if (IsDecimal)
                    return Convert.ToDecimal(Value.ToString());

                return Decimal.MinValue;
            }
        }

        public int IntValue
        {
            get
            {
                if (!Int32.TryParse(Value.ToString(), out int value))
                    return Int32.MinValue;

                return value;
            }
        }

        public bool IsBoolean { get; set; }

        public bool IsDecimal { get; set; }

        public int LineNumber { get; set; }
    }
}
