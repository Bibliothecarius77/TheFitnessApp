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

using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserProfile> UserProfiles { get; set; } = default!;
        public DbSet<WorkoutSchedule> Schedules { get; set; } = default!;
        public DbSet<WorkoutSession> Sessions { get; set; } = default!;
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutGoal> Goals { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
    }
}
