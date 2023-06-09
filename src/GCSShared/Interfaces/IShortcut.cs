namespace GSendShared.Interfaces
{
    public interface IShortcut
    {
        /// <summary>
        /// Group name for shortcut
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// Unique name of shortcut
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Delegate that is called to perform action of shortcut
        /// </summary>
        ShortcutKeyDelegate Trigger { get; }

        /// <summary>
        /// Existing key combination (maps to System.Windows.Forms.Keys)
        /// </summary>
        List<int> DefaultKeys { get; }
    }
}
