using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Exercise
        public IActionResult Index()
        {
            // TODO: Hämta data via Repository när det är implementerat
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    ExerciseID = 1,
                    SessionID = 1,
                    Type = ExerciseType.Cardio,
                    Category = "Test exercise",
                    Sets = 3,
                    Reps = 10,
                    WeightKG = 50,
                    CaloriesBurnt = 200,
                    METValue = 5.5f
                },
                new Exercise
                {
                    ExerciseID = 2,
                    SessionID = 1,
                    Type = ExerciseType.Strength,
                    Category = "Test exercise",
                    Sets = 4,
                    Reps = 8,
                    WeightKG = 70,
                    CaloriesBurnt = 250,
                    METValue = 6.0f
                }
            };

            return View(exercises);
        }

        // GET: /Exercise/Details/{id}
        public IActionResult Details(int id)
        {
            var exercise = new Exercise
            {
                ExerciseID = id,
                SessionID = 1,
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
            //return View();
            var exercise = new Exercise
            {
                ExerciseID = id,
                Category = "Strength",
                Sets = 3,
                Reps = 10,
                WeightKG = 50,
                METValue = 6.0f,
                CaloriesBurnt = 200
            };
            return View(exercise);
        }

        // POST: /Exercise/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, Exercise exercise)
        {
            // TODO: Uppdatera via Repository
            return RedirectToAction(nameof(Index));
        }

        // GET: /Exercise/Delete/{id}
        public IActionResult Delete(int id)
        {
            var exercise = new Exercise
            {
                ExerciseID = id,
                Category = "Strength",
                Sets = 3,
                Reps = 10,
                WeightKG = 50,
                CaloriesBurnt = 200
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
