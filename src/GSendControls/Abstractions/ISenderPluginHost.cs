using GSendShared;
using GSendShared.Models;

namespace GSendControls.Abstractions
{
    public interface ISenderPluginHost : IPluginHost
    {
        bool IsPaused();

        bool IsRunning();

        bool IsConnected();

        void SendMessage(string message);

        MachineStateModel MachineStatus();

        IMachine GetMachine();
    }
}
