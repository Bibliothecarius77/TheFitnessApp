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
    public class UserProfile
    {
        public Guid ProfileID { get; set; }
        public required AppUser User { get; set; }
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

        public void UpdateProfile()
        {
            // Add content here (?)
        }

        // Maybe add more methods here
    }
}
