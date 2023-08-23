using System;

using GSendService.Attributes;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using SimpleDB;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class ServiceScheduleController : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;

        public ServiceScheduleController(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
