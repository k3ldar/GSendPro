using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    internal sealed class TuningWizardSettings
    {
        private MachineStateModel _stateModel;

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

        public decimal NewMaxAccelerationX { get; set; }

        public decimal NewMaxAccelerationY { get; set; }

        public decimal NewMaxAccelerationZ { get; set; }

        public decimal NewMaxFeedX { get; set; }

        public decimal NewMaxFeedY { get; set; }

        public decimal NewMaxFeedZ { get; set; }

        public double CurrentX { get; private set; }

        public double CurrentY { get; private set; }

        public double CurrentZ { get; private set; }

        public MachineState CurrentState { get; private set; }

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
    }
}
