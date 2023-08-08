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

        IJobProfile JobProfileGet(long jobId);

        IReadOnlyList<IJobProfile> JobProfilesGet();

        long JobProfileAdd(string name, string description);

        void JobProfileUpdate(IJobProfile jobProfile);

        void JobProfileRemove(long jobId);

        ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile);

        IReadOnlyList<IToolProfile> ToolsGet();

        IToolProfile ToolGet(long toolProfileId);

        IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId);

        void JobExecutionUpdate(IJobExecution jobExecution);

        TimeSpan JobExecutionByTool(IToolProfile toolProfile);
    }
}
