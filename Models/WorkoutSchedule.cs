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
    public class WorkoutSchedule
    {
        public Guid ScheduleID { get; set; }               // Primary Key
        public required AppUser User { get; set; }
        public required Guid UserID { get; set; }          // Foreign Key
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        // Navigation property for one-to-many relationsip
        public List<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();

        public WorkoutSchedule()
        {
            // Add content here
        }

        //public void CreateSchedule()
        //{
        //    // Add content here
        //}

        public void UpdateSchedule()
        {
            // Add content here
        }

        public void AddSession(WorkoutSession session)
        {
            Sessions ??= [];  // If no existing sessions, create a fresh array
            Sessions.Add(session);
        }

        public void AddSessionRange(WorkoutSession[] listSessions)
        {
            Sessions ??= [];  // If no existing sessions, create a fresh array
            Sessions.AddRange(listSessions);
        }

        public WorkoutSession[] GetUpcoming()
        {
            if (Sessions == null)
                return [];

            return Sessions
                .Where(s => s.StartTime >= DateTime.Now)
                .OrderBy(s => s.StartTime).ToArray();
        }

        public WorkoutSession[] GetHistory()
        {
            // If no existing sessions, return an empty array
            if (Sessions == null)
                return [];

            return Sessions
                .Where(s => s.EndTime < DateTime.Now)
                .OrderByDescending(s => s.StartTime).ToArray();
        }
    }
}
