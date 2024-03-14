using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class EmployeeController : Controller
    {
        [Route("employee/home")]
        public IActionResult Home()
        {
            return View("Home");
        }

        [Route("employee/details")]
        public IActionResult Details()
        {
            return View("Details");
        }

        [Route("employee/departments")]
        public IActionResult Departments()
        {
            return View("Departments");
        }

        [Route("employee/attendance")]
        public IActionResult Attendance()
        {
            return View("Attendance");
        }
    }
}
