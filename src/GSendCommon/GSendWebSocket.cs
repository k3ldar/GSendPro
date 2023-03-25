using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GSendShared;

using Shared.Classes;

using static GSendShared.Constants;

namespace GSendCommon
{

    public sealed class GSendWebSocket
    {
        private ClientWebSocket _clientWebSocket;
        private readonly CancellationToken _cancellationToken;
        private bool _isConnected = false;
        private DateTime _lastConnectionAttempt = DateTime.MinValue;
        private readonly string _clientId;

        public event ProcessMessageHandler ProcessMessage;
        public event EventHandler Connected;
        public event EventHandler ConnectionLost;

        public WebSocketState State => _clientWebSocket.State;

        public GSendWebSocket(CancellationToken cancellationToken, string clientId)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _cancellationToken = cancellationToken;
            Task.Run(() => ReceiveMessageAsync()).ConfigureAwait(false);
        }

        private async Task ValidateConnection()
        {
            if (_isConnected && _clientWebSocket.State != WebSocketState.Open)
            {
                ConnectionLost?.Invoke(this, EventArgs.Empty);
                _isConnected = false;
            }
            else if (!_isConnected)
            {
                TimeSpan span = DateTime.UtcNow - _lastConnectionAttempt;

                if (span.TotalMilliseconds > MinConnectionWait)
                {
                    _lastConnectionAttempt = DateTime.UtcNow;
                    await ConnectToWebSocket(_clientId).ConfigureAwait(false);
                }
            }
        }

        private void SetupWebSocket()
        {
            _clientWebSocket = new ClientWebSocket();

            _clientWebSocket.Options.KeepAliveInterval = TimeSpan.FromMinutes(SocketKeepAliveMinutes);
        }

        private async Task ConnectToWebSocket(string clientId)
        {
            try
            {
                if (_clientWebSocket == null || _clientWebSocket.State == WebSocketState.Closed || _clientWebSocket.State == WebSocketState.Aborted)
                {
                    Trace.Write("Calling SetupWebSocket");
                    SetupWebSocket();
                }

                if (_clientWebSocket.State != WebSocketState.Open && _clientWebSocket.State != WebSocketState.Connecting)
                {
                    Trace.Write("Calling SocketConnectAsync");
                    await _clientWebSocket.ConnectAsync(new Uri(String.Format(ServerUri, clientId)), _cancellationToken).ConfigureAwait(false);
                }

                while (_clientWebSocket.State == WebSocketState.Connecting || !_isConnected)
                {
                    Trace.Write("Waiting for socket connection");

                    _isConnected = _clientWebSocket.State == WebSocketState.Open;

                    if (_isConnected)
                    {
                        Connected.Invoke(this, EventArgs.Empty);
                    }

                    await Task.Delay(10);
                }
            }
            catch (ObjectDisposedException)
            {

            }
            catch (InvalidOperationException)
            {

            }
        }

        public async Task SendAsync(string message)
        {
            await ValidateConnection();

            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            await _clientWebSocket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, _cancellationToken).ConfigureAwait(false);
        }

        private async Task ReceiveMessageAsync()
        {
            byte[] buffer = new byte[ReceiveBufferSize];

            while (true)
            {
                await ValidateConnection();

                if (!_isConnected)
                    continue;

                try
                {
                    WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationToken);

                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    //Trace.WriteLine($"Message Received: {message[..result.Count]}");
                    ProcessMessage?.Invoke(message[..result.Count]);
                }
                catch (IOException)
                {
                    Trace.WriteLine("IO Exception");
                }
                catch (InvalidOperationException)
                {
                    Trace.WriteLine("Invalid operation");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error in receiving messages: {err}", ex.Message);
                    break;
                }
            }
        }
    }
}
