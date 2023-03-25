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

        [Route("/webclient")]
        public async Task ClientConnection()
        {
            string clientId = "Web";
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                try
                {
                    using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await _processorMediator.ProcessClientCommunications(webSocket, clientId);
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

        [Route("/client2/{clientId}")]
        public async Task ClientConnection2(string clientId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                try
                {
                    using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await _processorMediator.ProcessClientCommunications(webSocket, clientId);
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
