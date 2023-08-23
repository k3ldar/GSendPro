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
    public class ServiceApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;

        public ServiceApi(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult ServicesGet(long machineId)
        {
            return GenerateJsonSuccessResponse(_gSendDataProvider.ServicesGet(machineId).ToList());
        }

        [HttpPost]
        [ApiAuthorization]
        public IActionResult ServiceAdd([FromBody] MachineServiceModel machineServiceModel)
        {
            _gSendDataProvider.ServiceAdd(machineServiceModel);

            return GenerateJsonSuccessResponse();
        }
    }
}
