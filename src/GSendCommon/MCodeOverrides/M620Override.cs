using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;

namespace GSendCommon.MCodeOverrides
{
    
    internal class M620Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {

            return false;
        }
    }
}
