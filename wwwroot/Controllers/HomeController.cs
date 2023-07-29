using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace gsend.pro.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbEditor))]
        public IActionResult Editor()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbSender))]
        public IActionResult Sender()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbVariables))]
        public IActionResult Variables()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbJobProfile))]
        public IActionResult JobProfile()
        {
            return View(new BaseModel(GetModelData()));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.BreadcrumbToolLife))]
        public IActionResult ToolLife()
        {
            return View(new BaseModel(GetModelData()));
        }
    }
}