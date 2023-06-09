using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
