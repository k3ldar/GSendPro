using GSendShared;
using GSendShared.Models;

namespace GSendApi
{
    public interface IGSendApiWrapper
    {
        Uri ServerAddress { get; }

        List<SpindleHoursModel> GetSpindleTime(long machineId, DateTime fromDate);
        bool IsLicenseValid();
        List<IJobProfile> JobProfilesGet();
        void MachineAdd(IMachine machine);
        bool MachineNameExists(string name);
        void MachineServiceAdd(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours);
        List<MachineServiceModel> MachineServices(long machineId);
        List<IMachine> MachinesGet();
        void MachineUpdate(IMachine machine);
        bool SubprogramDelete(string name);
        bool SubprogramExists(string name);
        List<ISubProgram> SubprogramGet();
        ISubProgram SubprogramGet(string name);
        bool SubprogramUpdate(ISubProgram subProgram);
        List<IToolProfile> ToolProfilesGet();
    }
}