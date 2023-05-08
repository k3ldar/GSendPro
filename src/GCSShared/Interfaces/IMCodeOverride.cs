using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Interfaces
{
    public interface IMCodeOverride
    {
        bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken);
    }
}
