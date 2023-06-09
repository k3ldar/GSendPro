using GSendShared.Interfaces;

namespace GSendShared.Models
{
    public sealed class ShortcutModel : IShortcut
    {
        public ShortcutModel(string groupName, string name, ShortcutKeyDelegate trigger, List<int> defaultKeys)
        {
            if (String.IsNullOrEmpty(groupName))
                throw new ArgumentNullException(nameof(groupName));

            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            GroupName = groupName;
            Name = name;
            Trigger = trigger ?? throw new ArgumentNullException(nameof(trigger));
            DefaultKeys = defaultKeys;
        }

        public string GroupName { get; }

        public string Name { get; }

        public ShortcutKeyDelegate Trigger { get; }

        public List<int> DefaultKeys { get; }
    }
}
