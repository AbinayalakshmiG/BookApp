using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            return View();  // will look for Views/Home/Index.cshtml
        }

        // GET: /About
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
