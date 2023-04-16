using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class MachineUpdateThread : ThreadManager
    {
        
        public MachineUpdateThread(TimeSpan runInterval)
            : base(null, runInterval)
        {

        }

        public bool IsThreadRunning {  get; set; }

        protected override bool Run(object parameters)
        {
            return base.Run(parameters);
        }
    }
}
