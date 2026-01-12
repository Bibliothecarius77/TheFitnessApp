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

using System.ComponentModel.DataAnnotations;

namespace TheFitnessApp.Models
{
    public class WorkoutSessionInputModel
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }

    public class WorkoutSession
    {
        public Guid SessionID { get; set; }                     // Primary Key
        public required WorkoutSchedule Schedule
        {
            get;
            set
            {
                // Set Foreign Key from this entity
                ScheduleID = value.ScheduleID;

                field = value;
            }
        }
        public Guid ScheduleID { get; set; }                    // Foreign Key
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime
        {
            get;
            set
            {
                if (value <= StartTime)
                    throw new InvalidOperationException("End time must be after start time.");

                field = value;
            }
        }
        public int TotalCalories { get; set; }

        // Navigation property for one-to-many relationsip
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        // Constructor for EF Core
        public WorkoutSession()
        {
        }

        public void UpdateTimes(DateTime start, DateTime end)
        {
            StartTime = start;
            EndTime = end;
        }
    }
}
