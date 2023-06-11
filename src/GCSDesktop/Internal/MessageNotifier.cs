using System.Windows.Forms;

using GSendDesktop.Abstractions;

namespace GSendDesktop.Internal
{
    internal class MessageNotifier : IMessageNotifier
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName);
        }
    }
}
