using System.Text.Json;

namespace GSendShared
{
    public static class Constants
    {
        public const int MinimumMachineNameLength = 5;
        public const int MaximumMachineNameLength = 40;
        public const int MaxSubCommandRecursionDepth = 15;
        public const int MinSpindleSpeed = 0;
        public const int MaxSpindleSpeed = 35000;

        public const int MaxLineSize = 256;
        public const byte AsciiAPosition = 65;

        public const char CharNull = '\0';
        public const char CharCarriageReturn = '\r';
        public const char CharLineFeed = '\n';
        public const char CharTab = '\t';
        public const char CharSpace = ' ';
        public const char CharOpeningBracket = '(';
        public const char CharClosingBracket = ')';
        public const char CharSemiColon = ';';
        public const char CharColon = ':';
        public const char CharA = 'A';
        public const char CharB = 'B';
        public const char CharC = 'C';
        public const char CharD = 'D';
        public const char CharE = 'E';
        public const char CharF = 'F';
        public const char CharG = 'G';
        public const char CharH = 'H';
        public const char CharI = 'I';
        public const char CharJ = 'J';
        public const char CharK = 'K';
        public const char CharL = 'L';
        public const char CharM = 'M';
        public const char CharN = 'N';
        public const char CharO = 'O';
        public const char CharP = 'P';
        public const char CharQ = 'Q';
        public const char CharR = 'R';
        public const char CharS = 'S';
        public const char CharT = 'T';
        public const char CharU = 'U';
        public const char CharV = 'V';
        public const char CharW = 'W';
        public const char CharX = 'X';
        public const char CharY = 'Y';
        public const char CharZ = 'Z';
        public const char CharPercent = '%';
        public const char CharVariable = '#';
        public const char CharDollar = '$';
        public const char CharEquals = '=';
        public const char CharVariableBlockStart = '[';
        public const char CharVariableBlockEnd = ']';

        public const int MCode600Pause = 600;
        public const int MCode601Timeout = 601;
        public const int MCode602JobName = 602;
        public const int MCode605 = 605;
        public const int MCode620 = 620;
        public const int MCode621 = 621;
        public const int MCode622 = 622;
        public const int MCode623 = 623;
        public const int MCode630RunProgram = 630;
        public const decimal MCode630RunProgramParams = 630.1M;
        public const int MCode631RunProgram = 631;
        public const decimal MCode631RunProgramParams = 631.1M;
        public const decimal MCode631RunProgramResult = 631.2M;

        public const int MCodeMinTimeoutValue = 100;
        public const int MCodeMaxTimeoutValue = 10000;


        public const int SystemVariableCount = 1;
        public const ushort SystemVariableTimeout = 1;
        public const ushort SystemVariableMax = 99;


        public const string HomeWebsite = "https://www.gsend.pro/";

        public const string HelpWebsite = "https://www.gsend.pro/help";

        public const int MaximumRecentFiles = 10;

        public const int WarningStatusWidth = 40;

        public const int QueueProcessMilliseconds = 20;

        public const int MinConnectionWait = 500;

        public const int SocketKeepAliveMinutes = 5;

        public const int ReceiveBufferSize = 1024 * 8;

        public const string GSendProAppFolder = "GSendPro";

        public const string GSendProDbFolder = "db";

        public const string GSendProSubProgramFolder = "Sub Programs";

        public const string GSendProErrorFolder = "Errors";

        public const string GSendProDesktopFolder = "DeskTop";

        public const string AppSettings = "appsettings.json";

        public const string AppPluginFile = "plugins.json";

        public const string GSendPathEnvVar = "GSendProRootPath";

        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
        };

        public const string SettingsName = "GSend";

        public const string NotificationMachineAdd = "MachineAdd";

        public const string NotificationMachineRemove = "MachineRemove";

        public const string NotificationMachineUpdated = "MachineUpdate";

        public const string NotificationJobProfileAdd = "JobProfileAdd";

        public const string NotificationJobProfileRemove = "JobProfileRemove";

        public const string NotificationJobProfileUpdated = "JobProfileUpdated";

        public const string NotificationJobExecutionAdd = "JobExecutionAdd";

        public const string NotificationMachineConnected = "MachineConnected";

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

        public const string MessageMachineWriteLineServer = "mWriteLine";

        public const string MessageMachineWriteLine = "mWriteLine:{0}:{1}";

        public const string MessageMachineWriteLineServerR = "mWriteLineR";

        public const string MessageMachineWriteLineR = "mWriteLineR:{0}:{1}";

        public const string MessageMachineSpindleAdmin = "mSpindle";

        public const string MessageMachineSpindle = "mSpindle:{0}:{1}:{2}";

        public const string MessageMachineUpdateOverridesAdmin = "mOverrides";

        public const string MessageMachineUpdateOverrides = "mOverrides:{0}:{1}";

        public const string MessageLoadGCodeAdmin = "mLoad";

        public const string MessageLoadGCode = "mLoad:{0}:{1}";

        public const string MessageUnloadGCodeAdmin = "mUnload";

        public const string MessageUnloadGCode = "mUnload:{0}";

        public const string MessageToggleSimulationServer = "mToggleSimulation";

        public const string MessageToggleSimulation = "mToggleSimulation:{0}";

        public const string MessageLineStatusUpdated = "LineStatusUpdated";

        public const string MessageInformationUpdate = "mInfoUpdate";

        public const string MessageConfigurationUpdated = "mConfigUpdate";

        public const string MessageAddEventsAdmin = "mAddEvents";

        public const string MessageRemoveEventsAdmin = "mRemoveEvents";

        public const string StateUndefined = "Undefined";

        public const string MessageRunGCodeAdmin = "mStart";

        public const string MessageRunGCode = "mStart:{0}:{1}:{2}";

        public const string InvalidComPort = "InvalidComPort";

        public const string ComPortTimeOut = "ComPortTimeout";

        public const string StateChanged = "StateChanged";

        public const string StateSleep = "Sleep";

        public const string StateHome = "Home";

        public const string StateCheck = "Check";

        public const string StateDoor = "Door";

        public const string StateAlarm = "Alarm";

        public const string StateJog = "Jog";

        public const string StateRun = "Run";

        public const string StateIdle = "Idle";

        public const string StateHold = "Hold";

        public const string Pause = "Pause";

        public const string Resume = "Resume";

        public const string Stop = "Stop";

        public const string Start = "Start";

        public const string Connect = "Connect";

        public const string Disconnect = "Disconnect";

        public const string CommandSent = "CommandSent";

        public const string ResponseReceived = "ResponseReceived";

        public const string MessageReceived = "MessageReceived";

        public const string GrblError = "GrblError";

        public const string SerialPinChanged = "SerialPinChanged";

        public const string SerialError = "SerialError";

        public const string SemiColon = ";";

        public const char EqualsChar = '=';

        public const char ColonChar = ':';

        public const string ProbeCommand = "G91 G21\r\nG38.2 Z-50 F{0}\r\nG92 Z{1}\r\nG0 Z28\r\nM30";

        public const string StartWizardSelectedJob = "StartJobSelectedProfileName";

        public const string DefaultSubProgramFileExtension = ".sub";
    }
}
