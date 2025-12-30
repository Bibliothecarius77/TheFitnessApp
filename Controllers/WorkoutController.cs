using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för träningspass (WorkoutSession)
    public class WorkoutController : Controller
    {
        // Databaskontext
        private readonly ApplicationDbContext _context;

        // Konstruktor med Dependency Injection. ASP.NET Core skickar in ApplicationDbContext automatiskt.
        public WorkoutController(ApplicationDbContext context)
        {
            _context = context; // Gör databasen tillgänglig i controllern för att läsa och spara data
        }

        // GET: /Workout
        // Visar alla träningspas
       public IActionResult Index()
        {
            // TODO: Hämta träningspass via Repository när det är implementerat
            return View();
        }

        // READ – visa detaljer för ett träningspass
        // GET: /Workout/Details/{id}
        public IActionResult Details(int id)
        {
            // Här ska ett specifikt träningspass hämtas via Repository baserat på id 
            
            return View();
        }

        // GET: /Workout/Create
        public IActionResult Create()
        {
            // Visar formulär för att skapa ett nytt träningspass
            return View();
        }

        // CREATE – spara nytt träningspass
        // POST: /Workout/Create
         [HttpPost]
        public IActionResult Create(WorkoutSession workoutSession)
        {
            // TODO: Skapa nytt träningspass via Repository

            return RedirectToAction(nameof(Index));
        }


        // UPDATE – visa formulär för redigering
        // GET: /Workout/Edit/{id}
        public IActionResult Edit(int id)
        {
            // Här ska valt träningspass hämtas via Repository
            return View();
        }

       
        // UPDATE – spara ändringar
        // POST: /Workout/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, WorkoutSession workoutSession)
        {
            // TODO: Uppdatera träningspass via Repository

            return RedirectToAction(nameof(Index));
        }

        
        // DELETE – visa bekräftelse
        // GET: /Workout/Delete/{id}
        public IActionResult Delete(int id)
        {
            // TODO: Hämta träningspass för bekräftelse via Repository
            return View();
        }

        
        // DELETE – ta bort träningspass
        // POST: /Workout/DeleteConfirmed/{id}
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Ta bort träningspass via Repository

            return RedirectToAction(nameof(Index));
        }
    }
}
