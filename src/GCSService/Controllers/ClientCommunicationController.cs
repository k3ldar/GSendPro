using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

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
                    // not used in this context
                }
                catch (Exception ex)
                {
                    if (ex.Message == "hello")
                    {
                        // used for breakpoints only
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
                catch (Exception)
                {
                    // do not throw the exception
                }
            }
            else
            {
                HttpContext.Response.StatusCode = SharedPluginFeatures.Constants.HtmlResponseBadRequest;
            }
        }
    }
}
