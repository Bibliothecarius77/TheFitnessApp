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
            // Tillfällig testdata (utan databas)
            var schedules = new List<WorkoutSchedule>
    {
        new WorkoutSchedule
        {
            ScheduleID = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(7),
            Notes = "Test schedule – Week 1"
        },
        new WorkoutSchedule
        {
            ScheduleID = 2,
            StartDate = DateTime.Today.AddDays(7),
            EndDate = DateTime.Today.AddDays(14),
            Notes = "Test schedule – Week 2"
        }
    };

            return View(schedules);
        }



        // READ – visa detaljer för ett schema
        // GET: /Schedule/Details/{id} 
        //Tillfällig lösning för att visa detaljer för ett träningsschema baserat på ID
        public IActionResult Details(int id)
        {
            var schedule = new WorkoutSchedule
            {
                ScheduleID = id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Notes = "Test schedule"
            };

            return View(schedule);
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
        // tillfällig lösning för att visa redigeringsformulär för ett träningsschema baserat på ID
        public IActionResult Edit(int id)
        {
            var schedule = new WorkoutSchedule
            {
                ScheduleID = id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Notes = "Editable test schedule"
            };

            return View(schedule);
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
        // tillfällig lösning för att visa bekräftelsesidan för att ta bort ett träningsschema baserat på ID

        public IActionResult Delete(int id)
        {
            var schedule = new WorkoutSchedule
            {
                ScheduleID = id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Notes = "Schedule to be deleted"
            };

            return View(schedule);
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
