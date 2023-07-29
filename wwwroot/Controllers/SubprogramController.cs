using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class SubprogramController : BaseController
    {
        public const string Name = "Subprogram";

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbSubprograms))]
        public IActionResult Index()
        {
            return View(new BaseModel(GetModelData()));
        }
    }
}
