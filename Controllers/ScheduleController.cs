/*
 * ZenMove - The Ultimate Fitness App
 *
 * IT-påbyggnad Utvecklare, Fullstack .NET (Lexicon)
 * Kursvecka 13-16 (vv.2551-2602)
 *
 * Grupp Blå:
 *   Arsalan Habib
 *   Jacob Damm
 *   Liridona Demaj
 *   Victoria Rådberg
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
/*
    public class ScheduleController : Controller
    {
        private readonly IRepository<WorkoutSchedule> _scheduleRepo;
        private readonly IRepository<WorkoutSession> _sessionRepo;

        public ScheduleController(
            IRepository<WorkoutSchedule> scheduleRepo,
            IRepository<WorkoutSession> sessionRepo)
        {
            _scheduleRepo = scheduleRepo;
            _sessionRepo = sessionRepo;
        }

        // GET: /Schedule
        // Visa alla scheman
        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleRepo.GetAsync();
            return View(schedules);
        }

        // GET: /Schedule/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var schedule = await _scheduleRepo.GetByIDAsync(id);
            if (schedule == null) return NotFound();

            var sessions = (await _sessionRepo.GetAsync())
                           .Where(s => s.ScheduleID == id)
                           .ToList();
            schedule.Sessions = sessions;

            return View(schedule);
        }

        // GET: /Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                // Primary Keys are automatically handled by the database
                //schedule.ScheduleID = Guid.NewGuid();
                await _scheduleRepo.InsertAsync(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: /Schedule/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var schedule = await _scheduleRepo.GetByIDAsync(id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // POST: /Schedule/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkoutSchedule schedule)
        {
            if (id != schedule.ScheduleID) return BadRequest();

            if (ModelState.IsValid)
            {
                await _scheduleRepo.UpdateAsync(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: /Schedule/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var schedule = await _scheduleRepo.GetByIDAsync(id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // POST: /Schedule/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _scheduleRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Schedule/Upcoming/{id}
        public async Task<IActionResult> Upcoming(Guid id)
        {
            var sessions = (await _sessionRepo.GetAsync())
                           .Where(s => s.ScheduleID == id && s.StartTime >= DateTime.Now)
                           .ToList();
            return View(sessions);
        }

        // GET: /Schedule/History/{id}
        public async Task<IActionResult> History(Guid id)
        {
            var sessions = (await _sessionRepo.GetAsync())
                           .Where(s => s.ScheduleID == id && s.StartTime < DateTime.Now)
                           .ToList();
            return View(sessions);
        }
    }
*/

    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly UnifiedContext _context;
        private readonly IUserAggregateService _users;
        private readonly UserManager<AppUser> _userManager;

        public ScheduleController(UnifiedContext context, IUserAggregateService users, UserManager<AppUser> userManager)
        {
            _context = context;
            _users = users;
            _userManager = userManager;
        }

        // GET: /Schedule
        // Visa användarens enda schema (man kan bara ha ett, men schemat kan sedan ha flers Sessions)
        public async Task<IActionResult> Index()
        {
            //var userId = Guid.Parse(_userManager.GetUserId(User)!);
            //var user = await _users.GetAsync(userId);
            var user = await GetCurrentUserAsync();

            if (user?.Schedule == null)
                return NotFound();

            return View(user.Schedule);
        }

        // GET: /Schedule/Details
        public async Task<IActionResult> Details()
        {
            var user = await GetCurrentUserAsync();

            if (user?.Schedule == null)
                return NotFound();

            return View(user.Schedule);
        }

        // GET: /Schedule/Create
        public IActionResult Create()
        {
            return View(new WorkoutSessionInputModel());
        }

        // POST: /Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutSession session)
        {
            if (!ModelState.IsValid)
                return View(session);

            var user = await GetCurrentUserAsync();

            if (user?.Schedule == null)
                return NotFound();

            // Enforce aggregate ownership
            user.Schedule.Sessions.Add(session);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        // GET: /Schedule/EditSchedule
        public async Task<IActionResult> EditSchedule()
        {
            var user = await GetCurrentUserAsync();

            if (user?.Schedule == null)
                return NotFound();

            return View(user.Schedule);
        }

        // POST: /Schedule/EditSchedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(WorkoutSchedule updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var user = await GetCurrentUserAsync();
            var schedule = user?.Schedule;

            if (schedule == null)
                return NotFound();

            // Update non-required properties
            schedule.StartDate = updated.StartDate;
            schedule.EndDate = updated.EndDate;
            schedule.Notes = updated.Notes;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        // GET: /Schedule/Edit/{id}
        public async Task<IActionResult> EditSession(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var session = user?.Schedule?.Sessions.SingleOrDefault(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            return View(session);
        }

        // POST: /Schedule/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSession(Guid id, WorkoutSession updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var user = await GetCurrentUserAsync();
            var session = user?.Schedule?.Sessions.SingleOrDefault(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            // Update non-required properties
            session.StartTime = updated.StartTime;
            session.EndTime = updated.EndTime;
            session.TotalCalories = updated.TotalCalories;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        // GET: /Schedule/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var session = user?.Schedule?.Sessions.SingleOrDefault(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            return View(session);
        }

        // POST: /Schedule/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var session = user?.Schedule?.Sessions.SingleOrDefault(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            user!.Schedule!.Sessions.Remove(session);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        // GET: /Schedule/Upcoming
        public async Task<IActionResult> Upcoming()
        {
            var user = await GetCurrentUserAsync();
            var sessions = user?.Schedule?.Sessions
                .Where(s => s.StartTime >= DateTime.Now)
                .OrderBy(s => s.StartTime)
                .ToList();

            return View(sessions);
        }

        // GET: /Schedule/History
        public async Task<IActionResult> History()
        {
            var user = await GetCurrentUserAsync();
            var sessions = user?.Schedule?.Sessions
                .Where(s => s.EndTime < DateTime.Now)
                .OrderByDescending(s => s.StartTime)
                .ToList();

            return View(sessions);
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            return await _users.GetAsync(userId)
                ?? throw new InvalidOperationException("User not found.");
        }
    }
}
