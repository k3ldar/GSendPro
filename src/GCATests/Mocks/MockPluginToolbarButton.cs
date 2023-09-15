using System.Diagnostics.CodeAnalysis;
using System.Drawing;

using GSendShared.Models;
using GSendShared.Plugins;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockPluginToolbarButton : IPluginToolbarButton
    {
        public MockPluginToolbarButton(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public ButtonType ButtonType => ButtonType.Button;

        public Image Picture => null;

        public string Text { get; private set; }

        public int Index { get; private set; }

        public void Clicked()
        {
            // not used in this context
        }

        public bool IsEnabled() => true;

        public void MachineStatusChanged(MachineStateModel machineStateModel)
        {
            // not used in this context
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // not used in this context
        }
    }
}
