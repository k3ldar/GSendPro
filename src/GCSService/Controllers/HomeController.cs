using GSendService.Models;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMachineProvider _machineProvider;

        public HomeController(IMachineProvider machineProvider)
        {
            _machineProvider = machineProvider ?? throw new ArgumentNullException(nameof(machineProvider));
        }

        public IActionResult Index()
        {
            BaseModelData model = GetModelData();
            return View(CreateIndexModel());
        }

        private IndexModel CreateIndexModel()
        {
            return new IndexModel(GetModelData(), _machineProvider.MachinesGet());
        }
    }
}
