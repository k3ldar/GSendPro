using System;
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

        private static readonly string[] _validMCodes = {
            "M600", "M601", "M602", "M605", "M620", "M621",
            "M622", "M623", "M630", "M630.1", "M631",
            "M631.1", "M631.2" };

        private static readonly Dictionary<string, decimal[]> _seeAlso = new()
        {
            { "M601", new decimal[] { 620, 623, 630, 631 } },
            { "M620", new decimal[] { 601, 621, 622, 623 } },
            { "M621", new decimal[] { 620, 622, 623 } },
            { "M622", new decimal[] { 620, 621, 623 } },
            { "M623", new decimal[] { 620, 621, 622 } },
            { "M630", new decimal[] { 601, 630.1m } },
            { "M630.1", new decimal[] { 630 } },
            { "M631", new decimal[] { 601, 631.1m, 631.2m } },
            { "M631.1", new decimal[] { 631, 631.2m } },
            { "M631.2", new decimal[] { 631, 631.1m } },
        };

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbMCodes))]
        [Route("/MCodes/Index")]
        public IActionResult Index()
        {
            return View(new MCodeIndex(GetModelData(), _validMCodes));
        }

        [HttpGet]
        [Route("/MCodes/{mCode}/")]
        public IActionResult MCode(string mCode)
        {
            if (!_validMCodes.Contains(mCode))
                return RedirectToAction(nameof(Index));

            System.Resources.ResourceManager resManager = GSend.Language.Resources.ResourceManager;
            string menuData = resManager.GetString($"WebMenu{mCode.ToUpper()}", GSend.Language.Resources.Culture);

            bool seeAlsoExists = _seeAlso.TryGetValue(mCode, out _);
            MCodeModel mCodeModel = new(GetModelData(), mCode, menuData,
                seeAlsoExists ? _seeAlso[mCode] : Array.Empty<decimal>());
            mCodeModel.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.BreadcrumbMCodes, "/MCodes/Index", false));
            mCodeModel.Breadcrumbs.Add(new BreadcrumbItem(mCode, $"/MCodes/{mCode}/", false));

            return View(mCode.Replace(".", "_"), mCodeModel);
        }
    }
}
