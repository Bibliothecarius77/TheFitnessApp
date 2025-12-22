using Microsoft.AspNetCore.Mvc;          // Ger tillgång till MVC-funktioner
using TheFitnessApp.Data;               // Ger tillgång till ApplicationDbContext
using TheFitnessApp.Models;             // Ger tillgång till Exercise-modellen

namespace TheFitnessApp.Controllers
{
    // Controller som ansvarar för hantering av övningar (Exercise)
    public class ExerciseController : Controller
    {
        // Privat fält för databaskontexten
        // (kommer senare ersättas av Repository)
        private readonly ApplicationDbContext _context;

        // Konstruktor med Dependency Injection.
        // ASP.NET Core skickar in databaskontexten automatiskt
        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // READ – visa alla objekt
        // GET: /Exercise
        public IActionResult Index()
        {
            // TODO: Hämta data via Repository när det är implementerat
            return View();
        }

        
        // READ – visa detaljer
        // GET: /Exercise/Details/{id}
        public IActionResult Details(int id)
        {
            // TODO: Hämta ett specifikt objekt via Repository
            return View();
        }

        
        // CREATE – visa formulär
        // GET: /Exercise/Create
        public IActionResult Create()
        {
            // Visar formulär för att skapa nytt objekt
            return View();
        }

        // CREATE – spara nytt objekt
        // POST: /Exercise/Create
        [HttpPost]
        public IActionResult Create(Exercise exercise)
        {
            // TODO: Skapa nytt objekt via Repository

            // Efter skapande skickas användaren tillbaka till Index
            return RedirectToAction(nameof(Index));
        }

       
        // UPDATE – visa formulär
        // GET: /Exercise/Edit/{id}
        public IActionResult Edit(int id)
        {
            // TODO: Hämta valt objekt via Repository
            return View();
        }

        // UPDATE – spara ändringar
        // POST: /Exercise/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, Exercise exercise)
        {
            // TODO: Uppdatera objekt via Repository

            // Efter uppdatering skickas användaren tillbaka till Index
            return RedirectToAction(nameof(Index));
        }

       
        // DELETE – visa bekräftelse
        // GET: /Exercise/Delete/{id}
        public IActionResult Delete(int id)
        {
            // TODO: Hämta objekt för bekräftelse via Repository
            return View();
        }

    
        // DELETE – ta bort objekt
        // POST: /Exercise/DeleteConfirmed/{id}
  
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Ta bort objekt via Repository

            // Efter borttagning skickas användaren tillbaka till Index
            return RedirectToAction(nameof(Index));
        }
    }
}
