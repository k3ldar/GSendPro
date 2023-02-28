using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Models;

namespace GSendShared
{
    public interface IMachineProvider
    {
        bool MachineAdd(IMachine machine);

        IReadOnlyList<IMachine> MachinesGet();

        IMachine MachineGet(long machineId);

        void MachineRemove(long machineId);

        void MachineUpdate(IMachine machine);
    }
}
