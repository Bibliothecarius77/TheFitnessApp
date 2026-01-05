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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    public class UnifiedContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
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
            //SeedData(optionsBuilder);
        }

        private static void ConfigureDomain(ModelBuilder builder)
        {
            // Fluent API - Map AppUser class to the standard ASP.NET table
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
            builder.Entity<UserProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutGoal>()
                .HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserID);

            builder.Entity<Statistics>()
                .HasOne(s => s.User)
                .WithMany(u => u.Statistics)
                .HasForeignKey(s => s.UserID);

            builder.Entity<WorkoutSchedule>()
                .HasOne(s => s.User)
                .WithOne(u => u.Schedule)
                .HasForeignKey<WorkoutSchedule>(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutSession>()
                .HasOne(s => s.Schedule)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.ScheduleID);

            builder.Entity<Exercise>()
                .HasOne(s => s.Session)
                .WithMany(u => u.Exercises)
                .HasForeignKey(s => s.SessionID);
        }

/*
        private void SeedData(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("<ConnectionString>").UseSeeding((context, _) =>
            {
                if (!context.Set<AppUser>().Any(x => x.UserName == "Guest"))
                {
                    context.Set<AppUser>().Add(new AppUser
                    {
                        UserName = "Guest",
                        NormalizedUserName = "GUEST",
                        Email = "guest@gmail.com",
                        NormalizedEmail = "GUEST@GMAIL.COM",
                        EmailConfirmed = true
                    });
                }

                if (!context.Set<UserProfile>().Any(x => x.Name == "Felipe"))
                {
                    context.Set<UserProfile>().Add(new UserProfile
                    {
                        Name = "Felipe"
                    });
                }
 
                context.SaveChanges();
            });



            Users.AddRange(
            [
                new Exercise
                {
                    Email = "arsalan_dx@hotmail.com",
                    NormalizedEmail = "ARSALAN_DX@HOTMAIL.COM",
                    UserName = "Arsalan",
                    NormalizedUserName = "ARSALAN",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "jacob.damm@gmail.com",
                    NormalizedEmail = "JACOB.DAMM@GMAIL.COM",
                    UserName = "Jacob",
                    NormalizedUserName = "JACOB",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "liridona.demaj@outlook.com",
                    NormalizedEmail = "LIRIDONA.DEMAJ@OUTLOOK.COM",
                    UserName = "Liridona",
                    NormalizedUserName = "LIRIDONA",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "radbergvictoria@gmail.com",
                    NormalizedEmail = "RADBERGVICTORIA@GMAIL.COM",
                    UserName = "Victoria",
                    NormalizedUserName = "VICTORIA",
                    EmailConfirmed = true
                }
            ]);

            Exercises.AddRange(
            [
                new Exercise
                {
                    Email = "arsalan_dx@hotmail.com",
                    NormalizedEmail = "ARSALAN_DX@HOTMAIL.COM",
                    UserName = "Arsalan",
                    NormalizedUserName = "ARSALAN",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "jacob.damm@gmail.com",
                    NormalizedEmail = "JACOB.DAMM@GMAIL.COM",
                    UserName = "Jacob",
                    NormalizedUserName = "JACOB",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "liridona.demaj@outlook.com",
                    NormalizedEmail = "LIRIDONA.DEMAJ@OUTLOOK.COM",
                    UserName = "Liridona",
                    NormalizedUserName = "LIRIDONA",
                    EmailConfirmed = true
                },
                new AppUser
                {
                    Email = "radbergvictoria@gmail.com",
                    NormalizedEmail = "RADBERGVICTORIA@GMAIL.COM",
                    UserName = "Victoria",
                    NormalizedUserName = "VICTORIA",
                    EmailConfirmed = true
                }
            ]);
        }
*/
    }
}
