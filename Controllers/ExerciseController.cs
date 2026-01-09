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
using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
/*
    public class ExerciseController : Controller
    {
        private readonly IRepository<Exercise> _exerciseRepo;

        public ExerciseController(IRepository<Exercise> exerciseRepo)
        {
            _exerciseRepo = exerciseRepo;
        }

        // GET: /Exercise
        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseRepo.GetAsync();
            return View(exercises);
        }

        // GET: /Exercise/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var exercise = await _exerciseRepo.GetByIDAsync(id);
            if (exercise == null)
                return NotFound();

            return View(exercise);
        }

        // GET: /Exercise/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Exercise/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                await _exerciseRepo.InsertAsync(exercise);
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: /Exercise/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var exercise = await _exerciseRepo.GetByIDAsync(id);
            if (exercise == null)
                return NotFound();

            return View(exercise);
        }

        // POST: /Exercise/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Exercise exercise)
        {
            if (id != exercise.ExerciseID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _exerciseRepo.UpdateAsync(exercise);
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: /Exercise/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var exercise = await _exerciseRepo.GetByIDAsync(id);
            if (exercise == null)
                return NotFound();

            return View(exercise);
        }

        // POST: /Exercise/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _exerciseRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
*/

    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly UnifiedContext _context;
        private readonly IUserAggregateService _users;
        private readonly UserManager<AppUser> _userManager;

        public ExerciseController(UnifiedContext context, IUserAggregateService users, UserManager<AppUser> userManager)
        {
            _context = context;
            _users = users;
            _userManager = userManager;
        }

        // GET: /Exercise/Create/{sessionId}
        public async Task<IActionResult> Create(Guid sessionId)
        {
            var user = await GetCurrentUserAsync();
            var session = user.Schedule!.Sessions.SingleOrDefault(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound();

            ViewBag.SessionId = sessionId;

            return View(new ExerciseInputModel());
        }

        // POST: /Exercise/Create/{sessionId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid sessionId, ExerciseInputModel input)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SessionId = sessionId;
                return View(input);
            }

            var user = await GetCurrentUserAsync();
            var session = user.Schedule!.Sessions.SingleOrDefault(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound();

            // Set non-required properties
            //Exercise exercise = new(
            var exercise = new Exercise(
                input.Type,
                input.Category,
                input.Sets,
                input.Reps,
                input.WeightKG,
                input.CaloriesBurnt,
                input.METValue
            );

            session.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Schedule", new { id = sessionId });
        }

        // GET: /Exercise/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var exercise = user.Schedule!.Sessions.SelectMany(s => s.Exercises).SingleOrDefault(e => e.ExerciseID == id);

            if (exercise == null)
                return NotFound();

            var model = new ExerciseInputModel
            {
                Type = exercise.Type,
                Category = exercise.Category!,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                WeightKG = exercise.WeightKG,
                CaloriesBurnt = exercise.CaloriesBurnt,
                METValue = exercise.METValue
            };

            return View(model);
        }

        // POST: /Exercise/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExerciseInputModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var user = await GetCurrentUserAsync();
            var exercise = user.Schedule!.Sessions.SelectMany(s => s.Exercises).SingleOrDefault(e => e.ExerciseID == id);

            if (exercise == null)
                return NotFound();

            exercise.Update(
                input.Type,
                input.Category,
                input.Sets,
                input.Reps,
                input.WeightKG,
                input.CaloriesBurnt,
                input.METValue
            );

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Schedule");
        }

        // GET: /Exercise/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var exercise = user.Schedule!.Sessions.SelectMany(s => s.Exercises).SingleOrDefault(e => e.ExerciseID == id);

            if (exercise == null)
                return NotFound();

            return View(exercise);
        }

        // POST: /Exercise/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await GetCurrentUserAsync();
            var session = user.Schedule!.Sessions.SingleOrDefault(s => s.Exercises.Any(e => e.ExerciseID == id));

            if (session == null)
                return NotFound();

            var exercise = session.Exercises.Single(e => e.ExerciseID == id);
            session.Exercises.Remove(exercise);

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Schedule");
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            return await _users.GetAsync(userId)
                ?? throw new InvalidOperationException("User not found.");
        }
    }
}
