namespace GSendShared.Abstractions
{
    public interface IGSendSettings
    {
        bool AllowDuplicateComPorts { get; set; }

        int WriteTimeout { get; set; }

        int ReadTimeout { get; set; }

        int BaudRate { get; set; }

        string Parity { get; set; }

        int DataBits { get; set; }

        string StopBits { get; set; }

        int SendTimeOut { get; set; }

        public int ConnectTimeOut { get; set; }

        int UpdateMilliseconds { get; set; }

        string FileFilter { get; set; }

        int MaximumLineLength { get; set; }

        int MaximumBufferSize { get; set; }
    }
}
