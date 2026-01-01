using Microsoft.AspNetCore.Mvc;

namespace TheFitnessApp_1.Controllers
{
    public class AccountController : Controller
    {
        // Visar login-sidan
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Hanterar login-formuläret
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return RedirectToAction("Welcome", "Home");
        }

        // Visar register-sidan
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Hanterar register-formuläret
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string password)
        {
            return RedirectToAction("Welcome", "Home");
        }
    }
}
