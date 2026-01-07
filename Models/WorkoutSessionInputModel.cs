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

using Microsoft.Build.Framework;

namespace TheFitnessApp.Models
{
    public class WorkoutSessionInputModel
    {
                //[Required]
                //public WorkoutSchedule Schedule { get; set; }

                [Required]
                public DateTime StartTime { get; set; }

                [Required]
                public DateTime EndTime { get; set; }
    }
}
