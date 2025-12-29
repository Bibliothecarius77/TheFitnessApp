using Microsoft.AspNetCore.Mvc;

namespace TheFitnessApp_1.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return RedirectToAction("Welcome", "Home");
        }

        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string password)
        {
            return RedirectToAction("Welcome", "Home");
        }
    }
}
