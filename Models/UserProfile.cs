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
    //[Table("UserProfiles")]                                // DB table name
    public class UserProfile
    {
        //[Key]                                              // EF Core - DB Primary Key
        public Guid ProfileID { get; set; }
        public required AppUser User { get; set; }
        //[ForeignKey(nameof(AppUser))]                      // EF Core - DB Foreign Key
        public required Guid UserID { get; set; }          // EF Core - DB Foreign Key
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? AddrStreet { get; set; }
        public int AddrNumber { get; set; }
        public string? AddrZip { get; set; }
        public required string AddrCity { get; set; }
        public required string AddrCountry { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public int HeightCM { get; set; }
        public float WeightKG { get; set; }

        public UserProfile()
        {
        }

        // Do we need this constructor?
        //public UserProfile(string firstName, string lastName, string city, string country, DateTime dateOfBirth)
        //{
        //}

        // Do we need this constructor?
        //public UserProfile(string firstName, string lastName, string street, int number, string zip,
        //    string city, string country, DateTime dateOfBirth, int heightCM, float weightKG)
        //{
        //}

        public void UpdateProfile()
        {
            // Add content here (?)
        }

        // Maybe add more methods here
    }
}
