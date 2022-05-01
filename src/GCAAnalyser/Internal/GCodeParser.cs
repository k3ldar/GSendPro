using System.Text;

using GCAAnalyser.Abstractions;

using static GCAAnalyser.Internal.Consts;

namespace GCAAnalyser.Internal
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

            return InternalParseGCode(UTF8Encoding.UTF8.GetBytes(gCodeCommands));
        }

        #endregion Public Methods

        #region Private Methods

        private IGCodeAnalyses InternalParseGCode(byte[] gCodeCommands)
        {
            GCodeAnalyses Result = new GCodeAnalyses();

            Span<char> line = new Span<char>(new char[MaxLineSize]);
            int position = 0;
            ClearLineData(line);
            GCodeCommand lastCommand = null;
            StringBuilder currentValues = new StringBuilder(MaxLineSize);
            _index = 0;

            for (int i = 0; i < gCodeCommands.Length; i++)
            {
                char c = (char)gCodeCommands[i];

                switch (c)
                {
                    case CharG:
                        if (line[0] != CharNull)
                        {
                            lastCommand = InternalParseLine(Result, line, lastCommand, currentValues);
                            ClearLineData(line);
                        }

                        position = 0;
                        line[position++] = c;

                        continue;

                    case CharLineFeed:
                        lastCommand = InternalParseLine(Result, line, lastCommand, currentValues);
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
                InternalParseLine(Result, line, lastCommand, currentValues);
            }

            return Result;
        }

        private void ClearLineData(in Span<char> line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == CharNull)
                    break;

                line[i] = CharNull;
            }
        }

        private Dictionary<byte, decimal> CreateCommandDictionary()
        {
            byte currValue = 0;

            return new Dictionary<byte, decimal>(InitialDictionarySize)
            {
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0}, // never used but introduced for perf
                { currValue++, 0},
                { currValue++, Decimal.MinValue},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0}, // never used but introduced for perf
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0},
                { currValue++, 0}
            };
        }

        private GCodeCommand InternalParseLine(GCodeAnalyses analysis, in Span<char> line, GCodeCommand lastCommand, StringBuilder currentValues)
        {
            GCodeCommand result = null;

            if (line[0] == CharNull)
                return result;

            currentValues.Clear();

            Dictionary<byte, decimal> values = CreateCommandDictionary();


            char currentCommand = CharNull;
            bool isComment = false;
            string comment = String.Empty;

            void UpdateGCodeValue()
            {
                if (currentCommand != CharNull && currentValues.Length > 0)
                {
                    if (isComment)
                    {
                        comment = currentValues.ToString();
                    }
                    else if (Decimal.TryParse(currentValues.ToString(), out decimal value))
                    {
                        values[(byte)(currentCommand - AsciiAPosition)] = value;
                    }
                }

                currentValues.Clear();
            };

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (isComment && c != CharNull && c != CharClosingBracket)
                {
                    currentValues.Append(c);
                    continue;
                }

                switch (c)
                {
                    case CharNull:
                        UpdateGCodeValue();

                        if (values[CharG - AsciiAPosition] == Decimal.MinValue)
                            values[CharG - AsciiAPosition] = lastCommand.Code;

                        result = new GCodeCommand(_index++, values, comment);

                        if (lastCommand != null)
                            lastCommand.NextCommand = result;

                        result.PreviousCommand = lastCommand;

                        analysis.Add(result);

                        return result;

                    case CharLineFeed:
                        if (currentCommand != CharNull)
                            UpdateGCodeValue();

                        continue;

                    case CharG:
                        currentCommand = c;

                        continue;

                    case CharA:
                    case CharB:
                    case CharC:
                    case CharD:
                    case CharF:
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
                            UpdateGCodeValue();

                        currentCommand = c;

                        continue;

                    case CharOpeningBracket:
                    case CharSemiColon:
                        // comment start
                        if (isComment)
                        {
                            currentValues.Append(c);
                        }
                        else
                        {
                            UpdateGCodeValue();
                            comment = String.Empty;
                            isComment = true;
                        }

                        continue;

                    case CharClosingBracket:
                        //comment end
                        UpdateGCodeValue();
                        isComment = false;

                        continue;

                    case CharTab:
                    case CharSpace:
                        if (isComment)
                        {
                            currentValues.Append(c);
                        }

                        continue;

                    default:
                        currentValues.Append(c);
                        continue;
                }
            }

            UpdateGCodeValue();

            return result;
        }

        #endregion Private Methods
    }
}
