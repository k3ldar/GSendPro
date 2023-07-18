using System.Linq;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class MCodesController : BaseController
    {
        public const string MCodes = "MCodes";

        private static readonly string[] _validMCodes = { "M600", "M605", "M620", "M621", "M622", "M650" };

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbMCodes))]
        [Route("/MCodes/Index")]
        public IActionResult Index()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Route("/MCodes/{mCode}/")]
        public IActionResult MCode(string mCode)
        {
            if (!_validMCodes.Contains(mCode))
                return RedirectToAction(nameof(Index));

            BaseModel baseModel = new BaseModel(GetModelData());
            baseModel.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.BreadcrumbMCodes, "/MCodes/Index", false));
            baseModel.Breadcrumbs.Add(new BreadcrumbItem(mCode, $"/MCodes/{mCode}/", false));

            return View(mCode, baseModel);
        }
    }
}
