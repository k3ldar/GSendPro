using GSendShared.Interfaces;

namespace GSendShared.Models
{
    public sealed class ShortcutModel : IShortcut
    {
        public ShortcutModel(string name, ShortcutKeyDelegate trigger)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            Trigger = trigger ?? throw new ArgumentNullException(nameof(trigger));
        }

        public string Name { get; }

        public ShortcutKeyDelegate Trigger { get; }
    }
}
