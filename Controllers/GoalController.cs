using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för träningsmål (WorkoutGoal)
    public class GoalController : Controller
    {
        private readonly IRepository<WorkoutGoal> _repo;

        // Konstruktor med Dependency Injection
        public GoalController(IRepository<WorkoutGoal> repo)
        {
            _repo = repo;
        }

        // READ – visa alla träningsmål
        // GET: /Goal
        public async Task<IActionResult> Index()
        {
            var goals = await _repo.GetAsync();
            return View(goals);
        }

        // READ – visa detaljer för ett mål
        // GET: /Goal/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var goal = await _repo.GetByIDAsync(id);
            if (goal == null) return NotFound();
            return View(goal);
        }

        // CREATE – visa formulär
        // GET: /Goal/Create
        public IActionResult Create() => View();

        // CREATE – spara nytt mål
        // POST: /Goal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutGoal workoutGoal)
        {
            if (ModelState.IsValid)
            {
                await _repo.InsertAsync(workoutGoal);
                return RedirectToAction(nameof(Index));
            }
            return View(workoutGoal);
        }

        // UPDATE – visa formulär för redigering
        // GET: /Goal/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var goal = await _repo.GetByIDAsync(id);
            if (goal == null) return NotFound();
            return View(goal);
        }

        // UPDATE – spara ändringar
        // POST: /Goal/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkoutGoal workoutGoal)
        {
            if (id != workoutGoal.GoalID) return BadRequest();

            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(workoutGoal);
                return RedirectToAction(nameof(Index));
            }

            return View(workoutGoal);
        }

        // DELETE – visa bekräftelse
        // GET: /Goal/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var goal = await _repo.GetByIDAsync(id);
            if (goal == null) return NotFound();
            return View(goal);
        }

        // DELETE – ta bort mål
        // POST: /Goal/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
