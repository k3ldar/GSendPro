using GSendShared;

namespace GSendAnalyser
{
    internal class GCodeVariable : IGCodeVariable
    {
        public GCodeVariable(string block, int lineNumber)
        {
            VariableBlock = block;
            Variables = new();
            VariableIds = new();

            ParseVariables();
            LineNumber = lineNumber;
        }

        public string VariableBlock { get; }

        public int LineNumber { get; }

        public List<string> Variables { get; }

        public List<ushort> VariableIds { get; }

        private void ParseVariables()
        {
            string innerBlock = VariableBlock[1..^1];

            bool isVariable = false;
            string varBlock = "";

            foreach (char c in innerBlock)
            {
                switch (c)
                {
                    case '#':
                        if (isVariable)
                        {
                            GetVariableDetails(ref varBlock, ref isVariable);
                        }
                        isVariable = true;
                        varBlock += c;
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (isVariable)
                            varBlock += c;

                        break;

                    default:
                        if (isVariable)
                        {
                            GetVariableDetails(ref varBlock, ref isVariable);
                        }

                        break;
                }
            }

            if (isVariable)
                GetVariableDetails(ref varBlock, ref isVariable);
        }

        private void GetVariableDetails(ref string varBlock, ref bool isVariable)
        {
            if (ushort.TryParse(varBlock.Substring(1), out ushort value))
            {
                Variables.Add(varBlock);
                VariableIds.Add(value);
                varBlock = String.Empty;
                isVariable = false;
            }
        }
    }
}
