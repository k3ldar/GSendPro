namespace GSendShared.Models
{
    public sealed class M623Model
    {
        public M623Model(string comPort, string response, int timeout, string command)
        {
            ComPort = comPort;
            Response = response;
            Timeout = timeout;
            Command = command;
        }

        public string ComPort { get; }

        public string Response { get; }

        public int Timeout { get; }

        public string Command { get; }
    }
}
