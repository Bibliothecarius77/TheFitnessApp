/*
 * ZenMove - The Ultimate Fitness App
 *
 * IT-påbyggnad Utvecklare (Lexicon)
 * Kursvecka 13-16 (vv.2551-2602)
 *
 * Grupp Blå:
 *   Arsalan Habib
 *   Jacob Damm
 *   Liridona Demaj
 *   Victoria Rådberg
 */

using Microsoft.AspNetCore.Identity;

namespace TheFitnessApp.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        //public override Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

        // Navigation properties for one-to-one relationsips
        public UserProfile? Profile { get; set; }
        public WorkoutSchedule? Schedule { get; set; }

        // Navigation properties for one-to-many relationsips
        public ICollection<WorkoutGoal> Goals { get; set; } = new List<WorkoutGoal>();
        public ICollection<Statistics> Statistics { get; set; } = new List<Statistics>();

        public AppUser()
        {
            CreationDate = DateTime.Now;
        }
    }
}
