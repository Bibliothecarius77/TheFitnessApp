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

namespace TheFitnessApp.Models
{
    public class WorkoutSession
    {
        public Guid SessionID { get; set; }
        public required WorkoutSchedule Schedule { get; set; }
        public Guid ScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalCalories { get; set; }

        // Navigation properties for one-to-many relationsips
        //public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        public WorkoutSession()
        {
            // Add content here
        }

        // Add methods here
    }
}
