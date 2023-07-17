using GSendShared.Models;

namespace GSendShared
{
    public interface IGCodeProcessor
    {
        ConnectResult Connect();

        bool Disconnect();

        bool Start(IJobExecution jobExecution);

        bool Pause();

        bool Resume();

        bool Stop();

        bool LoadGCode(IGCodeAnalyses gCodeAnalyses);

        bool ZeroAxes(ZeroAxis axis, int coordinateSystem);

        void Clear();

        bool Home();

        bool Probe();

        string Help();

        bool Unlock();

        bool JogStart(JogDirection jogDirection, double stepSize, double feedRate);

        bool JogStop();

        UpdateSettingResult UpdateSetting(string updateCommand);

        bool WriteLine(string gCode);

        string SendCommandWaitForOKCommand(string commandText);

        void QueueCommand(string commandText);

        Dictionary<int, object> Settings();

        bool UpdateSpindleSpeed(int speed, bool clockWise);

        bool TurnMistCoolantOn();

        bool TurnFloodCoolantOn();

        bool CoolantOff();

        bool ToggleSimulation();

        OverrideModel MachineOverrides { get; set; }

        long Id { get; }

        TimeSpan TimeOut { get; set; }

        TimeSpan HomingTimeout { get; set; }

        string Cpu { get; }

        bool IsRunning { get; }

        bool IsPaused { get; }

        bool IsConnected { get; }

        bool IsLocked { get; }

        int CommandCount { get; }

        int LineCount { get; }

        int NextCommand { get; }

        int BufferSize { get; }

        bool SpindleActive { get; }

        int SpindleSpeed { get; }

        bool MistCoolantActive { get; }

        bool FloodCoolantActive { get; }

        MachineStateModel StateModel { get; }

        IMachine Machine { get; }

        event GSendEventHandler OnConnect;

        event GSendEventHandler OnDisconnect;

        event GSendEventHandler OnStart;

        event GSendEventHandler OnStop;

        event GSendEventHandler OnPause;

        event GSendEventHandler OnResume;

        event CommandSentHandler OnCommandSent;

        event GSendEventHandler OnSerialError;

        event GSendEventHandler OnSerialPinChanged;

        event GrblErrorHandler OnGrblError;

        event GrblAlarmHandler OnGrblAlarm;

        event GSendEventHandler OnInvalidComPort;

        event GSendEventHandler OnComPortTimeOut;

        event MachineStateHandler OnMachineStateChanged;

        event MessageHandler OnMessageReceived;

        event ResponseReceivedHandler OnResponseReceived;

        event UpdateLineStatus OnLineStatusUpdated;
    }
}
