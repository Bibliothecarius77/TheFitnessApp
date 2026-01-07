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
    public class AppUser : IdentityUser<Guid>
    {
        // These entities are provided by the parent class (IdentityUser):
        //   Id                   - Primary Key
        //   UserName
        //   PhoneNumber
        //   PhoneNumberConfirmed - Flag indicating if a user has confirmed their telephone number
        //   Email
        //   NormalizedEmail      - E-mail address in upperacase letters only
        //   EmailConfirmed       - Flag indicating if the user has confirmed their email address
        //   PasswordHash         - Hashed representation of the password
        //   TwoFactorEnabled     - Flag indicating if two factor authentication is enabled
        //   LockoutEnabled       - Flag indicating if the user could be locked out
        //   LockoutEnd           - Date and time, in UTC, when any user lockout ends
        //   SecurityStamp        - A random value that must change whenever the user's
        //                          credentials change (password changed, login removed)
        //
        // These navigation properties are provided by the parent class (IdentityUser):
        //   Claims               - The claims this user possesses
        //   Logins               - This user's login accounts
        //   Roles                - The roles this user belongs to (UserRole)

        // Overriding the PK would have been nice, but too much work for everybody now...
        // public override Guid UserID { get; set; }          // Primary Key

        //public required UserProfile Profile { get; set; }  // Inverse navigation
        //public required Guid ProfileID { get; set; }       // Foreign Key (auto)
        public DateTime CreationDate { get; set; }

        // Navigation properties for one-to-one relationsips
        public UserProfile? Profile { get; set; }
        public WorkoutSchedule? Schedule { get; set; }

        // Navigation properties for one-to-many relationsips
        //public ICollection<WorkoutGoal> Goals { get; set; } = new List<WorkoutGoal>();
        public List<WorkoutGoal> Goals { get; set; } = new List<WorkoutGoal>();
        //public ICollection<Statistics> Statistics { get; set; } = new List<Statistics>();
        public List<Statistics> Statistics { get; set; } = new List<Statistics>();

        public AppUser()
        {
            CreationDate = DateTime.Now;
        }
    }
}
