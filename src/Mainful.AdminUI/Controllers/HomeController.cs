using Microsoft.AspNetCore.Mvc;
using Mainful.AdminUI.Shared.Helpers;

namespace Mainful.AdminUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Test(string key = "ConnectionStrings:DefaultConnection")
        {
            return Json(ConfigHelper.Get(key));
        }
    }
}
