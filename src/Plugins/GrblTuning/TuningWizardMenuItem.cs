using System.Text.Json;

using GSendControls;

using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class TuningWizardMenuItem : IPluginMenu
    {
        private ISenderPluginHost _senderPluginHost;
        private TuningWizardSettings _wizardSettings;
        private bool _isWizardShowing = false;
        private readonly IPluginMenu _parentMenu;
        private bool _isHoming = false;
        private MachineState _lastMachineState = MachineState.Idle;

        public TuningWizardMenuItem(IPluginMenu parentMenu)
        {
            _parentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
        }

        public string Text => GSend.Language.Resources.TuneWizard;

        public int Index => -1;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => _parentMenu;

        public bool ReceiveClientMessages => true;

        public void Clicked()
        {
            _wizardSettings.ExitError = false;
            _isWizardShowing = true;
            using WizardForm wizardForm = new(GSend.Language.Resources.TuneWizard,
                new BaseWizardPage[]
                {
                    new PageWelcome(_wizardSettings),
                    new MachineMoveLocation(_wizardSettings),
                    new MachineTuneXAxisFeedRate(_wizardSettings),
                    new MachineTuneYAxisFeedRate(_wizardSettings),
                    new MachineTuneZAxisFeedRate(_wizardSettings),
                    new MachineTuneXAxisAcceleration(_wizardSettings),
                    new MachineTuneYAxisAcceleration(_wizardSettings),
                    new MachineTuneZAxisAcceleration(_wizardSettings),
                    new PageFinish(_wizardSettings)
                });

            DialogResult wizardResult = wizardForm.ShowDialog();

            long machineId = _wizardSettings.Machine.Id;

            if (wizardResult == DialogResult.Cancel)
            {
                string feedX = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$110={_wizardSettings.OriginalMaxFeedX}");
                string feedY = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$111={_wizardSettings.OriginalMaxFeedY}");
                string feedZ = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$112={_wizardSettings.OriginalMaxFeedZ}");
                string accelX = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$120={_wizardSettings.OriginalMaxAccelerationZ}");
                string accelY = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$121={_wizardSettings.OriginalMaxAccelerationY}");
                string accelZ = String.Format(Constants.MessageMachineUpdateSetting, machineId, $"$122={_wizardSettings.OriginalMaxAccelerationZ}");

                _wizardSettings.SenderPluginHost.SendMessage(feedX);
                _wizardSettings.SenderPluginHost.SendMessage(feedY);
                _wizardSettings.SenderPluginHost.SendMessage(feedZ);
                _wizardSettings.SenderPluginHost.SendMessage(accelX);
                _wizardSettings.SenderPluginHost.SendMessage(accelY);
                _wizardSettings.SenderPluginHost.SendMessage(accelZ);
            }

            _isWizardShowing = false;
        }

        public bool IsChecked() => false;

        public bool IsEnabled()
        {
            return _senderPluginHost.IsConnected() && !_senderPluginHost.IsRunning() && !_senderPluginHost.IsPaused();
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            if (!_isWizardShowing)
                return;

            switch (clientMessage.request)
            {
                case Constants.StateChanged:
                case Constants.MessageMachineStatusServer:
                    MachineStateModel stateModel = JsonSerializer.Deserialize<MachineStateModel>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    if (stateModel == null)
                        return;

                    if (!_wizardSettings.ExitError && !stateModel.IsConnected)
                        _wizardSettings.ExitError = true;

                    _wizardSettings.UpdatePosition(stateModel);

                    if (_isHoming != stateModel.IsHoming)
                    {
                        _isHoming = stateModel.IsHoming;
                        _wizardSettings.RaiseStateChanged(MachineState.Home);
                    }

                    if (_lastMachineState != stateModel.MachineState)
                    {
                        _lastMachineState = stateModel.MachineState;
                        _wizardSettings.RaiseStateChanged(_lastMachineState);
                    }

                    break;

                case Constants.Disconnect:
                case Constants.GrblError:
                case Constants.StateAlarm:
                    _wizardSettings.ExitError = true;
                    break;
            }
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            _senderPluginHost = senderPluginHost as ISenderPluginHost ?? throw new ArgumentNullException(nameof(senderPluginHost));
            _wizardSettings = new TuningWizardSettings(_senderPluginHost);
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = GSend.Language.Resources.ShortcutMenuTools;
            shortcutName = GSend.Language.Resources.TuneWizard;
            return true;
        }
    }
}
