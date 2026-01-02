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
    //[Table("Statistics")]                                  // DB table name
    public class Statistics
    {
        //[Key]                                              // EF Core - DB Primary Key
        public Guid StatsID { get; set; }
        public required AppUser User { get; set; }
        //[ForeignKey(nameof(AppUser))]                      // EF Core - DB Foreign Key
        public required Guid UserID { get; set; }          // EF Core - DB Foreign Key
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
