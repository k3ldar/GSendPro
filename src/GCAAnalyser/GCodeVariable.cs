using GSendShared;

namespace GSendAnalyser
{
    internal class GCodeVariable : IGCodeVariable
    {
        public GCodeVariable(string block)
        {
            VariableBlock = block;
        }

        public string VariableBlock { get; }
    }
}
