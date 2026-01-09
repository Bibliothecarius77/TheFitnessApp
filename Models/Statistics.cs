/*
 * ZenMove - The Ultimate Fitness App
 *
 * IT-påbyggnad Utvecklare, Fullstack .NET (Lexicon)
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
    public class Statistics
    {
        public Guid StatsID { get; set; }                  // Primary Key
        public required AppUser User { get; set; }
        public required Guid UserID { get; set; }          // Foreign Key
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int TotalWorkouts { get; set; }
        public int TotalDurationMin { get; set; }
        public int TotalCaloriesBurnt { get; set; }

        public Statistics()
        {
            // Add content here
        }

        // Add methods here
    }
}
