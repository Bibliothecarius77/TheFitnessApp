using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för träningsmål (WorkoutGoal)
    public class GoalController : Controller
    {
        // // Databaskontext (ersätts senare av Repository)
        private readonly UnifiedContext _context;

        // Konstruktor med Dependency Injection
        // ASP.NET Core skickar in UnifiedContext automatiskt
        public GoalController(UnifiedContext context)
        {
            _context = context;
        }

        // READ – visa alla träningsmål
        // GET: /Goal
        public IActionResult Index()
        {
            // TODO: Hämta data via Repository när det är implementerat

            // Just nu visas endast vyn (skelett)
            return View();
        }


        // READ – visa detaljer för ett mål
        // GET: /Goal/Details/{id}
        //public IActionResult Details(int id)
        public IActionResult Details(Guid id)
        {
            // TEMPORÄRT testobjekt tills Repository är klart
            var goal = new WorkoutGoal
            {
                GoalID = id,
                //UserID = 1,
                UserID = Guid.NewGuid(),
                Type = GoalType.WeightLoss,
                TargetValue = 10,
                TargetDate = DateTime.Now.AddMonths(1),
                IsCompleted = false
            };

            return View(goal);
        }



        // CREATE – visa formulär
        // GET: /Goal/Create
        public IActionResult Create()
        {
            // Visar formulär för att skapa ett nytt träningsmål
            return View();
        }

        // CREATE – spara nytt mål
        // POST: /Goal/Create
        [HttpPost]
        public IActionResult Create(WorkoutGoal workoutGoal)
        {
            // TODO: Skapa nytt träningsmål via Repository

            return RedirectToAction(nameof(Index));
        }


        // UPDATE – visa formulär för redigering
        // GET: /Goal/Edit/{id}
        //public IActionResult Edit(int id)
        public IActionResult Edit(Guid id)
        {
            // TEMPORÄRT testobjekt
            var goal = new WorkoutGoal
            {
                GoalID = id,
                Type = GoalType.WeightLoss,
                TargetValue = 10,
                TargetDate = DateTime.Now.AddMonths(1),
                IsCompleted = false
            };

            return View(goal);
        }

        // UPDATE – spara ändringar
        // POST: /Goal/Edit/{id}
        [HttpPost]
        //public IActionResult Edit(int id, WorkoutGoal workoutGoal)
        public IActionResult Edit(Guid id, WorkoutGoal workoutGoal)
        {
            // TODO: Uppdatera träningsmål via Repository

            return RedirectToAction(nameof(Index));
        }


        // DELETE – visa bekräftelse
        // GET: /Goal/Delete/{id}
        //public IActionResult Delete(int id)
        public IActionResult Delete(Guid id)
        {
            // TEMPORÄRT testobjekt
            var goal = new WorkoutGoal
            {
                GoalID = id,
                Type = GoalType.WeightLoss,
                TargetValue = 10,
                TargetDate = DateTime.Now.AddMonths(1),
                IsCompleted = false
            };

            return View(goal);
        }


        // DELETE – ta bort mål
        // POST: /Goal/DeleteConfirmed/{id}
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Ta bort träningsmål via Repository

            return RedirectToAction(nameof(Index));
        }
    }
}
