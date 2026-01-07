using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    /*
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
    */

    // Controller för användarens profil
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UnifiedContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UnifiedContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Profile
        // Visar användarens profil
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var user = await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Goals)
                .Include(u => u.Statistics)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user?.Profile == null)
                return NotFound();

            return View(user.Profile);
        }

        // GET: /Profile/Edit
        // Visa formulär för att redigera profil
        public async Task<IActionResult> Edit()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var profile = await _context.UserProfiles.SingleOrDefaultAsync(p => p.UserID == userId);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        // POST: /Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfile profile)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            if (profile.UserID != userId)
                return Forbid();

            if (ModelState.IsValid)
            {
                _context.UserProfiles.Update(profile);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(profile);
        }

        // GET: /Profile/Goals
        // Visar användarens träningsmål
        public async Task<IActionResult> Goals()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var goals = await _context.Goals
                .Where(g => g.UserID == userId)
                .ToListAsync();

            return View(goals);
        }

        // GET: /Profile/Statistics
        // Visar användarens statistik
        public async Task<IActionResult> Statistics()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var stats = await _context.Statistics
                .Where(s => s.UserID == userId)
                .ToListAsync();

            return View(stats);
        }
    }
}
