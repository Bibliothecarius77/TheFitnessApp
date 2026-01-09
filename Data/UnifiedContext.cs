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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    public class UnifiedContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        // DbSet<AppUser> is created "behind the scenes" by ASP.NET Core Identity, it must not be added here
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkoutGoal> Goals { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<WorkoutSchedule> Schedules { get; set; }
        public DbSet<WorkoutSession> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public UnifiedContext(DbContextOptions<UnifiedContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureDomain(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Could seed data here automatically
            // Now it's called from Program.cs instead
        }

        // Database structure definitions
        private static void ConfigureDomain(ModelBuilder builder)
        {
            // Fluent API - Map AppUser class to the standard ASP.NET database table
            builder.Entity<AppUser>().ToTable("AspNetUsers");

            // Fluent API - Define primary keys
            builder.Entity<UserProfile>()
                .HasKey(u => u.ProfileID);

            builder.Entity<WorkoutGoal>()
                .HasKey(u => u.GoalID);

            builder.Entity<Statistics>()
                .HasKey(u => u.StatsID);

            builder.Entity<WorkoutSchedule>()
                .HasKey(u => u.ScheduleID);

            builder.Entity<WorkoutSession>()
                .HasKey(u => u.SessionID);

            builder.Entity<Exercise>()
                .HasKey(u => u.ExerciseID);

            // Fluent API - Define table relationships
            //   The custom properties in AppUser are not 'required',
            //   so that entity needs only the default Identity mappings
            builder.Entity<UserProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.UserID)
                .IsRequired()
                //.OnDelete(DeleteBehavior.Cascade);
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutGoal>()
                .HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Statistics>()
                .HasOne(s => s.User)
                .WithMany(u => u.Statistics)
                .HasForeignKey(s => s.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutSchedule>()
                .HasOne(s => s.User)
                .WithOne(u => u.Schedule)
                .HasForeignKey<WorkoutSchedule>(s => s.UserID)
                .IsRequired()
                //.OnDelete(DeleteBehavior.Cascade);
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutSession>()
                .HasOne(s => s.Schedule)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.ScheduleID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Exercise>()
                .HasOne(s => s.Session)
                .WithMany(u => u.Exercises)
                .HasForeignKey(s => s.SessionID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
