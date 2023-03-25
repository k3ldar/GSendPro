
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

    public delegate void JogCommand(JogDirection jogDirection, double stepSize, double feedRate);
}
