using System;
using System.Collections.Generic;
using System.Linq;

using GSendDB.Tables;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Api
{
    public class SpindleHoursApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;

        public SpindleHoursApi(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        [HttpGet]
        [Route("/SpindleHoursApi/SpindleHoursGet/{machineId}/{fromDateTimeTicks}/")]
        [ApiAuthorization]
        public IActionResult SpindleHoursGet(long machineId, long fromDateTimeTicks)
        {
            DateTime fromDate = new(fromDateTimeTicks, DateTimeKind.Utc);
            List<SpindleHoursModel> Result = [];
            IReadOnlyList<ISpindleTime> allSpindleTime = _gSendDataProvider.SpindleTimeGet(machineId).Where(m =>
                m.StartTime >= fromDate && m.FinishTime > DateTime.MinValue).ToList();

            foreach (ISpindleTime spindleTime in allSpindleTime)
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
