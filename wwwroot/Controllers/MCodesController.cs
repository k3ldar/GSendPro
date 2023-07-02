using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class MCodesController : BaseController
    {
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbMCodes))]
        public IActionResult Index()
        {
            return View(new BaseModel(GetModelData()));
        }
    }
}
