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
    [Table("UserProfiles")]                                // DB table name
    public class UserProfile
    {
        [Key]                                              // EF Core - DB Primary Key
        public int ProfileID { get; set; }
        [ForeignKey(nameof(User))]                         // EF Core - DB Foreign Key
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddrStreet { get; set; }
        public int AddrNumber { get; set; }
        public string AddrZip { get; set; }
        public string AddrCity { get; set; }
        public string AddrCountry { get; set; }
        //public string AddrPhone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HeightCM { get; set; }
        public float WeightKG { get; set; }

        public UserProfile()
        {
            // Add content here
        }

        public void UpdateProfile()
        {
            // Add content here
        }

        // Maybe add more methods here
    }
}
