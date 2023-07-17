namespace GSendShared
{
    public interface IGCodeLine
    {
        LineStatus Status { get; set; }

        List<IGCodeCommand> Commands { get; }

        /// <summary>
        /// Retrieves gcode that can be sent directly to the grbl processor,
        /// with this all variable blocks etc would have been replaced
        /// </summary>
        /// <returns></returns>
        string GetGCode();

        IGCodeLine GetGCode(int feedRate);

        IGCodeLineInfo GetGCodeInfo();

        bool IsCommentOnly { get; }

        int LineNumber { get; }

        int MasterLineNumber { get; }
    }
}
