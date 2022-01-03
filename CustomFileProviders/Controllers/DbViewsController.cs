using Microsoft.AspNetCore.Mvc;

namespace CustomFileProviders.Controllers
{
    public class DbViewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
