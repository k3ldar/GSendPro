using GSendShared.Abstractions;

namespace GSendCommon.Settings
{
    public sealed class GSendSettings : IGSendSettings
    {
        public bool AllowDuplicateComPorts { get; set; }

        public int WriteTimeout { get; set; } = 1000;

        public int ReadTimeout { get; set; } = 1000;

        public int BaudRate { get; set; } = 115200;

        public string Parity { get; set; } = "None";

        public int DataBits { get; set; } = 8;

        public string StopBits { get; set; } = "One";

        public int SendTimeOut { get; set; } = 1000;

        public int ConnectTimeOut { get; set; } = 10000;

        public int UpdateMilliseconds { get; set; } = 200;

        public string FileFilter { get; set; } = GSend.Language.Resources.FileFilterDefaults;
    }
}
