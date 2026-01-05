using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
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
                schedule.ScheduleID = Guid.NewGuid();
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
}
