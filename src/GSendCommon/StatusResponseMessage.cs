namespace GSendCommon
{
    public sealed class StatusResponseMessage
    {
        public long Id { get; set; }

        public bool Connected { get; set; }

        public string State { get; set; }

        public string CpuStatus { get; set; }
    }
}
