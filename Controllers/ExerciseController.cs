using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly UnifiedContext _context;

        public ExerciseController(UnifiedContext context)
        {
            _context = context;
        }

        // GET: /Exercise
        public IActionResult Index()
        {
            // TODO: Hämta data via Repository när det är implementerat
            return View();
        }

        // GET: /Exercise/Details/{id}
        //public IActionResult Details(int id)
        public IActionResult Details(Guid id)
        {
            var exercise = new Exercise
            {
                ExerciseID = id,
                //SessionID = 1,
                SessionID = Guid.NewGuid(),
                Type = ExerciseType.Cardio,
                Category = "Test exercise",
                Sets = 3,
                Reps = 10,
                WeightKG = 50,
                CaloriesBurnt = 200,
                METValue = 5.5f
            };

            return View(exercise);
        }

        // GET: /Exercise/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Exercise/Create
        [HttpPost]
        public IActionResult Create(Exercise exercise)
        {
            // TODO: Skapa via Repository
            return RedirectToAction(nameof(Index));
        }

        // GET: /Exercise/Edit/{id}
        public IActionResult Edit(int id)
        {
            // TODO: Hämta via Repository
            return View();
        }

        // POST: /Exercise/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, Exercise exercise)
        {
            // TODO: Uppdatera via Repository
            return RedirectToAction(nameof(Index));
        }

        // GET: /Exercise/Delete/{id}
        //public IActionResult Delete(int id)
        public IActionResult Delete(Guid id)
        {
            var sessionNew = new WorkoutSession();

            var exercise = new Exercise
            {
                ExerciseID = id,
                SessionID = sessionNew.SessionID,
                Session = sessionNew,
                Category = "Strength",
                Sets = 3,
                Reps = 10,
                WeightKG = 50
            };

            return View(exercise);
        }

        // POST: /Exercise/DeleteConfirmed/{id}
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Ta bort via Repository
            return RedirectToAction(nameof(Index));
        }
    }
}
