using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Models; // ändrat till deras namespace

namespace TheFitnessApp.Controllers // ändrat till deras namespace
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
