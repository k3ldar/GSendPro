using GrblTuning;

using GSendShared.Models;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class TuningWizardMenuItem : IPluginMenu
    {
        public string Name => "Tuning Wizard";

        public int Index => -1;

        public MenuType MenuType => MenuType.MenuItem;

        public MenuParent ParentMenu => MenuParent.Tools;

        public void Clicked()
        {
            using (FrmTuningWizard tuningWizard = new FrmTuningWizard())
            {
                tuningWizard.ShowDialog();
            }
        }

        public void MachineStatusChanged(MachineStateModel machineStateModel)
        {
            throw new NotImplementedException();
        }
    }
}
