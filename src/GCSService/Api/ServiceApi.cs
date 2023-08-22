using System;
using System.Collections.Generic;

using GSendDB.Tables;

using GSendShared;
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
        [ApiAuthorization]
        public IActionResult ServicesGet(long machineId)
        {
            List<MachineServiceModel> dates = new();
            IReadOnlyList<MachineServiceDataRow> services = _serviceTable.Select(m => m.MachineId.Equals(machineId));

            foreach (MachineServiceDataRow service in services)
            {
                dates.Add(new MachineServiceModel(service.Id, service.MachineId, service.ServiceDate, 
                    (ServiceType)service.ServiceType, service.SpindleHours, service.Items));
            }

            return GenerateJsonSuccessResponse(dates);
        }

        [HttpPost]
        [ApiAuthorization]
        public IActionResult ServiceAdd([FromBody] MachineServiceModel machineServiceModel)
        {
            MachineServiceDataRow serviceTableDataRow = new()
            {
                MachineId = machineServiceModel.MachineId,
                ServiceDate = machineServiceModel.ServiceDate,
                ServiceType = machineServiceModel.ServiceType,
                SpindleHours = machineServiceModel.SpindleHours,
            };

            machineServiceModel.ServiceItems.ForEach(serviceTableDataRow.Items.Add);

            _serviceTable.Insert(serviceTableDataRow);

            return GenerateJsonSuccessResponse();
        }
    }
}
