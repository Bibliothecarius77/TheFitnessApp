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
    [Table("Schedules")]                                   // DB table name
    public class WorkoutSchedule
    {
        [Key]                                              // EF Core - DB Primary Key
        public int ScheduleID { get; set; } 
        [ForeignKey(nameof(User))]                         // EF Core - DB Foreign Key
        public int UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        private List<WorkoutSession> listSessions;

        public WorkoutSchedule()
        {
            listSessions = new List<WorkoutSession>();

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
            listSessions.Add(session);
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
