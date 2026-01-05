using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // =========================
    // Controller som hanterar användarkonton: inloggning, registrering och utloggning
    // =========================
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;       // Används för att skapa och hantera användare
        private readonly SignInManager<AppUser> _signInManager;   // Används för att logga in/ut användare

        // Konstruktor – Dependency Injection skickar in UserManager och SignInManager automatiskt
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // =========================
        // LOGIN
        // =========================

        // GET: /Account/Login
        // Visar inloggningssidan
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        // Tar emot data från inloggningsformuläret
        [HttpPost]
        [ValidateAntiForgeryToken] // Skyddar mot Cross-Site Request Forgery
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Om data inte validerar, visa samma formulär igen
            if (!ModelState.IsValid)
                return View(model);

            // Försök logga in användaren med e-post och lösenord
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: model.RememberMe, // Om true, användaren håller sig inloggad
                lockoutOnFailure: false         // Om true, lås kontot efter för många misslyckade försök
            );

            if (result.Succeeded)
            {
                // Om inloggning lyckas, gå till dashboard/Welcome
                return RedirectToAction("Welcome", "Home");
            }

            // Om inloggning misslyckas, visa felmeddelande
            ModelState.AddModelError("", "Felaktig inloggning. Kontrollera e-post och lösenord.");
            return View(model);
        }

        // =========================
        // REGISTER
        // =========================

        // GET: /Account/Register
        // Visar registreringsformuläret
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        // Tar emot data från registreringsformuläret
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Om data inte validerar, visa samma formulär igen
            if (!ModelState.IsValid)
                return View(model);

            // Skapa en ny AppUser med e-post som användarnamn
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // Skapa användaren i databasen med lösenord
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Tilldela rollen "User" till ny registrerad användare
                await _userManager.AddToRoleAsync(user, "User");

                // Logga in användaren direkt efter registrering
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Skicka användaren till Welcome/dashboard
                return RedirectToAction("Welcome", "Home");
            }

            // Om något gick fel vid registrering, visa felmeddelanden i formuläret
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // =========================
        // LOGOUT
        // =========================

        // POST: /Account/Logout
        // Loggar ut användaren
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Loggar ut användaren
            return RedirectToAction("Index", "Home"); // Skicka till startsidan
        }
    }

    // =========================
    // ViewModels – datamodeller för formulären
    // =========================

    // Model för inloggningsformuläret
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string Email { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // Om användaren vill hålla sig inloggad
        public bool RememberMe { get; set; } = false;
    }

    // Model för registreringsformuläret
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string Email { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
