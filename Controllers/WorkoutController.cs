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
            var schedule = new WorkoutSchedule();
            schedule.listSessions.AddRange(new[]
            {
                new WorkoutSession
                {
                    SessionID = 1,
                    ScheduleID = 1,
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(1),
                    TotalCalories = 450
                },
                new WorkoutSession
                {
                    SessionID = 2,
                    ScheduleID = 1,
                    StartTime = DateTime.Now.AddDays(-2),
                    EndTime = DateTime.Now.AddDays(-2).AddHours(1),
                    TotalCalories = 300
                },
                new WorkoutSession
                {
                    SessionID = 3,
                    ScheduleID = 2,
                    StartTime = DateTime.Now.AddDays(3),
                    EndTime = DateTime.Now.AddDays(3).AddHours(1),
                    TotalCalories = 500
                }
            });

            // TODO: Hämta träningspass via Repository när det är implementerat
            return View(schedule.listSessions);
        }

        // READ – visa detaljer för ett träningspass
        // GET: /Workout/Details/{id}
        public IActionResult Details(int id)
        {
            // Här ska ett specifikt träningspass hämtas via Repository baserat på id 
            var session = new WorkoutSession
            {
                SessionID = id,
                ScheduleID = 1,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-1).AddHours(1),
                TotalCalories = 400
            };
            return View(session);
        }

        // GET: /Workout/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            // TEMP sample data (replace with DB later)
            var session = new WorkoutSession
            {
                ScheduleID = id,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                TotalCalories = 200
            };

            // Pass date limits to View
            //ViewBag.ScheduleStart = schedule.StartDate;
            //ViewBag.ScheduleEnd = schedule.EndDate;

            return View(session);
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // TEMP sample data (replace with DB later)
            var session = new WorkoutSession
            {
                SessionID = id,
                ScheduleID = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                TotalCalories = 400
            };
            
            // Pass date limits to View
            //ViewBag.ScheduleStart = schedule.StartDate;
            //ViewBag.ScheduleEnd = schedule.EndDate;

            return View(session);
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var session = new WorkoutSession
            {
                SessionID = id,
                ScheduleID = 2,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-1).AddHours(1),
                TotalCalories = 300
            };
            return View(session);
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
