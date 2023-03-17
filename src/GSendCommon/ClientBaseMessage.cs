namespace GSendCommon
{
    public class ClientBaseMessage
    {
        public bool success { get; set; }

        public string request { get; set; }

        public object message { get; set; }

        public decimal ServerCpuStatus { get; set; }
    }
}
