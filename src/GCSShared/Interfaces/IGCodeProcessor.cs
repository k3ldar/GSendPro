using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared
{
    public interface IGCodeProcessor
    {
        bool Connect();

        bool Disconnect();

        bool Start();

        bool Pause();

        bool Resume();

        bool Stop();

        void LoadGCode(IReadOnlyList<IGCodeCommand> commands);

        void ZeroAxis(Axis axis);

        void Clear();

        TimeSpan TimeOut { get; set; }

        void Home();

        string Help();

        void Unlock();

        Dictionary<int, decimal> Settings();

        void UpdateSpindleSpeed(int speed);

        void TurnMistCoolantOn();

        void TurnFloodCoolantOn();

        void CoolantOff();

        long Id { get; }

        bool IsRunning { get; }

        bool IsPaused { get; }

        bool IsConnected { get; }

        bool IsLocked { get; }

        int CommandCount { get; }

        int NextCommand { get; }

        int BufferSize { get; }

        IGCodeCommand ActiveCommand { get; }

        bool SpindleActive { get; }

        int SpindleSpeed { get; }

        bool MistCoolantActive { get; }

        bool FloodCoolantActive { get; }

        event EventHandler OnConnect;

        event EventHandler OnDisconnect;

        event EventHandler OnStart;

        event EventHandler OnStop;

        event EventHandler OnPause;

        event EventHandler OnResume;

        event CommandSentHandler OnCommandSent;

        event BufferSizeHandler OnBufferSizeChanged;

        event EventHandler OnSerialError;

        event EventHandler OnSerialPinChanged;

        event GrblErrorHandler OnGrblError;

        event EventHandler OnInvalidComPort;
    }
}
