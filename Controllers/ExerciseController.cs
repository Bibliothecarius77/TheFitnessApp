using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
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
}
