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
    public class WorkoutSchedule
    {
        public Guid ScheduleID { get; set; }
        public required AppUser User { get; set; }
        public required Guid UserID { get; set; }          // EF Core - DB Foreign Key
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        // Navigation properties for one-to-many relationsips
        //public ICollection<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();
        public List<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();

        public WorkoutSchedule()
        {
            // Add content here
        }

        public void CreateSchedule()
        {
            // Add content here
        }

        public void UpdateSchedule()
        {
            // Add content here
        }

        public void AddSession(WorkoutSession session)
        {
            if (Sessions == null)
                Sessions = [];

            Sessions.Add(session);
        }

        public void AddSessionRange(WorkoutSession[] listSessions)
        {
            if (Sessions == null)
                Sessions = [];

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
            if (Sessions == null)
                return [];

            return Sessions
              .Where(s => s.EndTime < DateTime.Now)
              .OrderByDescending(s => s.StartTime).ToArray();
        }

        // Maybe add more methods here
    }
}
