using System.Text.Json;

namespace GSendShared
{
    public sealed class Constants
    {
        public const int MinConnectionWait = 500;

        public const int SocketKeepAliveMinutes = 5;

        public const int ReceiveBufferSize = 1024 * 8;

        public const string ServerUri = "wss://localhost:7154/client2/{0}/";

        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new();

        public const string SettingsName = "GSend";

        public const string NotificationMachineAdd = "MachineAdd";

        public const string NotificationMachineRemove = "MachineRemove";

        public const string NotificationMachineUpdated = "MachineUpdate";

        public const string MessageMachineStatusAll = "mStatusAll";

        public const string MessageMachinePauseAll = "mPauseAll";

        public const string MessageMachineResumeAll = "mResumeAll";

        public const string MessageMachineStatusServer = "mStatus";

        public const string MessageMachineStatus = "mStatus:{0}";

        public const string MessageMachineResumeServer = "mResume";

        public const string MessageMachineResume = "mResume:{0}";

        public const string MessageMachineHomeServer = "mHome";

        public const string MessageMachineHome = "mHome:{0}";

        public const string MessageMachineConnectServer = "mConnect";

        public const string MessageMachineConnect = "mConnect:{0}";

        public const string MessageMachineDisconnectServer = "mDisconnect";

        public const string MessageMachineDisconnect = "mDisconnect:{0}";

        public const string MessageMachineClearAlarmServer = "mClearAlm";

        public const string MessageMachineClearAlarm = "mClearAlm:{0}";

        public const string MessageMachineProbeServer = "mProbe";

        public const string MessageMachineProbe = "mProbe:{0}";

        public const string MessageMachinePauseServer = "mPause";

        public const string MessageMachinePause = "mPause:{0}";

        public const string MessageMachineStopServer = "mStop";

        public const string MessageMachineStop = "mStop:{0}";

        public const string MessageMachineJogStopServer = "mJogStop";

        public const string MessageMachineJogStop = "mJogStop:{0}";

        public const string MessageMachineJogStartServer = "mJogStart";

        public const string MessageMachineJogStart = "mJogStart:{0}:{1}:{2}:{3}";

        public const string MessageMachineUpdateSettingServer = "mUpdateSetting";

        public const string MessageMachineUpdateSetting = "mUpdateSetting:{0}:{1}";

        public const string MessageMachineSetZeroServer = "mZero";

        public const string MessageMachineSetZero = "mZero:{0}:{1}:{2}";




        public const string StateUndefined = "Undefined";

        public const string StateSleep = "Sleep";

        public const string StateHome = "Home";

        public const string StateCheck = "Check";

        public const string StateDoor = "Door";

        public const string StateAlarm = "Alarm";

        public const string StateJog = "Jog";

        public const string StateRun = "Run";

        public const string StateIdle = "Idle";

        public const string StateHold = "Hold";



        public const string SemiColon = ";";

        public const char EqualsChar = '=';

        public const char ColonChar = ':';


        public const string ProbeCommand = "G91 G21\r\nG38.2 Z-50 F{0}\r\nG92 Z{1}\r\nG0 Z28\r\nM30";

    }
}
