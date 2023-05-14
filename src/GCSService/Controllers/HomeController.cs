using GSendService.Models;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;

        public HomeController(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(CreateIndexModel());
        }

        [HttpGet]
        [Route("/ViewMachine/{machineId}/")]
        public IActionResult ViewMachine(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
                RedirectToAction(nameof(Index));

            return View(CreateMachineModel(machine));
        }

        private IndexViewModel CreateIndexModel()
        {
            return new IndexViewModel(GetModelData(), _gSendDataProvider.MachinesGet());
        }

        private MachineViewModel CreateMachineModel(IMachine machine)
        {
            return new MachineViewModel(GetModelData(), machine.Name)
            {

            };
        }
    }
}
