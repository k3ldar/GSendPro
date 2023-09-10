using GSendShared.Models;

namespace GSendShared.Plugins
{
    public interface ISenderPluginHost : IPluginHost
    {
        bool IsPaused();

        bool IsRunning();

        void SendMessage(string message);

        MachineStateModel MachineStatus();

        IMachine GetMachine();
    }
}
