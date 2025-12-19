using Microsoft.AspNetCore.Mvc;

namespace TheFitnessApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            // TODO: backend-teamet implementerar validering mot databasen
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string FirstName, string LastName, string Email, string Password)
        {
            // TODO: backend-teamet implementerar skapande av ny användare
            return RedirectToAction("Login");
        }
    }
}
