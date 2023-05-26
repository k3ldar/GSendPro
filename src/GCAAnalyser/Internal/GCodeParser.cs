﻿using System.Text;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using PluginManager.Abstractions;

using static GSendAnalyser.Internal.Consts;

namespace GSendAnalyser.Internal
{
    internal sealed class GCodeParser : IGCodeParser
    {
        #region Private Members

        private const int UserVariableStartingId = 100;

        private readonly IPluginClassesService _pluginClassesService;
        private int _index;

        #endregion Private Members

        #region Constructors

        public GCodeParser(IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
        }

        #endregion Constructors

        #region Public Methods

        public IGCodeAnalyses Parse(string gCodeCommands)
        {
            if (String.IsNullOrEmpty(gCodeCommands))
                throw new ArgumentNullException(nameof(gCodeCommands));

            return InternalParseGCode(UTF8Encoding.UTF8.GetBytes(gCodeCommands.Trim()));
        }

        #endregion Public Methods

        #region Private Methods

        private IGCodeAnalyses InternalParseGCode(byte[] gCodeCommands)
        {
            GCodeAnalyses Result = new(_pluginClassesService);

            Span<char> line = new(new char[MaxLineSize]);
            int position = 0;
            int currentLine = 1;
            bool isComment = false;
            bool isVariable = false;
            bool isVariableBlock = false;
            ClearLineData(line);
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
                        }

                        continue;

                    case CharG:
                        if (!isComment && !isVariable)
                        {
                            if (line[0] != CharNull)
                            {
                                lastCommand = InternalParseLine(Result, line, lastCommand, lineValues, currentValues, currentLine);
                                ClearLineData(line);
                            }

                            position = 0;
                        }

                        line[position++] = c;

                        continue;

                    case CharLineFeed:
                        if (isVariable)
                        {
                            isVariable = false;
                            InternalParseVariable(Result, currentLine, line);
                        }
                        else
                        {
                            lastCommand = InternalParseLine(Result, line, lastCommand, lineValues, currentValues, currentLine);
                        }

                        ClearLineData(line);
                        currentLine++;
                        position = 0;


                        continue;

                    case CharCarriageReturn:
                        Result.AddOptions(AnalysesOptions.ContainsCRLF);

                        continue;

                    case CharOpeningBracket:
                    case CharSemiColon:
                        line[position++] = c;
                        isComment = true;

                        continue;

                    default:
                        if (position + 1 > MaxLineSize)
                        {
                            currentValues.Attributes |= CommandAttributes.InvalidLineTooLong;
                            invalidGCode = true;
                            break;
                        }
                        else
                        {
                            currentValues.Attributes &= ~CommandAttributes.InvalidLineTooLong;
                            line[position++] = c;
                        }

