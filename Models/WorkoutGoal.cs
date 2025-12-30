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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheFitnessApp.Models
{
    [Table("Goals")]                                       // DB table name
    public class WorkoutGoal
    {
        [Key]                                              // EF Core - DB Primary Key
        public int GoalID { get; set; }
        [ForeignKey(nameof(User))]                         // EF Core - DB Foreign Key
        public int UserID { get; set; }
        public GoalType Type { get; set; }
        public int TargetValue { get; set; }
        public DateTime Deadline { get; set; }
        public required bool IsCompleted { get; set; }

        public WorkoutGoal()
        {
            IsCompleted = false;

            // Add content here
        }

        // Add methods here
    }
}
