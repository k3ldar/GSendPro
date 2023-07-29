using System.Net.WebSockets;

namespace GSendShared
{
    public interface IProcessorMediator
    {
        IReadOnlyList<IGCodeProcessor> Machines { get; }

        void OpenProcessors();

        void CloseProcessors();

        Task ProcessClientCommunications(WebSocket webSocket, string clientId);

        CancellationToken CancellationToken { get; }
    }
}
