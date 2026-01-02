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
    //[Table("Exercises")]                                   // DB table name
    public class Exercise
    {
        //[Key]                                              // EF Core - DB Primary Key
        public Guid ExerciseID { get; set; }
        //[ForeignKey(nameof(WorkoutSession))]               // EF Core - DB Foreign Key
        public Guid SessionID { get; set; }
        public required WorkoutSession Session { get; set; }
        public ExerciseType Type { get; set; }
        public string? Category { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int WeightKG { get; set; }
        public int CaloriesBurnt { get; set; }
        public float METValue { get; set; }

        public Exercise()
        {
            // Add content here
        }

        // Add methods here
    }
}
