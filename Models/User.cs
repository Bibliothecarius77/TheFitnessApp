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
    [Table("Users")]                                       // DB table name
    public class User
    {
        [Key]                                              // EF Core - DB Primary Key
        public int UserID { get; set; }
        public UserRole Role { get; set; }
        public required string Email { get; set; }  // Required for login
        public required string PasswordHash { get; set; }  // Required for login
        public DateTime CreationDate { get; set; }

        public User()
        {
            // Add content here
        }

        // Returns true if succesful
        public bool Login()
        {
            // Add content here
            return false;
        }

        public void AssignRole(UserRole role)
        {
            // Add content here
        }

        // Maybe add more methods here
    }
}
