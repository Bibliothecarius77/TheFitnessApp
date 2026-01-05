using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFitnessApp.Data;
using TheFitnessApp.Models;
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




/*using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<UserProfile> _profileRepo;
        private readonly IRepository<WorkoutGoal> _goalRepo;
        private readonly IRepository<WorkoutSession> _sessionRepo;

        public HomeController(
            IRepository<UserProfile> profileRepo,
            IRepository<WorkoutGoal> goalRepo,
            IRepository<WorkoutSession> sessionRepo)
        {
            _profileRepo = profileRepo;
            _goalRepo = goalRepo;
            _sessionRepo = sessionRepo;
        }

        // GET: /
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Welcome
        public async Task<IActionResult> Welcome()
        {
            // Hämta inloggad användares GUID från ClaimsPrincipal
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Index"); // Ej inloggad
            }

            // Hämta användarprofil
            var profile = await _profileRepo.GetAsync();
            var userProfile = profile.FirstOrDefault(p => p.UserID == userId);

            // Hämta statistik för användaren
            var allGoals = await _goalRepo.GetAsync();
            var userGoals = allGoals.Where(g => g.UserID == userId);

            var allSessions = await _sessionRepo.GetAsync();
            var userSessions = allSessions.Where(s => s.UserID == userId);

            var model = new DashboardViewModel
            {
                UserName = userProfile?.FirstName ?? "User",
                TotalSessions = userSessions.Count(),
                ActiveGoals = userGoals.Count(g => !g.IsCompleted),
                TotalMinutes = userSessions.Sum(s => s.DurationMinutes)
            };

            return View(model);
        }
    }

    // Enkel Dashboard ViewModel
    public class DashboardViewModel
    {
        public string UserName { get; set; } = "User";
        public int TotalSessions { get; set; } = 0;
        public int ActiveGoals { get; set; } = 0;
        public int TotalMinutes { get; set; } = 0;
    }
}
*/