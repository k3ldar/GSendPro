using System.Collections.Generic;

namespace gsend.pro.Internal
{
    public sealed class ClientMenuItems
    {
        public ClientMenuItems()
        {
            MenuItems = new();
        }

        public Dictionary<string, string> MenuItems { get; set; }
    }
}
