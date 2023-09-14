using GrblTuning;

using GSendShared.Models;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class TuningWizardMenuItem : IPluginMenu
    {
        private ISenderPluginHost _senderPluginHost;

        public string Text => "Tuning Wizard";

        public int Index => -1;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public MenuParent ParentMenu => MenuParent.Tools;

        public void Clicked()
        {
            using (FrmTuningWizard tuningWizard = new FrmTuningWizard())
            {
                tuningWizard.ShowDialog();
            }
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return _senderPluginHost.IsConnected() && !_senderPluginHost.IsRunning() && !_senderPluginHost.IsPaused();
        }

        public void MachineStatusChanged(MachineStateModel machineStateModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            _senderPluginHost = senderPluginHost as ISenderPluginHost ?? throw new ArgumentNullException(nameof(senderPluginHost));
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = GSend.Language.Resources.ShortcutMenuTools;
            shortcutName = "Grbl Tuning Wizard";
            return true;
        }
    }
}
