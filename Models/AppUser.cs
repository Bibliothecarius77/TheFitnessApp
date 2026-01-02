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

using Microsoft.AspNetCore.Identity;

namespace TheFitnessApp.Models
{
    //[Table("Users")]                                       // DB table name
    //[Table("AspNetUsers")]                                 // DB table name
    public class AppUser : IdentityUser<Guid>
    {
        // ASP.NET uses string 'Id' as the primary key by default

        //[Key]                                              // EF Core - DB Primary Key
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public override Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

        // Navigation properties for one-to-one relationsips
        public UserProfile? Profile { get; set; }
        public WorkoutSchedule? Schedule { get; set; }

        // Navigation properties for one-to-many relationsips
        public ICollection<WorkoutGoal> Goals { get; set; } = new List<WorkoutGoal>();
        public ICollection<Statistics> Statistics { get; set; } = new List<Statistics>();

        public AppUser()
        {
            CreationDate = DateTime.Now;
        }

        // Returns true if succesful
        //public bool Login()
        //{
        //    // Add content here
        //    return false;
        //}

        //public void AssignRole(UserRole role)
        //{
        //    Role = role;
        //}
        //public async Task<IdentityResult> AssignRoleAsync(string role)
        //{
        //    await _userRoleService.AssignRoleAsync(this, role);
        //}
    }
}
