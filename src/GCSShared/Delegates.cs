
using GSendShared.Models;

namespace GSendShared
{
    public delegate void CommandSentHandler(IGCodeProcessor sender, CommandSent e);

    public delegate void GrblErrorHandler(IGCodeProcessor sender, GrblError errorCode);

    public delegate void GrblAlarmHandler(IGCodeProcessor sender, GrblAlarm alarm);

    public delegate void BufferSizeHandler(IGCodeProcessor sender, int size);

    public delegate void MachineStateHandler(IGCodeProcessor sender, MachineStateModel machineState);

    public delegate void MessageHandler(IGCodeProcessor sender, string message);

    public delegate void ResponseReceivedHandler(IGCodeProcessor sender, string response);

    public delegate void GSendEventHandler(IGCodeProcessor sender, EventArgs e);

    public delegate void ProcessMessageHandler(string message);

    public delegate void JogCommandHandler(JogDirection jogDirection, double stepSize, double feedRate);

    public delegate void ShortcutKeyDownHandler(bool isKeyDown);

    public delegate void ShortcutUpdatedHandler(List<int> keys);

    public delegate void ShortcutKeyHandler(object sender, ShortcutArgs e);

    public delegate void UpdateLineStatusHandler(int lineNumber, int masterLineNumber, LineStatus lineStatus);

    public delegate void InformationUpdateHandler(InformationType informationType, string message);

    public delegate void ConfigurationUpdatedHandler(ConfigurationUpdatedMessage configurationUpdatedMessage);
}
