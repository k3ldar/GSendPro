using System.Text.RegularExpressions;

using FastColoredTextBoxNS;

using Range = FastColoredTextBoxNS.Range;

namespace GSendEditor
{
    public sealed class GCodeSyntaxHighLighter : SyntaxHighlighter
    {
        private Regex GCodeCommentRegex1;
        private Regex GCodeCommentRegex2;
        private Regex GCodeCommentRegex3;
        private Regex GCodeNumberRegex;
        private Regex GCodeVariableRegex;
        private Regex GCodeVariableGroupRegex;
        private Regex GCodeKeywordRegex;

        public GCodeSyntaxHighLighter(FastColoredTextBox currentTb)
          : base(currentTb)
        {
            InitGCodeRegex();
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            AttributeStyle = RedStyle;
            KeywordStyle = BlueStyle;
            CommentTagStyle = GrayStyle;
        }

        public override void HighlightSyntax(Language language, Range range)
        {
            GCodeSyntaxHighlight(range);
        }

        private void InitGCodeRegex()
        {
            GCodeCommentRegex1 = new Regex(@"\;.*$", RegexCompiledOption);
            GCodeCommentRegex2 = new Regex(@"\(([^\)]+)\)", RegexCompiledOption);
            GCodeCommentRegex3 = new Regex(@"\;.*\n", RegexCompiledOption);
            GCodeNumberRegex = new Regex(@"[0-9]", RegexCompiledOption);
            GCodeVariableRegex = new Regex(@"(#[0-9]*)", RegexCompiledOption);
            GCodeVariableGroupRegex = new Regex(@"[\[\]\*\+\=\-\/]", RegexCompiledOption);
            GCodeKeywordRegex = new Regex(@"(A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z)", RegexCompiledOption);
        }

        /// <summary>
        /// Highlights GCode
        /// </summary>
        /// <param name="range"></param>
        public void GCodeSyntaxHighlight(Range range)
        {
            //clear style of changed range
            range.ClearStyle(GreenStyle, MagentaStyle, RedStyle, BlueStyle);

            //comment highlighting
            range.SetStyle(GreenStyle, GCodeCommentRegex1);
            range.SetStyle(GreenStyle, GCodeCommentRegex2);
            range.SetStyle(GreenStyle, GCodeCommentRegex3);

            //variable highlighting
            range.SetStyle(MagentaStyle, GCodeVariableRegex);

            //attribute highlighting
            range.SetStyle(RedStyle, GCodeVariableGroupRegex);

            //keyword highlighting
            range.SetStyle(BlueStyle, GCodeKeywordRegex);

            //clear folding markers
            range.ClearFoldingMarkers();
        }
    }
}