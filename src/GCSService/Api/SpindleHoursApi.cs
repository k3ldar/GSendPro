using GSendDB.Tables;

using GSendService.Models;

using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Api
{
    public class SpindleHoursApi : BaseController
    {
        private readonly ISimpleDBOperations<MachineSpindleTimeDataRow> _spindleTimeTable;

        public SpindleHoursApi(ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable)
        {
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(spindleTimeTable));
        }

        [HttpGet]
        [Route("/SpindleHoursApi/SpindleHoursGet/{machineId}/{fromDateTimeTicks}/")]
        public IActionResult SpindleHoursGet(long machineId, long fromDateTimeTicks)
        {
            DateTime fromDate = new DateTime(fromDateTimeTicks, DateTimeKind.Utc);
            List<SpindleHoursModel> Result = new();
            IReadOnlyList<MachineSpindleTimeDataRow> allSpindleTime = _spindleTimeTable.Select(m => m.MachineId.Equals(machineId) && m.StartTime >= fromDate);

            foreach (MachineSpindleTimeDataRow spindleTime in allSpindleTime)
            {
                Result.Add(new SpindleHoursModel()
                { 
                    MachineId = spindleTime.MachineId,
                    MaxRpm = spindleTime.MaxRpm,
                    StartDateTime = spindleTime.StartTime,
                    TotalTime = spindleTime.FinishTime - spindleTime.StartTime
                });
            }

            return GenerateJsonSuccessResponse(Result);
        }
    }
}
