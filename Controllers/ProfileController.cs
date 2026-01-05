using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för användarens profil
    public class ProfileController : Controller
    {
        private readonly IRepository<UserProfile> _profileRepo;
        private readonly IRepository<WorkoutGoal> _goalRepo;
        private readonly IRepository<Statistics> _statsRepo;

        // Konstruktor med Dependency Injection
        public ProfileController(
            IRepository<UserProfile> profileRepo,
            IRepository<WorkoutGoal> goalRepo,
            IRepository<Statistics> statsRepo)
        {
            _profileRepo = profileRepo;
            _goalRepo = goalRepo;
            _statsRepo = statsRepo;
        }

        // GET: /Profile
        // Visar användarens profil
        public async Task<IActionResult> Index(Guid id)
        {
            var profile = await _profileRepo.GetByIDAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        // GET: /Profile/Edit/{id}
        // Visa formulär för att redigera profil
        public async Task<IActionResult> Edit(Guid id)
        {
            var profile = await _profileRepo.GetByIDAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        // POST: /Profile/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserProfile profile)
        {
            if (id != profile.ProfileID) return BadRequest();

            if (ModelState.IsValid)
            {
                await _profileRepo.UpdateAsync(profile);
                return RedirectToAction(nameof(Index), new { id = profile.ProfileID });
            }

            return View(profile);
        }

        // GET: /Profile/Goals/{id}
        // Visar användarens träningsmål
        public async Task<IActionResult> Goals(Guid userId)
        {
            var allGoals = await _goalRepo.GetAsync();
            var userGoals = allGoals.Where(g => g.UserID == userId).ToList();

            return View(userGoals);
        }

        // GET: /Profile/Statistics/{id}
        // Visar användarens statistik
        public async Task<IActionResult> Statistics(Guid userId)
        {
            var allStats = await _statsRepo.GetAsync();
            var userStats = allStats.Where(s => s.UserID == userId).ToList();

            return View(userStats);
        }
    }
}
