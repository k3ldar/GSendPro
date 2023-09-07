using System.Text;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using PluginManager.Abstractions;

using static GSendShared.Constants;

namespace GSendAnalyzer.Internal
{
    internal sealed class GCodeParser : IGCodeParser
    {
        #region Private Members

        private const int UserVariableStartingId = 100;

        private readonly IPluginClassesService _pluginClassesService;
        private readonly ISubprograms _subprograms;
        private int _index;

        #endregion Private Members

        #region Constructors

        public GCodeParser(IPluginClassesService pluginClassesService, ISubprograms subprograms)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _subprograms = subprograms ?? throw new ArgumentNullException(nameof(subprograms));
        }

        #endregion Constructors

        #region Public Methods

        public IGCodeAnalyses Parse(string gCodeCommands)
        {
            if (String.IsNullOrEmpty(gCodeCommands))
                throw new ArgumentNullException(nameof(gCodeCommands));

            return InternalParseGCode(UTF8Encoding.UTF8.GetBytes(gCodeCommands.Trim()), 0);
        }

        #endregion Public Methods

        #region Private Methods

        private IGCodeAnalyses InternalParseGCode(byte[] gCodeCommands, int recursionDepth)
        {
            GCodeAnalyses Result = new(_pluginClassesService);
            Result = InternalParseGCode(Result, gCodeCommands, recursionDepth);

            // system variables
            Result.AddVariable(new GCodeVariableModel(Constants.SystemVariableTimeout, 1000));

            return Result;
        }

        private GCodeAnalyses InternalParseGCode(GCodeAnalyses Result, byte[] gCodeCommands, int recursionDepth)
        {
            if (recursionDepth > Constants.MaxSubCommandRecursionDepth)
            {
                Result.AddError(GSend.Language.Resources.SubProgramError2);
                return Result;
            }

            Span<char> line = new(new char[MaxLineSize]);
            int position = 0;
            int currentLine = 1;
            bool isComment = false;
            bool isVariable = false;
            bool isVariableBlock = false;
            ClearLineData(line, ref isComment, ref isVariable, ref isVariableBlock);
            GCodeCommand lastCommand = null;
            StringBuilder lineValues = new(MaxLineSize);
            CurrentCommandValues currentValues = new();
            _index = 0;
            bool invalidGCode = false;

            for (int i = 0; i < gCodeCommands.Length; i++)
            {
                if (invalidGCode)
                    break;

                char c = (char)gCodeCommands[i];

                switch (c)
                {
                    case CharVariableBlockStart:
                        isVariableBlock = true;
                        line[position++] = c;

                        continue;

                    case CharVariableBlockEnd:
                        isVariableBlock = false;
                        line[position++] = c;

                        continue;

                    case CharVariable:
                        if (isVariableBlock)
                        {
                            line[position++] = c;
                        }
                        else
                        {
                            isVariable = true;

                            if (position > 0)
                                line[position++] = c;
                        }

                        continue;

                    case CharLineFeed:
                        if (isVariable)
                        {
                            InternalParseVariable(Result, currentLine, line);
                        }
                        else
                        {
                            lastCommand = InternalParseLine(Result, line, lastCommand, lineValues, currentValues, currentLine, recursionDepth + 1);
                        }

                        ClearLineData(line, ref isComment, ref isVariable, ref isVariableBlock);
                        currentLine++;
                        position = 0;


                        continue;

                    case CharCarriageReturn:
                        Result.AddOptions(AnalysesOptions.ContainsCRLF);

                        continue;

                    case CharOpeningBracket:
                    case CharSemiColon:

                        if (!isVariable)
                            line[position++] = c;

                        isComment = true;

                        continue;

                    default:
                        if (position + 1 > MaxLineSize)
                        {
                            if (isComment)
                            {
                                currentValues.Attributes |= CommandAttributes.InvalidCommentTooLong;
                            }
                            else
                            {
                                currentValues.Attributes |= CommandAttributes.InvalidLineTooLong;
                                invalidGCode = true;
                            }

                            break;
                        }
                        else
                        {
                            currentValues.Attributes &= ~CommandAttributes.InvalidLineTooLong;
                            currentValues.Attributes &= ~CommandAttributes.InvalidCommentTooLong;

                            if (isVariable && isComment)
                            {

                            }
                            else
                            {
                                line[position++] = c;
                            }
                        }

                        continue;
                }
            }

            if (line[0] != CharNull)
            {
                // if a variable and the line starts with a number then
                // parse as variable, otherwise parse as gcode
                if (isVariable && line[0] > 47 && line[0] < 58)
                    InternalParseVariable(Result, currentLine, line);
                else
                    InternalParseLine(Result, line, lastCommand, lineValues, currentValues, currentLine, recursionDepth + 1);
            }

            return Result;
        }

