using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class WebSocketThread : ThreadManager
    {
        public WebSocketThread(object parameters, TimeSpan runInterval, ThreadManager parent = null, 
            int delayStart = 0, int sleepInterval = 200, bool runAtStart = true, bool monitorCPUUsage = true) 
            : base(parameters, runInterval, parent, delayStart, sleepInterval, runAtStart, monitorCPUUsage)
        {
        }
    }
}
