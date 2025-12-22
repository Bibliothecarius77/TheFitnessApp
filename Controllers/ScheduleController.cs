// Gör det möjligt att använda MVC-funktioner som Controller och IActionResult
using Microsoft.AspNetCore.Mvc;

// Ger tillgång till ApplicationDbContext (databasen)
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    public class ScheduleController : Controller // Controller som ansvarar för träningsscheman (WorkoutSchedule)
    {
        private readonly ApplicationDbContext _context;  // Databaskontext (ersätts senare av Repository)

        public ScheduleController(ApplicationDbContext context) // Konstruktor som använder Dependency Injection. // ApplicationDbContext skickas in automatiskt av ASP.NET Core
        {
            _context = context;  // Sparar kontexten så den kan användas i hela controllern, t.ex. för att läsa och skriva till databasen.
        }

        // GET: /Schedule
        // Hämtar alla träningsscheman från databasen
        public IActionResult Index()
        {
            // TODO: Hämta träningsscheman via Repository när det är implementerat
            // Tillfälligt returneras endast vyn
            return View();
        }

        
        // READ – visa detaljer för ett schema
        // GET: /Schedule/Details/{id}
       
        public IActionResult Details(int id)
        {
            // TODO: Hämta specifikt schema via Repository baserat på id
            return View();
        }

        // GET: /Schedule/Create
       
        public IActionResult Create()
        {
            // Visar formulär för att skapa ett nytt schema
            return View();
        }

        
        // CREATE – spara nytt schema
        // POST: /Schedule/Create
        [HttpPost]
        public IActionResult Create(WorkoutSchedule workoutSchedule)
        {
            // TODO: Skapa nytt träningsschema via Repository

            // Efter skapande skickas användaren tillbaka till Index

            return RedirectToAction(nameof(Index));
        }

      
        // UPDATE – visa formulär för redigering
        // GET: /Schedule/Edit/{id}
        public IActionResult Edit(int id)
        {
            // TODO: Hämta valt schema via Repository
            return View();
        }

       
        // UPDATE – spara ändringar
        // POST: /Schedule/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, WorkoutSchedule workoutSchedule)
        {
            // TODO: Uppdatera träningsschema via Repository

            return RedirectToAction(nameof(Index));
        }

        
        // DELETE – visa bekräftelse
        // GET: /Schedule/Delete/{id}
  
        public IActionResult Delete(int id)
        {
            // TODO: Hämta schema för bekräftelse via Repository
            return View();
        }


        // DELETE – ta bort schema
        // POST: /Schedule/DeleteConfirmed/{id}

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Ta bort träningsschema via Repository

            return RedirectToAction(nameof(Index));
        }
    }
}
