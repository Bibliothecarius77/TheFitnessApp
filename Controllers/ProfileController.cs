using Microsoft.AspNetCore.Mvc;
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp.Controllers
{
    // Controller för användarens profil
    public class ProfileController : Controller
    {
        private readonly UnifiedContext _context;

        // Konstruktor med Dependency Injection
        public ProfileController(UnifiedContext context)
        {
            _context = context;
        }
/*
        // GET: /Profile
        // Visar användarens profil
        public IActionResult Index()
        {
            var profile = new UserProfile
            {
                FirstName = "Test",
                LastName = "User",
                AddrCity = "Stockholm",
                AddrCountry = "Sweden",
                DateOfBirth = new DateTime(1995, 1, 1),
                HeightCM = 170,
                WeightKG = 65
            };

            return View(profile);
        }

        // GET: /Profile/Goals
        // Visar användarens träningsmål
        public IActionResult Goals()
        {
            // Tillfälliga testmål tills Repository är klart
            var goals = new List<WorkoutGoal>
            {
                new WorkoutGoal
                {
                    //GoalID = 1,
                    GoalID = Guid.NewGuid(),
                    Type = GoalType.WeightLoss,
                    TargetValue = 10,
                    TargetDate = DateTime.Now.AddMonths(1),
                    IsCompleted = false
                }
            };

            return View(goals);
        }

        // GET: /Profile/Statistics
        // Visar användarens träningsstatistik
        public IActionResult Statistics()
        {
            // TODO: Hämta användarens statistik via Repository
            return View();
        }
*/
    }
}
