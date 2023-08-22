using GSendService.Attributes;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class ServiceScheduleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
