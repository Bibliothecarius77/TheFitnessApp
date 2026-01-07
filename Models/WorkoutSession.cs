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
        public Guid SessionID { get; set; }                     // Primary Key
        //public required WorkoutSchedule Schedule { get; set; }
        public required WorkoutSchedule Schedule
        {
            get
            {
                return Schedule;
            }

            set
            {
                ScheduleID = value.ScheduleID;
            }
        }
        public Guid ScheduleID { get; set; }                    // Foreign Key
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public int TotalCalories { get; set; }

        // Navigation property for one-to-many relationsip
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        public WorkoutSession()
        {
        }

        public void UpdateTimes(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new InvalidOperationException("End time must be after start time.");

            StartTime = start;
            EndTime = end;
        }
    }
}
