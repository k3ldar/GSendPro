using GSendShared.Interfaces;

namespace GSendShared.Models
{
    public sealed class ShortcutModel : IShortcut
    {
        public ShortcutModel(string groupName, string name, List<int> defaultKeys, ShortcutKeyDelegate trigger, ShortcutUpdatedDelegate keysUpdated = null)
        {
            if (String.IsNullOrEmpty(groupName))
                throw new ArgumentNullException(nameof(groupName));

            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            GroupName = groupName;
            Name = name;
            DefaultKeys = defaultKeys;
            Trigger = trigger ?? throw new ArgumentNullException(nameof(trigger));
            KeysUpdated = keysUpdated;
        }

        public string GroupName { get; }

        public string Name { get; }

        public ShortcutKeyDelegate Trigger { get; }

        public ShortcutUpdatedDelegate KeysUpdated { get; }

        public List<int> DefaultKeys { get; }
    }
}
