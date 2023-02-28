using System.Text;

using GSendAnalyser.Abstractions;

using GSendShared;

using static GSendAnalyser.Internal.Consts;

namespace GSendAnalyser.Internal
{
    public class GCodeParser : IGCodeParser
    {
        #region Private Members

        private const int InitialDictionarySize = 26;

        private int _index;

        #endregion Private Members

        #region Constructors

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
            GCodeAnalyses Result = new();

            Span<char> line = new(new char[MaxLineSize]);
            int position = 0;
            ClearLineData(line);
            GCodeCommand lastCommand = null;
            StringBuilder lineValues = new(MaxLineSize);
            CurrentCommandValues currentValues = new();
            _index = 0;

            for (int i = 0; i < gCodeCommands.Length; i++)
            {
                char c = (char)gCodeCommands[i];

                switch (c)
                {

                    case CharG:
                        if (line[0] != CharNull)
                        {
                            lastCommand = InternalParseLine(Result, line, lastCommand, lineValues, currentValues);
                            ClearLineData(line);
                        }

                        position = 0;
                        line[position++] = c;

                        continue;

                    case CharLineFeed:
                        lastCommand = InternalParseLine(Result, line, lastCommand, lineValues, currentValues);
                        ClearLineData(line);
                        position = 0;

                        continue;

                    case CharCarriageReturn:
                        Result.ContainsCarriageReturn = true;

                        continue;

                    default:
                        line[position++] = c;

                        continue;
                }
            }

            if (line[0] != CharNull)
            {
                InternalParseLine(Result, line, lastCommand, lineValues, currentValues);
            }

            return Result;
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

        private GCodeCommand InternalParseLine(GCodeAnalyses analysis, in Span<char> line, GCodeCommand lastCommand, StringBuilder lineValues, CurrentCommandValues currentValues)
        {
            GCodeCommand result = null;

            if (line[0] == CharNull)
                return result;

            lineValues.Clear();


            char currentCommand = CharNull;
            bool isComment = false;
            string comment = String.Empty;

            GCodeCommand UpdateGCodeValue()
            {
                if (currentCommand != CharNull && lineValues.Length > 0)
                {
                    if (isComment)
                    {
                        comment = lineValues.ToString();
                    }
                }

                bool commandValueConvert = Decimal.TryParse(lineValues.ToString(), out decimal commandValue);
                Int32.TryParse(Math.Truncate(commandValue).ToString(), out int commandCode);
                decimal mantissa = Math.Round(100 * (commandValue - commandCode));

                switch (currentCommand)
                {
                    case CharF:
                        if (commandValueConvert)
                            currentValues.FeedRate = commandValue;
                        else
                            currentValues.Attributes |= CommandAttributes.FeedRateError;

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
                }

                CurrentCommandValues newValues = currentValues.Clone();
                result = new GCodeCommand(_index++, currentCommand, commandValue, lineValues.ToString(), comment, newValues);

                if (lastCommand != null)
                    lastCommand.NextCommand = result;

                result.PreviousCommand = lastCommand;

                analysis.Add(result);
                lineValues.Clear();
                return result;
            };

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (isComment && c != CharNull && c != CharClosingBracket)
                {
                    lineValues.Append(c);
                    continue;
                }

                switch (c)
                {
                    case CharA:
                    case CharB:
                    case CharC:
                    case CharD:
                    case CharF:
                    case CharG:
                    case CharH:
                    case CharI:
                    case CharJ:
                    case CharK:
                    case CharL:
                    case CharM:
                    case CharN:
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
                        // new command
                        if (currentCommand != CharNull)
                            lastCommand = UpdateGCodeValue();

                        currentCommand = c;

                        continue;

                    case CharE:
                    case CharO:
                        // not currently supported
                        break;

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
                            lastCommand = UpdateGCodeValue();
                            comment = String.Empty;
                            isComment = true;
                        }

                        continue;

                    case CharClosingBracket:
                        //comment end
                        lastCommand = UpdateGCodeValue();
                        isComment = false;

                        continue;

                    case CharTab:
                    case CharSpace:
                        if (isComment)
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

        #endregion Private Methods
    }
}
