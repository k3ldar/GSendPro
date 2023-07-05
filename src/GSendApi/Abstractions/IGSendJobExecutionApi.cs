using GSendShared;

namespace GSendApi
{
    public interface IGSendJobExecutionApi
    {
        IJobExecution JobExecutionCreate(long machineId, long toolId, long jobProfileId);
    }
}
