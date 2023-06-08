namespace GSendShared.Interfaces
{
    public interface IShortcut
    {
        string Name { get; }

        ShortcutKeyDelegate Trigger { get; }
    }
}
