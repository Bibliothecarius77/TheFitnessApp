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
    [Table("Sessions")]                                    // DB table name
    public class WorkoutSession
    {
        [Key]                                              // EF Core - DB Primary Key
        public int SessionID { get; set; }
        [ForeignKey(nameof(WorkoutSchedule))]              // EF Core - DB Foreign Key
        public int ScheduleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalCalories { get; set; }

        public WorkoutSession()
        {
            // Add content here
        }

        // Add methods here
    }
}
