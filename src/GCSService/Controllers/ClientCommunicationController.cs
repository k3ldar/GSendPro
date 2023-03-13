using System.Net.WebSockets;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    public class ClientCommunicationController : BaseController
    {
        private readonly IProcessorMediator _processorMediator;
        public ClientCommunicationController(IProcessorMediator processorMediator)
        {
            _processorMediator = processorMediator ?? throw new ArgumentNullException(nameof(processorMediator));
        }

        [Route("/client")]
        public async Task ClientConnection()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                try
                {
                    using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await _processorMediator.ProcessClientCommunications(webSocket);
                }
                catch (WebSocketException)
                {

                }
                catch (Exception ex)
                {
                    if (ex.Message == "hello")
                    {

                    }
                }
            }
            else
            {
                HttpContext.Response.StatusCode = SharedPluginFeatures.Constants.HtmlResponseBadRequest;
            }
        }
    }
}
