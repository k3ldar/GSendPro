using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

using GSendApi;

using GSendShared;

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
        private readonly string _serverUri;

        public event ProcessMessageHandler ProcessMessage;
        public event EventHandler Connected;
        public event EventHandler ConnectionLost;

        public WebSocketState State => _clientWebSocket.State;

        public GSendWebSocket(Uri serverUri, string clientId, CancellationToken cancellationToken)
        {
            if (serverUri == null)
                throw new ArgumentNullException(nameof(serverUri));

            //if (serverUri.)
            _serverUri = serverUri.ToString();

            if (serverUri.Scheme.Equals("http"))
                _serverUri = _serverUri.Replace("http", "ws");
            else
                _serverUri = _serverUri.Replace("https", "wss");

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

            DateTime startTime = DateTime.UtcNow;

            while (_clientWebSocket.State == WebSocketState.Connecting)
            {
                await Task.Delay(10);

                if (startTime - DateTime.UtcNow > TimeSpan.FromSeconds(10))
                    throw new TimeoutException();

                if (_clientWebSocket.State != WebSocketState.Connecting)
                    break;
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
                    await _clientWebSocket.ConnectAsync(new Uri(String.Format("{0}client2/{1}", _serverUri, clientId)), _cancellationToken).ConfigureAwait(false);
                }

                DateTime dateTime = DateTime.UtcNow;

                while (_clientWebSocket.State == WebSocketState.Connecting || !_isConnected)
                {
                    Trace.WriteLine("Waiting for socket connection");

                    _isConnected = _clientWebSocket.State == WebSocketState.Open;

                    if (_isConnected)
                    {
                        Connected.Invoke(this, EventArgs.Empty);
                    }

                    TimeSpan span = DateTime.UtcNow - dateTime;

                    if (span.TotalSeconds > 5)
                        throw new TimeoutException();


                    await Task.Delay(10);
                }
            }
            catch (ObjectDisposedException)
            {
                //ignore
            }
            catch (InvalidOperationException)
            {
                //ignore
            }
        }

        public async Task SendAsync(string message)
        {
            await ValidateConnection();

            if (_clientWebSocket.State == WebSocketState.Closed)
                return;

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
                catch (GSendApiException)
                {
                    Trace.WriteLine("ApiException");
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
