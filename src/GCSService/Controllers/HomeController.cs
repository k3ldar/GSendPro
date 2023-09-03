using System;
using System.IO;

using GSendService.Attributes;
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

        [LicenseValidation]
        [HttpGet]
        public IActionResult Index()
        {
            return View(CreateIndexModel());
        }

        [HttpGet]
        public IActionResult ViewLicense()
        {
            return View(GetLicenseModel());
        }

        [HttpPost]
        public IActionResult ViewLicense(AddLicenseModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (String.IsNullOrEmpty(model.NewLicense))
                ModelState.AddModelError(nameof(model.NewLicense), GSend.Language.Resources.LicenseInvalidEmpty);

            if (ModelState.IsValid)
            {
                ILicense license = _licenseFactory.LoadLicense(model.NewLicense);

                if (!license.IsValid)
                {
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.InvalidLicense);
                }
                else
                {
                    _licenseFactory.SetActiveLicense(license);
                    System.IO.File.WriteAllText(Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "lic.dat"), model.NewLicense);
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction("ViewLicense");

            return View(GetLicenseModel());
        }


        private AddLicenseModel GetLicenseModel()
        {
            ILicense activeLicense = _licenseFactory.GetActiveLicense();

            return new AddLicenseModel(GetModelData(), activeLicense.RegisteredUser, activeLicense.Expires, activeLicense.IsValid);
        }

        private IndexViewModel CreateIndexModel()
        {
            return new IndexViewModel(GetModelData(), _gSendDataProvider.MachinesGet());
        }
    }
}
