using GSendShared;

namespace GSendCommon
{
    public class ClientBaseMessage : IClientBaseMessage
    {
        public ClientBaseMessage()
        {
        }

        public ClientBaseMessage(string req)
        {
            success = true;
            request = req;
        }

        public ClientBaseMessage(string req, object msg)
            : this(req)
        {
            message = msg;
        }

        public string Identifier { get; set; }

        public bool success { get; set; }

        public string request { get; set; }

        public object message { get; set; }

        public decimal ServerCpuStatus { get; set; }

        public bool IsConnected { get; set; }

#if __LICENSED__
        public bool IsLicensed { get; set; }
#endif

        public long CombinedHash { get; set; }
    }
}
