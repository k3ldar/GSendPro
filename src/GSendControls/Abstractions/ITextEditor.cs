namespace GSendControls.Abstractions
{
    /// <summary>
    /// Text Editor interface
    /// </summary>
    public interface ITextEditor
    {
        void ShowGoToDialog();

        int LineCount { get; }
    }
}
