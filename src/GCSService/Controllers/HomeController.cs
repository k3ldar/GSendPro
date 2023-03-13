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

        [HttpGet]
        public IActionResult Index()
        {
            return View(CreateIndexModel());
        }

        [HttpGet]
        [Route("/ViewMachine/{machineId}/")]
        public IActionResult ViewMachine(long machineId)
        {
            IMachine machine = _machineProvider.MachineGet(machineId);

            if (machine == null)
                RedirectToAction(nameof(Index));

            return View(CreateMachineModel(machine));
        }

        private IndexViewModel CreateIndexModel()
        {
            return new IndexViewModel(GetModelData(), _machineProvider.MachinesGet());
        }

        private MachineViewModel CreateMachineModel(IMachine machine)
        {
            return new MachineViewModel(GetModelData(), machine.Name)
            {

            };
        }
    }
}
