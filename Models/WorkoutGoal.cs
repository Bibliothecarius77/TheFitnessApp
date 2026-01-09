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
    public class WorkoutGoal
    {
        public Guid GoalID { get; set; }                   // Primary Key
        public required AppUser User { get; set; }
        public required Guid UserID { get; set; }          // Foreign Key
        public GoalType Type { get; set; }
        public int TargetValue { get; set; }
        public DateTime TargetDate { get; set; }
        public required bool IsCompleted { get; set; }

        public WorkoutGoal()
        {
            IsCompleted = false;

            // Add content here
        }

        // Add methods here
    }
}
