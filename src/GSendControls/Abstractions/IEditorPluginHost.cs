using FastColoredTextBoxNS;

namespace GSendControls.Abstractions
{
    public interface IEditorPluginHost : IPluginHost
    {
        /// <summary>
        /// indicates that the editor has unsaved changes
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Returns true if a subprogram is loaded, otherwise false for a file
        /// </summary>
        bool IsSubprogram { get; }

        /// <summary>
        /// Name of file that is loaded (regular file or subprogram)
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// In app editor
        /// </summary>
        FastColoredTextBox Editor { get; }
    }
}
