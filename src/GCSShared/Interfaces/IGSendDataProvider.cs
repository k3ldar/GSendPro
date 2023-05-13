namespace GSendShared
{
    public interface IGSendDataProvider
    {
        bool MachineAdd(IMachine machine);

        IReadOnlyList<IMachine> MachinesGet();

        IMachine MachineGet(long machineId);

        void MachineRemove(long machineId);

        void MachineUpdate(IMachine machine);

        long SpindleTimeCreate(long machineId, int maxSpindleSpeed);

        void SpindleTimeFinish(long spindleTimeId);

        IJobProfile JobProfileGet(long jobId);

        IReadOnlyList<IJobProfile> JobProfilesGet();

        long JobProfileAdd(string name, string description);

        void JobProfileUpdate(IJobProfile jobProfile);

        ulong JobProfileGetNextSerialNumber(IJobProfile jobProfile);
    }
}
