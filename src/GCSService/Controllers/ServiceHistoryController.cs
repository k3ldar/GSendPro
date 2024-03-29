﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

using GSendService.Attributes;
using GSendService.Models;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class ServiceHistoryController : BaseController
    {
        public const string Name = "ServiceHistory";

        private readonly IGSendDataProvider _gSendDataProvider;

        public ServiceHistoryController(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        [Breadcrumb(nameof(GSend.Language.Resources.ServiceHistory))]
        public IActionResult Index()
        {
            List<NameIdModel> machineNames = _gSendDataProvider.MachinesGet().Select(m => new NameIdModel(m.Id, m.Name)).ToList();

            long machineId = -1;

            if (machineNames.Count > 0)
            {
                machineId = machineNames[0].Id;
            }

            List<MachineServiceModel> services = [.. _gSendDataProvider.ServicesGet(machineId)];

            ServiceHistoryModel model = new(GetModelData(), machineNames, services, machineId);

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeMachine(ServiceHistoryModel model)
        {
            List<NameIdModel> machineNames = _gSendDataProvider.MachinesGet().Select(m => new NameIdModel(m.Id, m.Name)).ToList();

            long machineId = model.MachineId;

            List<MachineServiceModel> services = [.. _gSendDataProvider.ServicesGet(machineId)];

            model = new ServiceHistoryModel(GetModelData(), machineNames, services, machineId);

            return View("/Views/ServiceHistory/Index.cshtml", model);
        }
    }
}
