using GSendShared.Models;

using Shared.Communication;

namespace GSendShared
{
    public interface IGCodeProcessor
    {
        ConnectResult Connect();

        bool Disconnect();

        bool Start();

        bool Pause();

        bool Resume();

        bool Stop();

        void LoadGCode(IGCodeAnalyses gCodeAnalyses);

        void ZeroAxis(Axis axis);

        void Clear();

        void Home();

        bool Probe();

        string Help();

        void Unlock();

        void JogStart(JogDirection jogDirection, double stepSize, double feedRate);

        void JogStop();

        UpdateSettingResult UpdateSetting(string updateCommand);

        void WriteLine(string gCode);

        Dictionary<int, object> Settings();

        void UpdateSpindleSpeed(int speed);

        void TurnMistCoolantOn();

        void TurnFloodCoolantOn();

        void CoolantOff();

        long Id { get; }

        TimeSpan TimeOut { get; set; }

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

        event GSendEventHandler OnConnect;

        event GSendEventHandler OnDisconnect;

        event GSendEventHandler OnStart;

        event GSendEventHandler OnStop;

        event GSendEventHandler OnPause;

        event GSendEventHandler OnResume;

        event CommandSentHandler OnCommandSent;

        event BufferSizeHandler OnBufferSizeChanged;

        event GSendEventHandler OnSerialError;

        event GSendEventHandler OnSerialPinChanged;

        event GrblErrorHandler OnGrblError;

        event GrblAlarmHandler OnGrblAlarm;

        event GSendEventHandler OnInvalidComPort;

        event MachineStateHandler OnMachineStateChanged;

        event MessageHandler OnMessageReceived;

        event ResponseReceivedHandler OnResponseReceived;
    }
}
