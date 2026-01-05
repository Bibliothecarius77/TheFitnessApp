using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för träningspass (WorkoutSession)
    public class WorkoutController : Controller
    {
        private readonly IRepository<WorkoutSession> _workoutRepo;
        private readonly IRepository<WorkoutSchedule> _scheduleRepo; // 🎯 Repository för scheman

        public WorkoutController(
            IRepository<WorkoutSession> workoutRepo,
            IRepository<WorkoutSchedule> scheduleRepo) // 🎯 Dependency Injection
        {
            _workoutRepo = workoutRepo;
            _scheduleRepo = scheduleRepo;
        }

        // GET: /Workout
        public async Task<IActionResult> Index()
        {
            var sessions = await _workoutRepo.GetAsync();
            return View(sessions);
        }

        // GET: /Workout/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var session = await _workoutRepo.GetByIDAsync(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // GET: /Workout/Create
        public async Task<IActionResult> Create(Guid? scheduleId)
        {
            if (scheduleId == null) return BadRequest();

            // Hämta schemat från repository
            var schedule = await _scheduleRepo.GetByIDAsync(scheduleId.Value);
            if (schedule == null) return NotFound();

            // Skicka start/slut till ViewBag för vyn (null-säker)
            ViewBag.ScheduleStart = schedule?.StartDate ?? DateTime.Now;
            ViewBag.ScheduleEnd = schedule?.EndDate ?? DateTime.Now.AddHours(1);

            var session = new WorkoutSession
            {
                SessionID = Guid.NewGuid(),
                ScheduleID = scheduleId.Value,
                Schedule = schedule, //  Required property måste sättas
                StartTime = schedule.StartDate,
                EndTime = schedule.StartDate.AddHours(1) // default 1 timme
            };

            return View(session);
        }

        // POST: /Workout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutSession workoutSession)
        {
            if (ModelState.IsValid)
            {
                workoutSession.SessionID = Guid.NewGuid();
                await _workoutRepo.InsertAsync(workoutSession);
                return RedirectToAction("Details", "Schedule", new { id = workoutSession.ScheduleID });
            }

            // Om ModelState är ogiltig, hämta schemat igen för ViewBag
            var schedule = await _scheduleRepo.GetByIDAsync(workoutSession.ScheduleID);
            ViewBag.ScheduleStart = schedule?.StartDate ?? DateTime.Now;
            ViewBag.ScheduleEnd = schedule?.EndDate ?? DateTime.Now.AddHours(1);

            return View(workoutSession);
        }

        // GET: /Workout/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var session = await _workoutRepo.GetByIDAsync(id);
            if (session == null) return NotFound();

            // Null-säker ViewBag för start/slut (om du använder det i vyn)
            ViewBag.ScheduleStart = session.Schedule?.StartDate ?? DateTime.Now;
            ViewBag.ScheduleEnd = session.Schedule?.EndDate ?? DateTime.Now.AddHours(1);

            return View(session);
        }

        // POST: /Workout/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkoutSession workoutSession)
        {
            if (id != workoutSession.SessionID) return BadRequest();

            if (ModelState.IsValid)
            {
                await _workoutRepo.UpdateAsync(workoutSession);
                return RedirectToAction(nameof(Index));
            }

            // Om ModelState är ogiltig, hämta schemat igen för ViewBag
            var schedule = await _scheduleRepo.GetByIDAsync(workoutSession.ScheduleID);
            ViewBag.ScheduleStart = schedule?.StartDate ?? DateTime.Now;
            ViewBag.ScheduleEnd = schedule?.EndDate ?? DateTime.Now.AddHours(1);

            return View(workoutSession);
        }

        // GET: /Workout/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var session = await _workoutRepo.GetByIDAsync(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // POST: /Workout/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var session = await _workoutRepo.GetByIDAsync(id);
            if (session != null)
            {
                await _workoutRepo.DeleteAsync(id);
                return RedirectToAction("Details", "Schedule", new { id = session.ScheduleID });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
