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
    public class UserProfile
    {
        public Guid ProfileID { get; set; }                // Primary Key
        public required AppUser User { get; set; }
        public required Guid UserID { get; set; }          // Foreign Key
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
/*
        public void UpdateProfile()
        {
        }
*/
        // Maybe add more methods here
    }
}
