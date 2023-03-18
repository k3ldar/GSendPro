using System.Text.Json;

namespace GSendShared
{
    public sealed class Constants
    {
        public const int MinConnectionWait = 500;

        public const int SocketKeepAliveMinutes = 5;

        public const int ReceiveBufferSize = 1024 * 4;

        public const string ServerUri = "wss://localhost:7154/client2";

        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new();

        public const string SettingsName = "GSend";

        public const string NotificationMachineAdd = "MachineAdd";

        public const string NotificationMachineRemove = "MachineRemove";

        public const string NotificationMachineUpdated = "MachineUpdate";

        public const string MessageMachineStatus = "mStatus";

        public const string MessageMachinePauseAll = "mPauseAll";

        public const string MessageMachineResumeAll = "mResumeAll";

        public const string MessageMachineResume = "mResume:{0}";

        public const string MessageMachineHome = "mHome:{0}";

        public const string MessageMachineConnect = "mConnect:{0}";

        public const string MessageMachineClearAlarm = "mClearAlm:{0}";

        public const string StateUndefined = "Undefined";

        public const string StateSleep = "Sleep";

        public const string StateHome = "Home";

        public const string StateCheck = "Check";

        public const string StateDoor = "Door";

        public const string StateAlarm = "Alarm";

        public const string StateJog = "Jog";

        public const string StateRun = "Run";

        public const string StateIdle = "Idle";
    }
}
