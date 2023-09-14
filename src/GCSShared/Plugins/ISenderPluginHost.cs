using GSendShared.Models;

namespace GSendShared.Plugins
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
