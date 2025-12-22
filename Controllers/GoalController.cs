using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för träningsmål (WorkoutGoal)
    public class GoalController : Controller
    {
        // // Databaskontext (ersätts senare av Repository)
        private readonly ApplicationDbContext _context;

        // Konstruktor med Dependency Injection
        public GoalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ – visa alla träningsmål
        // GET: /Goal
        public IActionResult Index()
        {
            // TODO: Hämta data via Repository när det är implementerat

            // Visar vyn: Views/Goal/Index.cshtml tills Repository-logiken är implementerad
            return View();
        }

        
        // READ – visa detaljer för ett mål
        // GET: /Goal/Details/{id}
        public IActionResult Details(int id)
        {
            // TODO: Hämta specifikt mål via Repository
            return View();
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
        
        public IActionResult Edit(int id)
        {
            // TODO: Hämta valt träningsmål via Repository
            return View();
        }

       
        // UPDATE – spara ändringar
        // POST: /Goal/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, WorkoutGoal workoutGoal)
        {
            // TODO: Uppdatera träningsmål via Repository

            return RedirectToAction(nameof(Index));
        }

        
        // DELETE – visa bekräftelse
        // GET: /Goal/Delete/{id}
        public IActionResult Delete(int id)
        {
            // TODO: Hämta träningsmål för bekräftelse via Repository
            return View();
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
