using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;

namespace TheFitnessApp.Controllers
{
    // Controller för användarens profil
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;  // Databaskontext (ersätts senare av Repository)

        // Konstruktor med Dependency Injection,// ASP.NET Core skickar in ApplicationDbContext automatiskt.
        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Profile
        // Visar användarens profil
        public IActionResult Index()
        {
            // TODO: Hämta användarens profilinformation via Repository
            return View();
        }

        // GET: /Profile/Statistics
        // Visar användarens träningsstatistik
        public IActionResult Statistics()
        {
            // TODO: Hämta användarens statistik via Repository
            return View();
        }

        // GET: /Profile/Goals
        // Visar användarens träningsmål
        public IActionResult Goals()
        {
            // TODO: Hämta användarens träningsmål via Repository
            return View();
        }
    }
}
