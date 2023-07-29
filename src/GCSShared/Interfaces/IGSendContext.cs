using GSendShared.Abstractions;

namespace GSendShared
{
    public interface IGSendContext
    {
        void ShowMachine(IMachine machine);

        void CloseContext();

        IServiceProvider ServiceProvider { get; }

        bool IsClosing { get; }

        IGSendSettings Settings { get; }
    }
}
