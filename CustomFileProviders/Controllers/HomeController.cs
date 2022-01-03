using Microsoft.AspNetCore.Mvc;

namespace CustomFileProviders.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View("deneme");
        }
    }
}
