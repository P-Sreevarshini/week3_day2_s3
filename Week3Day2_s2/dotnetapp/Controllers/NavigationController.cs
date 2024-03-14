using Microsoft.AspNetCore.Mvc;

namespace YourAppName.Controllers
{
    public class NavigationController : Controller
    {
        public IActionResult Home()
        {
            return View("Home");
        }

        public IActionResult Categories()
        {
            return View("Categories");
        }

        public IActionResult Services()
        {
            return View("Services");
        }

        public IActionResult About()
        {
            return View("About");
        }
    }
}
