using System.Text;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gsend.pro
{
    public static class HtmlHelpers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Not used but required for extension method")]
        public static HtmlString ToGCode(this IHtmlHelper htmlContent, string gCode)
        {
            return new HtmlString(ColorizeGCode(gCode));
        }

        private enum LastCodeType
        {
            None,
            Letter,
            Number,
            CommentStart,
            CommentEnd,
            VariableStart,
            VariableEnd,
            VariableCodeBlock
        }
        private static string ColorizeGCode(string gcode)
        {
            StringBuilder Result = new();
            LastCodeType lastCode = LastCodeType.None;
            
            void CloseBlock(string color, LastCodeType currentCode)
            {
                if (lastCode != LastCodeType.None && lastCode != currentCode)
                {
                    Result.Append("</span>");
                }

                if (lastCode != currentCode)
                {
                    lastCode = currentCode;
                    Result.Append("<span class=\"gcode-");
                    Result.Append(color);
                    Result.Append("\">");
                }
            }

            for (int i = 0; i < gcode.Length; i++)
            {
                bool peekAhead = i < gcode.Length - 1;

                char c = gcode[i];

                if (c == 91 || c == 93)
                {
                    CloseBlock("brown", LastCodeType.VariableCodeBlock);
                }
                if (c == 35)
                {
                    // variable start
                    CloseBlock("magenta", LastCodeType.VariableStart);
                }
                else if (c == 61)
                {
                    Result.Append(c);
                    CloseBlock("black", LastCodeType.VariableEnd);
                    continue;
                }
                else if (IsCommentStart(c))
                {
                    CloseBlock("green", LastCodeType.CommentStart);
                    // comment for everything after this
                }
                else if (IsCommentEnd(c))
                {
                    CloseBlock("black", LastCodeType.CommentEnd);
                }
                else if (IsNumber(c))
                {
                    if (lastCode != LastCodeType.CommentStart && lastCode != LastCodeType.VariableStart)
                        CloseBlock("black", LastCodeType.Number);
                    //number

                }
                else if (IsLetter(c))
                {
                    // a to z
                    if (lastCode != LastCodeType.CommentStart)
                        CloseBlock("blue", LastCodeType.Letter);
                }
                else if (c == 10)
                {
                    Result.Append("<br />");

                    if (lastCode.Equals(LastCodeType.CommentStart))
                        lastCode = LastCodeType.CommentEnd;
                }

                Result.Append(c);
            }

            Result.Append("</span>");

            return Result.ToString();
        }

        private static bool IsCommentStart(char c)
        {
            return c == 59 || c == 40;
        }

        private static bool IsCommentEnd(char c)
        {
            return c == 41;
        }

        private static bool IsLetter(char c)
        {
            return c > 64 && c < 91 || c > 96 && c < 123;
        }

        private static bool IsNumber(char c)
        {
            return c > 47 && c < 58;
        }
    }
}