                        continue;
                }
            }

            if (line[0] != CharNull)
            {
                InternalParseLine(Result, line, lastCommand, lineValues, currentValues, currentLine);
            }

            return Result;
        }

        private void InternalParseVariable(GCodeAnalyses Result, int lineNumber, Span<char> line)
        {
            string[] variableParts = line.ToString().Trim().Replace("\0", String.Empty).Split(CharEquals, 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (variableParts.Length != 2)
            {
                Result.AddError(String.Format(GSend.Language.Resources.VariableInvalid1, lineNumber));
                return;
            }

            if (!ushort.TryParse(variableParts[0], out ushort variableId) || variableId < UserVariableStartingId)
            {
                Result.AddError(String.Format(GSend.Language.Resources.VariableInvalid2, lineNumber));
                return;
            }

            if (!Result.AddVariable(new VariableModel(variableId, variableParts[1])))
            {
                Result.AddError(String.Format(GSend.Language.Resources.VariableInvalid3, lineNumber, variableId));
            }
        }

        private static void ClearLineData(in Span<char> line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == CharNull)
                    break;

                line[i] = CharNull;
            }
        }

        private GCodeCommand InternalParseLine(GCodeAnalyses analysis, in Span<char> line, GCodeCommand lastCommand, 
            StringBuilder lineValues, CurrentCommandValues currentValues, int lineNumber)
        {
            GCodeCommand result = null;

            if (line[0] == CharNull)
                return result;

            lineValues.Clear();


            char currentCommand = CharNull;
            bool isComment = false;
            StringBuilder comment = new(256);
            List<IGCodeVariable> variables = null;

            GCodeCommand UpdateGCodeValue()
            {
                bool commandValueConvert = Decimal.TryParse(lineValues.ToString(), out decimal commandValue);

                if (!commandValueConvert)
                    commandValue = Decimal.MinValue;

                if (!commandValueConvert)
                {
                    string variableBlock = lineValues.ToString();
                    
                    // does it contain variables
                    int variableBlockStart = variableBlock.IndexOf('[');

                    if (variableBlockStart > -1)
                    {
                        variables = ParseVariables(analysis, lineNumber, variableBlock, ref commandValueConvert, ref commandValue);

                        if (variables.Count > 0)
                            currentValues.Attributes |= CommandAttributes.ContainsVariables;
                    }
                }

                Int32.TryParse(Math.Truncate(commandValue).ToString(), out int commandCode);
                //decimal mantissa = Math.Round(100 * (commandValue - commandCode));

                currentValues.Attributes &= ~CommandAttributes.Extrude;
                currentValues.Attributes &= ~CommandAttributes.FeedRateError;
                currentValues.Attributes &= ~CommandAttributes.MovementError;
                currentValues.Attributes &= ~CommandAttributes.SpindleSpeedError;
                currentValues.Attributes &= ~CommandAttributes.ContainsVariables;

                switch (currentCommand)
                {
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
                result = new GCodeCommand(_index++, currentCommand, commandValue, lineValues.ToString(), comment.ToString(), variables, newValues, lineNumber);

                if (lastCommand != null)
                    lastCommand.NextCommand = result;

                result.PreviousCommand = lastCommand;

                analysis.Add(result);
                lineValues.Clear();
                return result;
            }

            bool isVariableBlock = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

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
                            lastCommand = UpdateGCodeValue();

                        currentCommand = c;

                        continue;

                    case CharNull:
                        lastCommand = UpdateGCodeValue();


                        return result;

                    case CharLineFeed:
                        if (currentCommand != CharNull)
                            lastCommand = UpdateGCodeValue();

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
                        if (isComment || isVariableBlock)
                        {
                            lineValues.Append(c);
                        }

                        continue;

                    default:
                        lineValues.Append(c);
                        continue;
                }
            }

            return UpdateGCodeValue();
        }

        private List<IGCodeVariable> ParseVariables(GCodeAnalyses analyses, int lineNumber, string line, ref bool commandValueConvert, ref decimal commandValue)
        {
            List<IGCodeVariable> Result = new();

            int variableBlockStart = line.IndexOf('[', 0);
            string newCommandValue = line.Substring(0, variableBlockStart);
            commandValueConvert = Decimal.TryParse(newCommandValue, out commandValue);

            if (!commandValueConvert)
                commandValue = Decimal.MinValue;

            while (variableBlockStart > -1)
            {
                int variableBlockEnd = line.IndexOf(']');

                if (variableBlockStart >= 0 && variableBlockEnd > variableBlockStart)
                {
                    string variable = line.Substring(variableBlockStart, variableBlockEnd - variableBlockStart + 1);
                    Result.Add(new GCodeVariable(variable));

                }
                else if (variableBlockStart > 0 && variableBlockEnd < variableBlockStart)
                {
                    analyses.AddError(String.Format(GSend.Language.Resources.VariableInvalid4, lineNumber));
                }

                if (variableBlockEnd > 0)
                    variableBlockStart = line.IndexOf('[', variableBlockEnd);
                else
                    variableBlockStart = -1;
            }

            return Result;
        }

        #endregion Private Methods
    }
}
