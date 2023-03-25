using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Interfaces
{
    public interface IGsendSettings
    {
        bool AllowDuplicateComPorts { get; set; }
        int BaudRate { get; set; }
        int DataBits { get; set; }
        string Parity { get; set; }
        int ReadTimeout { get; set; }
        int SendTimeOut { get; set; }
        string StopBits { get; set; }
        int UpdateMilliseconds { get; set; }
        int WriteTimeout { get; set; }
    }
}
