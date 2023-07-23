using System.Collections.Generic;
using System.Linq;

using gsend.pro.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class MCodesController : BaseController
    {
        public const string MCodes = "MCodes";

        private static readonly string[] _validMCodes = { "M600", "M601", "M605", "M620", "M621", "M622", "M623", "M630", "M650" };
        private static readonly Dictionary<string, int[]> _seeAlso = new Dictionary<string, int[]>()
        {
            { "M601", new int[] { 620, 623, 630 } },
            { "M620", new int[] { 601, 621, 622, 623 } },
            { "M621", new int[] { 620, 622, 623 } },
            { "M622", new int[] { 620, 621, 623 } },
            { "M623", new int[] { 620, 621, 622 } },
            { "M630", new int[] { 601 } }
        };

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

            System.Resources.ResourceManager resManager = GSend.Language.Resources.ResourceManager;
            string menuData = resManager.GetString($"WebMenu{mCode.ToUpper()}", GSend.Language.Resources.Culture);

            MCodeModel mCodeModel = new MCodeModel(GetModelData(), mCode, menuData,
                _seeAlso.ContainsKey(mCode) ? _seeAlso[mCode] : new int[] { });
            mCodeModel.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.BreadcrumbMCodes, "/MCodes/Index", false));
            mCodeModel.Breadcrumbs.Add(new BreadcrumbItem(mCode, $"/MCodes/{mCode}/", true));

            return View(mCode, mCodeModel);
        }
    }
}
