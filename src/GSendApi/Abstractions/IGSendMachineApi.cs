using GSendShared;
using GSendShared.Models;

namespace GSendApi
{
    public interface IGSendMachineApi
    {
        void MachineAdd(IMachine machine);

        bool MachineNameExists(string name);

        void MachineServiceAdd(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours);

        List<MachineServiceModel> MachineServices(long machineId);

        List<IMachine> MachinesGet();

        void MachineUpdate(IMachine machine);
    }
}
