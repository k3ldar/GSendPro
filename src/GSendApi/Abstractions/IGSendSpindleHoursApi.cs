using GSendShared.Models;

namespace GSendApi
{
    public interface IGSendSpindleHoursApi
    {
        List<SpindleHoursModel> GetSpindleTime(long machineId, DateTime fromDate);
    }
}
