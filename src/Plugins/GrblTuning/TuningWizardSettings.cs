using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    internal sealed class TuningWizardSettings
    {
        private MachineStateModel _stateModel;
        private decimal _finalReductionPercent = 10;
        private decimal _newMaxAccelerationX;
        private decimal _newMaxAccelerationY;
        private decimal _newMaxAccelerationZ;
        private decimal _newMaxFeedX;
        private decimal _newMaxFeedY;
        private decimal _newMaxFeedZ;

        public TuningWizardSettings(ISenderPluginHost senderPluginHost)
        {
            SenderPluginHost = senderPluginHost ?? throw new ArgumentNullException(nameof(senderPluginHost));
            Machine = senderPluginHost.GetMachine();
            ExitError = false;
            MaxTravelX = Machine.Settings.MaxTravelX;
            MaxTravelY = Machine.Settings.MaxTravelY;
            MaxTravelZ = Machine.Settings.MaxTravelZ;
            OriginalMaxAccelerationX = Machine.Settings.MaxAccelerationX;
            OriginalMaxAccelerationY = Machine.Settings.MaxAccelerationY;
            OriginalMaxAccelerationZ = Machine.Settings.MaxAccelerationZ;
            OriginalMaxFeedX = Machine.Settings.MaxFeedRateX;
            OriginalMaxFeedY = Machine.Settings.MaxFeedRateY;
            OriginalMaxFeedZ = Machine.Settings.MaxFeedRateZ;
            NewMaxAccelerationX = Machine.Settings.MaxAccelerationX;
            NewMaxAccelerationY = Machine.Settings.MaxAccelerationY;
            NewMaxAccelerationZ = Machine.Settings.MaxAccelerationZ;
            NewMaxFeedX = Machine.Settings.MaxFeedRateX;
            NewMaxFeedY = Machine.Settings.MaxFeedRateY;
            NewMaxFeedZ = Machine.Settings.MaxFeedRateZ;
        }

        public ISenderPluginHost SenderPluginHost { get; }

        public IMachine Machine { get; }

        public decimal MaxTravelX { get; }

        public decimal MaxTravelY { get; }

        public decimal MaxTravelZ { get; }

        public decimal OriginalMaxAccelerationX { get; }

        public decimal OriginalMaxAccelerationY { get; }

        public decimal OriginalMaxAccelerationZ { get; }

        public decimal OriginalMaxFeedX { get; }

        public decimal OriginalMaxFeedY { get; }

        public decimal OriginalMaxFeedZ { get; }

        public bool ExitError { get; set; }

        public decimal NewMaxAccelerationX
        {
            get => _newMaxAccelerationX;

            set
            {
                _newMaxAccelerationX = value;
                FinalMaxAccelerationX = HelperMethods.ReduceValueByPercentage(_newMaxAccelerationX, OriginalMaxAccelerationX, FinalReductionPercent);
                PercentMaxAccelerationX = HelperMethods.FormatPercent(OriginalMaxAccelerationX, FinalMaxAccelerationX);
            }
        }

        public decimal NewMaxAccelerationY
        {
            get => _newMaxAccelerationY;

            set
            {
                _newMaxAccelerationY = value;
                FinalMaxAccelerationY = HelperMethods.ReduceValueByPercentage(_newMaxAccelerationY, OriginalMaxAccelerationY, FinalReductionPercent);
                PercentMaxAccelerationY = HelperMethods.FormatPercent(OriginalMaxAccelerationY, FinalMaxAccelerationY);
            }
        }

        public decimal NewMaxAccelerationZ
        {
            get => _newMaxAccelerationZ;

            set
            {
                _newMaxAccelerationZ = value;
                FinalMaxAccelerationZ = HelperMethods.ReduceValueByPercentage(_newMaxAccelerationZ, OriginalMaxAccelerationZ, FinalReductionPercent);
                PercentMaxAccelerationZ = HelperMethods.FormatPercent(OriginalMaxAccelerationZ, FinalMaxAccelerationZ);
            }
        }

        public decimal NewMaxFeedX
        {
            get => _newMaxFeedX;

            set
            {
                _newMaxFeedX = value;
                FinalMaxFeedX = HelperMethods.ReduceValueByPercentage(_newMaxFeedX, OriginalMaxFeedX, FinalReductionPercent);
                PercentMaxFeedX = HelperMethods.FormatPercent(OriginalMaxFeedX, FinalMaxFeedX);
            }
        }

        public decimal NewMaxFeedY
        {
            get => _newMaxFeedY;

            set
            {
                _newMaxFeedY = value;
                FinalMaxFeedY = HelperMethods.ReduceValueByPercentage(_newMaxFeedY, OriginalMaxFeedY, FinalReductionPercent);
                PercentMaxFeedY = HelperMethods.FormatPercent(OriginalMaxFeedY, FinalMaxFeedY);
            }
        }

        public decimal NewMaxFeedZ
        {
            get => _newMaxFeedZ;

            set
            {
                _newMaxFeedZ = value;
                FinalMaxFeedZ = HelperMethods.ReduceValueByPercentage(_newMaxFeedZ, OriginalMaxFeedZ, FinalReductionPercent);
                PercentMaxFeedZ = HelperMethods.FormatPercent(OriginalMaxFeedZ, FinalMaxFeedZ);
            }
        }

        public double CurrentX { get; private set; }

        public double CurrentY { get; private set; }

        public double CurrentZ { get; private set; }

        public decimal FinalMaxAccelerationX { get; private set; }

        public decimal FinalMaxAccelerationY { get; private set; }

        public decimal FinalMaxAccelerationZ { get; private set; }

        public decimal FinalMaxFeedX { get; private set; }

        public decimal FinalMaxFeedY { get; private set; }

        public decimal FinalMaxFeedZ { get; private set; }

        public string PercentMaxAccelerationX { get; private set; }

        public string PercentMaxAccelerationY { get; private set; }

        public string PercentMaxAccelerationZ { get; private set; }

        public string PercentMaxFeedX { get; private set; }

        public string PercentMaxFeedY { get; private set; }

        public string PercentMaxFeedZ { get; private set; }

        public MachineState CurrentState { get; private set; }

        public decimal FinalReductionPercent
        {
            get => _finalReductionPercent;

            set
            {
                if (_finalReductionPercent == value)
                    return;

                _finalReductionPercent = value;

                // force update of final values
                NewMaxAccelerationX = NewMaxAccelerationX + 10 - 10;
                NewMaxAccelerationY = NewMaxAccelerationY + 10 - 10;
                NewMaxAccelerationZ = NewMaxAccelerationZ + 10 - 10;
                NewMaxFeedX = NewMaxFeedX + 10 - 10;
                NewMaxFeedY = NewMaxFeedY + 10 - 10;
                NewMaxFeedZ = NewMaxFeedZ + 10 - 10;
            }
        }

        public void SafeToContinue(DateTime timeStarted)
        {
            TimeSpan timeTaken = DateTime.UtcNow - timeStarted;

            if (timeTaken.TotalSeconds > 20 || 
                ExitError ||
                _stateModel == null ||
                !_stateModel.IsConnected ||
                _stateModel.IsLocked ||
                _stateModel.IsPaused ||
                _stateModel.IsRunning ||
                _stateModel.SpindleSpeed != 0)
            {
                throw new InvalidOperationException("Unable to continue as machine is disconnected or in use");
            }
        }

        internal void UpdatePosition(MachineStateModel stateModel)
        {
            _stateModel = stateModel;
            CurrentX = stateModel.WorkX;
            CurrentY = stateModel.WorkY;
            CurrentZ = stateModel.WorkZ;
            CurrentState = stateModel.MachineState;
        }

        public void UpdateMachineSettings()
        {
            UpdateMachineValue(OriginalMaxAccelerationX, FinalMaxAccelerationX, "120");
            Machine.Settings.MaxAccelerationX = FinalMaxAccelerationX;
            UpdateMachineValue(OriginalMaxAccelerationY, FinalMaxAccelerationY, "121");
            Machine.Settings.MaxAccelerationY = FinalMaxAccelerationY;
            UpdateMachineValue(OriginalMaxAccelerationZ, FinalMaxAccelerationZ, "122");
            Machine.Settings.MaxAccelerationZ = FinalMaxAccelerationZ;

            UpdateMachineValue(OriginalMaxFeedX, FinalMaxFeedX, "110");
            Machine.Settings.MaxFeedRateX = FinalMaxFeedX;
            UpdateMachineValue(OriginalMaxFeedY, FinalMaxFeedY, "111");
            Machine.Settings.MaxFeedRateY = FinalMaxFeedY;
            UpdateMachineValue(OriginalMaxFeedZ, FinalMaxFeedZ, "112");
            Machine.Settings.MaxFeedRateZ = FinalMaxFeedZ;
        }

        private void UpdateMachineValue(decimal oldValue, decimal newValue, string setting)
        {
            if (oldValue == newValue)
                return;

            string newValueMessage = String.Format(Constants.MessageMachineUpdateSetting,
                Machine.Id,
                $"${setting}={newValue}");
            SenderPluginHost.SendMessage(newValueMessage);
        }
    }
}
