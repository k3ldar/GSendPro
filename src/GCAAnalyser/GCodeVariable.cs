using GSendShared;

namespace GSendAnalyser
{
    internal class GCodeVariable : IGCodeVariable
    {
        public GCodeVariable(string block)
        {
            VariableBlock = block;
            Variable = VariableBlock[1..^1];

            if (ushort.TryParse(Variable.Substring(1), out ushort value))
                VariableId = value;
        }

        public string VariableBlock { get; }

        public string Variable { get; }

        public ushort VariableId { get; }
    }
}
