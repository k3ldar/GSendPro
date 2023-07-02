using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View(new BaseModel(GetModelData()));
        }

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbEditor))]
        public IActionResult Editor()
        {
            return View(new BaseModel(GetModelData()));
        }

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbSender))]
        public IActionResult Sender()
        {
            return View(new BaseModel(GetModelData()));
        }

        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbVariables))]
        public IActionResult Variables()
        {
            return View(new BaseModel(GetModelData()));
        }
    }
}