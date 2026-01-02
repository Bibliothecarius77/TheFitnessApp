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
    //[Table("Schedules")]                                   // DB table name
    public class WorkoutSchedule
    {
        //[Key]                                              // EF Core - DB Primary Key
        public Guid ScheduleID { get; set; }
        public required AppUser User { get; set; }
        //[ForeignKey(nameof(AppUser))]                      // EF Core - DB Foreign Key
        public required Guid UserID { get; set; }          // EF Core - DB Foreign Key
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        // Navigation properties for one-to-many relationsips
        public ICollection<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();

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

        public WorkoutSession[] GetUpcoming()
        { 
            return listSessions
              .Where(s => s.StartTime >= DateTime.Now)
              .OrderBy(s => s.StartTime).ToArray();
        }

        public WorkoutSession[] GetHistory()
        {
            return listSessions
              .Where(s => s.EndTime < DateTime.Now)
              .OrderByDescending(s => s.StartTime).ToArray();
        }

        // Maybe add more methods here
    }
}
