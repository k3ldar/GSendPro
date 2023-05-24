using GSendDB.Tables;

using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Api
{
    public class SpindleHoursApi : BaseController
    {
        private readonly ISimpleDBOperations<MachineSpindleTimeDataRow> _spindleTimeTable;
        private readonly ISimpleDBOperations<ToolDatabaseDataRow> _toolDatabaseTable;

        public SpindleHoursApi(ISimpleDBOperations<MachineSpindleTimeDataRow> spindleTimeTable,
            ISimpleDBOperations<ToolDatabaseDataRow> toolDatabaseTable)
        {
            _spindleTimeTable = spindleTimeTable ?? throw new ArgumentNullException(nameof(spindleTimeTable));
            _toolDatabaseTable = toolDatabaseTable ?? throw new ArgumentNullException(nameof(toolDatabaseTable));
        }

        [HttpGet]
        [Route("/SpindleHoursApi/SpindleHoursGet/{machineId}/{fromDateTimeTicks}/")]
        public IActionResult SpindleHoursGet(long machineId, long fromDateTimeTicks)
        {
            DateTime fromDate = new DateTime(fromDateTimeTicks, DateTimeKind.Utc);
            List<SpindleHoursModel> Result = new();
            IReadOnlyList<MachineSpindleTimeDataRow> allSpindleTime = _spindleTimeTable.Select(m => m.MachineId.Equals(machineId) &&
                m.StartTime >= fromDate && m.FinishTime > DateTime.MinValue);

            foreach (MachineSpindleTimeDataRow spindleTime in allSpindleTime)
            {
                Result.Add(new SpindleHoursModel()
                {
                    MachineId = spindleTime.MachineId,
                    MaxRpm = spindleTime.MaxRpm,
                    StartDateTime = spindleTime.StartTime,
                    FinishDateTime = spindleTime.FinishTime,
                    ToolProfile = spindleTime.ToolProfileId,
                });
            }

            return GenerateJsonSuccessResponse(Result);
        }
    }
}
