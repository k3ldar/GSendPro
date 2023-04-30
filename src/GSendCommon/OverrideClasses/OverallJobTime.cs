using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Interfaces;

namespace GSendCommon.OverrideClasses
{
    internal class OverallJobTime : IGCodeOverride
    {
        public MachineType MachineType => MachineType.Unspecified;

        public int SortOrder => Int32.MinValue;

        public void Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            
        }

        public void Process(GrblAlarm alarm)
        {
            
        }

        public void Process(GrblError error)
        {
            
        }
    }
}
