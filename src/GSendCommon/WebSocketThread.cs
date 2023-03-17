using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class WebSocketThread : ThreadManager
    {
        public WebSocketThread(ClientWebSocket clientWebSocket, TimeSpan runInterval) 
            : base(clientWebSocket, runInterval)
        {
            base.HangTimeout = Int32.MaxValue;
        }
    }
}
