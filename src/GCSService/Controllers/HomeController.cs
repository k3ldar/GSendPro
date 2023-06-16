using GSendService.Models;

using GSendShared;
using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    public class HomeController : BaseController
    {
        public const string Name = "Home";
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly ILicenseFactory _licenseFactory;

        public HomeController(IGSendDataProvider gSendDataProvider, ILicenseFactory licenseFactory)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _licenseFactory = licenseFactory ?? throw new ArgumentNullException(nameof(licenseFactory));
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsLicenseValid())
                return RedirectToAction(nameof(AddLicense));

            return View(CreateIndexModel());
        }

        [HttpGet]
        [Route("/ViewMachine/{machineId}/")]
        public IActionResult ViewMachine(long machineId)
        {
            if (!IsLicenseValid())
                return RedirectToAction(nameof(AddLicense));

            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
                RedirectToAction(nameof(Index));

            return View(CreateMachineModel(machine));
        }

        [HttpGet]
        public IActionResult AddLicense()
        {
            return View(new AddLicenseModel(GetModelData()));
        }

        [HttpPost]
        public IActionResult AddLicense(AddLicenseModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (String.IsNullOrEmpty(model.License))
                ModelState.AddModelError(nameof(model.License), GSend.Language.Resources.LicenseInvalidEmpty);

            if (ModelState.IsValid)
            {

            }

            return RedirectToAction("ViewLicense");
        }


        private bool IsLicenseValid()
        {
            ILicense license = _licenseFactory.GetActiveLicense();

            return license != null && license.IsValid;
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
