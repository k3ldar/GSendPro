﻿using System.Security.Policy;

namespace GSendCommon
{
    public sealed class GSendSettings
    {
        public bool AllowDuplicateComPorts { get; set; }

        public int WriteTimeout { get; set; } = 1000;

        public int ReadTimeout { get; set; } = 1000;

        public int BaudRate { get; set; } = 115200;

        public string Parity { get; set; } = "None";

        public int DataBits { get; set; } = 8;

        public string StopBits { get; set; } = "One";

        public int SendTimeOut { get; set; } = 1000;
    }
}