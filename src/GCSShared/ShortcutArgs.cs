namespace GSendShared
{
    public sealed class ShortcutArgs
    {
        public ShortcutArgs(string name, List<int> keys)
        {
            Name = name;
            Keys = keys;
        }

        public List<int> Keys { get; }

        public string Name { get; }
    }
}
