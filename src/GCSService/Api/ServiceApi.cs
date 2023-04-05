using GSendDB.Tables;

using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Api
{
    public class ServiceApi : BaseController
    {
        private readonly ISimpleDBOperations<MachineServiceDataRow> _serviceTable;

        public ServiceApi(ISimpleDBOperations<MachineServiceDataRow> serviceTable)
        {
            _serviceTable = serviceTable ?? throw new ArgumentNullException(nameof(serviceTable));
        }

        [HttpGet]
        public IActionResult ServicesGet(long machineId)
        {
            List<DateTime> dates = new List<DateTime>();
            IReadOnlyList<MachineServiceDataRow> services = _serviceTable.Select(m => m.MachineId.Equals(machineId));

            foreach (MachineServiceDataRow service in services)
            {
                dates.Add(service.ServiceDate);
            }

            return GenerateJsonSuccessResponse(dates);
        }

        [HttpPost]
        public IActionResult ServiceAdd([FromBody] MachineServiceModel machineServiceModel)
        {
            _serviceTable.Insert(new MachineServiceDataRow { MachineId = machineServiceModel.MachineId, ServiceDate = machineServiceModel.ServiceDate });
            return GenerateJsonSuccessResponse();
        }
    }
}
