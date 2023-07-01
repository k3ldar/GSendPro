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
            return View(new  BaseModel(GetModelData()));
        }

        public IActionResult Editor()
        {
            return View(new BaseModel(GetModelData()));
        }

        public IActionResult Sender()
        {
            return View(new BaseModel(GetModelData()));
        }
    }
}