using GSendShared.Models;

namespace GSendShared
{
    public interface IGSendDataProvider
    {
        bool MachineAdd(IMachine machine);

        IReadOnlyList<IMachine> MachinesGet();

        IMachine MachineGet(long machineId);

        void MachineRemove(long machineId);

        void MachineUpdate(IMachine machine);

        long SpindleTimeCreate(long machineId, int maxSpindleSpeed, long toolProfileId);

        void SpindleTimeFinish(long spindleTimeId);

        IReadOnlyList<ISpindleTime> SpindleTimeGet(long machineId);

        IJobProfile JobProfileGet(long jobId);

        IReadOnlyList<IJobProfile> JobProfilesGet();

        long JobProfileAdd(string name, string description);

        void JobProfileUpdate(IJobProfile jobProfile);

        void JobProfileRemove(long jobId);

        ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile);

        IReadOnlyList<IToolProfile> ToolsGet();

        IToolProfile ToolGet(long toolProfileId);

        void ToolAdd(IToolProfile toolProfile);

        void ToolUpdate(IToolProfile toolProfile);

        void ToolResetUsage(IToolProfile toolProfile);

        IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId);

        void JobExecutionUpdate(IJobExecution jobExecution);

        TimeSpan JobExecutionByTool(IToolProfile toolProfile);

        IEnumerable<JobExecutionStatistics> JobExecutionModelsGetByTool(IToolProfile toolProfile, bool sinceLastUsed);

        void ServiceAdd(MachineServiceModel service);

        IReadOnlyList<MachineServiceModel> ServicesGet(long machineId);

        List<ServiceItemModel> ServiceItemsGet(MachineType machineType);
    }
}