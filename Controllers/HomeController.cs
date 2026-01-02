using Microsoft.AspNetCore.Mvc;
using TheFitnessApp_1.Models;

namespace TheFitnessApp_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            var model = new DashboardViewModel
            {
                UserName = "Anna",       // Backend byter ut detta senare
                TotalSessions = 0,
                ActiveGoals = 0,
                TotalMinutes = 0
            };

            return View(model);
        }
    }
}