        private static void InternalParseVariable(GCodeAnalyses Result, int lineNumber, Span<char> line)
        {
            string[] variableParts = line.ToString().Trim().Replace("\0", String.Empty).Split(CharEquals, 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (variableParts.Length != 2)
            {
                Result.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid1, lineNumber));
                return;
            }

            if (!ushort.TryParse(variableParts[0], out ushort variableId) || variableId < UserVariableStartingId)
            {
                Result.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid2, lineNumber));
                return;
            }

            if (!Result.AddVariable(new GCodeVariableModel(variableId, variableParts[1], lineNumber)))
            {
                Result.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid3, lineNumber, variableId));
            }
        }

        private static void ClearLineData(in Span<char> line, ref bool isComment, ref bool isVariable, ref bool isVariableBlock)
        {
            isComment = false;
            isVariable = false;
            isVariableBlock = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == CharNull)
                    break;

                line[i] = CharNull;
            }
        }

        private GCodeCommand InternalParseLine(GCodeAnalyses analysis, in Span<char> line, GCodeCommand lastCommand,
            StringBuilder lineValues, CurrentCommandValues currentValues, int lineNumber, int recursionDepth)
        {
            GCodeCommand result = null;

            if (line[0] == CharNull)
                return result;

            lineValues.Clear();


            char currentCommand = CharNull;
            bool isComment = false;
            StringBuilder comment = new(256);

            bool isVariableBlock = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                bool canPeekAhead = i < line.Length + 1;

                if (isComment && c != CharNull && c != CharClosingBracket)
                {
                    comment.Append(c);
                    continue;
                }

                switch (c)
                {
                    case CharA:
                    case CharB:
                    case CharC:
                    case CharD:
                    case CharE:
                    case CharF:
                    case CharG:
                    case CharH:
                    case CharI:
                    case CharJ:
                    case CharK:
                    case CharL:
                    case CharM:
                    case CharN:
                    case CharO:
                    case CharP:
                    case CharQ:
                    case CharR:
                    case CharS:
                    case CharT:
                    case CharU:
                    case CharV:
                    case CharW:
                    case CharX:
                    case CharY:
                    case CharZ:
                    case CharPercent:
                        // new command
                        if (currentCommand != CharNull)
                            lastCommand = UpdateGCodeValue(analysis, lastCommand, lineValues, currentValues, lineNumber, recursionDepth, ref result, currentCommand, comment);

                        currentCommand = c;

                        continue;

                    case CharNull:
                        _ = UpdateGCodeValue(analysis, lastCommand, lineValues, currentValues, lineNumber, recursionDepth, ref result, currentCommand, comment);


                        return result;

                    case CharLineFeed:
                        if (currentCommand != CharNull)
                            lastCommand = UpdateGCodeValue(analysis, lastCommand, lineValues, currentValues, lineNumber, recursionDepth, ref result, currentCommand, comment);

                        continue;

                    case CharOpeningBracket:
                    case CharSemiColon:
                        // comment start
                        if (isComment)
                        {
                            lineValues.Append(c);
                        }
                        else
                        {
                            comment.Append(c);
                            isComment = true;
                        }

                        continue;

                    case CharClosingBracket:
                        //comment end
                        comment.Append(c);
                        isComment = false;

                        continue;

                    case CharVariableBlockStart:
                        isVariableBlock = true;
                        lineValues.Append(c);

                        continue;

                    case CharVariableBlockEnd:
                        isVariableBlock = false;
                        lineValues.Append(c);

                        continue;

                    case CharTab:
                    case CharSpace:
                        if (isComment || isVariableBlock || (canPeekAhead && line[i + 1] == '['))
                        {
                            lineValues.Append(c);
                        }

                        continue;

                    default:
                        lineValues.Append(c);
                        continue;
                }
            }

            return UpdateGCodeValue(analysis, lastCommand, lineValues, currentValues, lineNumber, recursionDepth, ref result, currentCommand, comment);
        }

        private GCodeCommand UpdateGCodeValue(GCodeAnalyses analysis, GCodeCommand lastCommand, StringBuilder lineValues, CurrentCommandValues currentValues, 
            int lineNumber, int recursionDepth, ref GCodeCommand result, char currentCommand, StringBuilder comment)
        {
            string lineValue = lineValues.ToString().Trim();
            List<IGCodeVariableBlock> variables = new();
            bool commandValueConvert = Decimal.TryParse(lineValue, out decimal commandValue);

            if (!commandValueConvert)
            {
                commandValue = Decimal.MinValue;

                decimal retreivedCommandValue = RetrieveCommandVariableBlocks(analysis, lineValue, currentValues, lineNumber, variables);

                if (!commandValueConvert)
                    commandValue = retreivedCommandValue;
            }

            string comments = comment.ToString();

            if (!String.IsNullOrWhiteSpace(comments))
            {
                RetrieveCommandVariableBlocks(analysis, comments, currentValues, lineNumber, variables);
            }

            currentValues.Attributes &= ~CommandAttributes.Extrude;
            currentValues.Attributes &= ~CommandAttributes.FeedRateError;
            currentValues.Attributes &= ~CommandAttributes.MovementError;
            currentValues.Attributes &= ~CommandAttributes.SpindleSpeedError;
            currentValues.Attributes &= ~CommandAttributes.ContainsVariables;
            currentValues.Attributes &= ~CommandAttributes.ChangeCoordinates;
            currentValues.Attributes &= ~CommandAttributes.InvalidGCode;
            GCodeAnalyses subProgramAnalyses = null;

            bool hasCommandCode = Int32.TryParse(Math.Truncate(commandValue).ToString(), out int commandCode);

            if (!hasCommandCode && currentCommand != '\0' && variables != null && !variables.Exists(v => v.VariableBlock == lineValues.ToString()))
            {
                currentValues.Attributes |= CommandAttributes.InvalidGCode;
            }

            switch (currentCommand)
            {
                case CharO:

                    string subProgram = $"{currentCommand}{commandValue}";

                    if (_subprograms.Exists(subProgram))
                    {
                        ISubprogram sub = _subprograms.Get(subProgram);

                        if (sub != null)
                        {
                            subProgramAnalyses = new GCodeAnalyses(_pluginClassesService);
                            IGCodeAnalyses subprogramAnalyses = InternalParseGCode(subProgramAnalyses, UTF8Encoding.UTF8.GetBytes(sub.Contents), recursionDepth + 1);

                            foreach (string error in subprogramAnalyses.Errors)
                                analysis.AddError(error);

                            foreach (string warning in subprogramAnalyses.Warnings)
                                analysis.AddError(warning);

                            // add subanalyses variables to parent
                            foreach (KeyValuePair<ushort, IGCodeVariable> variable in subProgramAnalyses.Variables)
                            {
                                if (analysis.Variables.ContainsKey(variable.Key))
                                {
                                    analysis.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid3, lineNumber, variable.Key));
                                    continue;
                                }

                                analysis.AddVariable(new GCodeVariableModel(variable.Value.VariableId, variable.Value.Value.ToString(), lineNumber));
                            }
                        }
                    }
                    else
                    {
                        analysis.AddError(String.Format(GSend.Language.Resources.SubprogramNotFound, subProgram));
                    }

                    break;

                case CharE:
                    currentValues.Attributes |= CommandAttributes.Extrude;
                    break;

                case CharF:
                    if (commandValueConvert)
                        currentValues.FeedRate = commandValue;
                    else
                        currentValues.Attributes |= CommandAttributes.FeedRateError;

                    break;

                case CharG:
                    if (!hasCommandCode)
                    {
                        break;
                    }

                    switch (commandCode)
                    {
                        case 0:
                            currentValues.Attributes |= CommandAttributes.UseRapidRate;
                            currentValues.Attributes &= ~CommandAttributes.AllowSpeedOverride;

                            break;

                        case 1:
                        case 2:
                        case 3:
                            currentValues.Attributes |= CommandAttributes.AllowSpeedOverride;
                            currentValues.Attributes &= ~CommandAttributes.UseRapidRate;

                            break;

                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                            currentValues.Attributes |= CommandAttributes.ChangeCoordinates;

                            break;

                        default:
                            currentValues.Attributes &= ~CommandAttributes.UseRapidRate;
                            currentValues.Attributes &= ~CommandAttributes.AllowSpeedOverride;

                            break;
                    }

                    break;

                case CharS:
                    if (commandValueConvert)
                        currentValues.SpindleSpeed = commandValue;
                    else
                        currentValues.Attributes |= CommandAttributes.SpindleSpeedError;

                    break;

                case CharZ:
                    if (commandValueConvert)
                        currentValues.Z = commandValue;
                    else
                        currentValues.Attributes |= CommandAttributes.MovementError;

                    break;

                case CharX:
                    if (commandValueConvert)
                        currentValues.X = commandValue;
                    else
                        currentValues.Attributes |= CommandAttributes.MovementError;

                    break;

                case CharY:
                    if (commandValueConvert)
                        currentValues.Y = commandValue;
                    else
                        currentValues.Attributes |= CommandAttributes.MovementError;

                    break;

                case CharM:
                    if (commandValueConvert && (commandValue == 2 || commandValue == 30))
                    {
                        currentValues.Attributes |= CommandAttributes.EndProgram;
                    }
                    else if (commandValueConvert && commandValue == 5)
                    {
                        currentValues.SpindleSpeed = 0;
                        currentValues.Attributes &= ~CommandAttributes.SpindleSpeed;
                    }

                    break;

                case CharPercent:
                    currentValues.Attributes |= CommandAttributes.StartProgram;

                    break;
            }

            if (currentValues.Attributes.HasFlag(CommandAttributes.StartProgram))
                currentValues.Attributes &= ~CommandAttributes.StartProgram;

            CurrentCommandValues newValues = currentValues.Clone();

            result = new GCodeCommand(_index++, currentCommand, commandValue, lineValue, comments, variables, newValues, lineNumber, subProgramAnalyses);

            if (lastCommand != null)
                lastCommand.NextCommand = result;

            result.PreviousCommand = lastCommand;

            analysis.Add(result);
            lineValues.Clear();
            return result;
        }

        private static decimal RetrieveCommandVariableBlocks(GCodeAnalyses analysis, string lineValues, CurrentCommandValues currentValues, int lineNumber, List<IGCodeVariableBlock> variables)
        {
            decimal Result = decimal.MinValue;

            string variableBlock = lineValues;

            // does it contain variables
            int variableBlockStart = variableBlock.IndexOf('[');

            if (variableBlockStart > -1)
            {

                Result = ParseVariableBlockss(analysis, variables, lineNumber, variableBlock);

                if (variables.Count > 0)
                    currentValues.Attributes |= CommandAttributes.ContainsVariables;
            }
            else if (variableBlockStart == -1 && variableBlock.IndexOf('#') > -1)
            {
                analysis.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid5, lineNumber));
            }

            return Result;
        }

        private static decimal ParseVariableBlockss(GCodeAnalyses analyses, List<IGCodeVariableBlock> variables, int lineNumber, string line)
        {
            int variableBlockStart = line.IndexOf('[', 0);
            string newCommandValue = line[..variableBlockStart];
            bool commandValueConvert = Decimal.TryParse(newCommandValue, out decimal commandValue);

            if (!commandValueConvert)
                commandValue = Decimal.MinValue;

            while (variableBlockStart > -1)
            {
                int variableBlockEnd = line.IndexOf(']', variableBlockStart);

                if (variableBlockStart >= 0 && variableBlockEnd > variableBlockStart)
                {
                    string variable = line.Substring(variableBlockStart, variableBlockEnd - variableBlockStart + 1);
                    GCodeVariableBlockModel gCodeVariable = new(variable, lineNumber);

                    variables.Add(gCodeVariable);
                }
                else if (variableBlockStart >= 0 && variableBlockEnd < variableBlockStart)
                {
                    analyses.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid4, lineNumber));
                }

                if (variableBlockEnd > 0)
                    variableBlockStart = line.IndexOf('[', variableBlockStart + 1);
                else
                    variableBlockStart = -1;
            }

            return commandValue;
        }

        #endregion Private Methods
    }
}
