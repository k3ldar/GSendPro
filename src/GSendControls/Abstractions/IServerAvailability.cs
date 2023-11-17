using System.Windows.Forms;

namespace GSendControls.Abstractions
{
    internal interface IServerAvailability
    {
        void UpdateServerAvailability(bool isAvailable, ListViewItem item);
    }
}
