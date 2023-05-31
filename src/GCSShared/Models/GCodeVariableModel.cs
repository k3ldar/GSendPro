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

        public GCodeVariableModel(ushort variableId, string value, int lineNumber)
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

            LineNumber = lineNumber;
        }

        public ushort VariableId { get; }

        public object Value { get; }

        public bool IsBoolean { get; }

        public bool IsDecimal { get; }

        public int LineNumber { get; }
    }
}
