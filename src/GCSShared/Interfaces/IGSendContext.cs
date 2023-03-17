using Microsoft.Extensions.DependencyInjection;

namespace GSendShared
{
    public interface IGSendContext
    {
        void ShowMachine(IMachine machine);

        void CloseContext();

        IServiceProvider ServiceProvider { get; }

        bool IsClosing { get; }

        
    }
}
