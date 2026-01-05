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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    // This helper class is used for "ASP.NET Identity operations" (operations
    // on users and their roles).
    // All methods herein have to be static, to not interfere with dependency
    // injection (specifically the service lifetime of scoped services like
    // UserManager, RoleManager, et al).
    public static class UserOperations
    {
        // Create ASP.NET Identity Roles based on the UserRole Enum
        public static async Task CreateDefaultRolesAsync(UnifiedContext context, CancellationToken cnToken)
        {
            IdentityResult result;

            // Retrieve service
            RoleManager<IdentityRole<Guid>> roleManager = context.GetService<RoleManager<IdentityRole<Guid>>>();

            // Create roles, if they don't already exist
            foreach (string role in Enum.GetNames<UserRole>())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    result = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    if (!result.Succeeded)
                        throw new Exception("Unable to create new IdentityRole '" + role.ToString() + "'.");
                }
            }
            await context.SaveChangesAsync(cnToken);
        }

        // Seed app users (regular users as well as admins)
        public static async Task SeedTestUsersAsync(UnifiedContext context, CancellationToken cnToken)
        {
            IdentityResult result, resultTwo;

            // Retrieve service
            UserManager<AppUser> userManager = context.GetService<UserManager<AppUser>>();

            // Seed test data for admin users
            AppUser[] admins = await GenerateTestAdminsAsync();

            // Create admin users, if they don't already exist
            foreach (AppUser user in admins)
            {
                if (!await userManager.Users.AnyAsync(u => u.UserName == user.UserName, cnToken))
                {
                    result = await userManager.CreateAsync(user, "Secret!123");

                    if (result.Succeeded)
                    {
                        resultTwo = await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

                        if (!resultTwo.Succeeded)
                            throw new Exception("Unable to add user " + user.UserName + " to IdentityRole '" + UserRole.Admin.ToString() + "'.");
                    }
                    else
                    {
                        string strError = "";
                        foreach (IdentityError error in result.Errors)
                            strError += error.Description + error.Code + "\n";

                        throw new Exception("Unable to create new user '" + user.UserName + "'.\n" + strError);
                    }
                }
            }
            await context.SaveChangesAsync(cnToken);

            // Seed test data for regular users
            AppUser[] users = await GenerateTestUsersAsync();

            // Create regular users (if they don't already exist) with some attached fitness data
            foreach (AppUser user in users)
            {
                if (!await userManager.Users.AnyAsync(u => u.UserName == user.UserName, cnToken))
                {
                    result = await userManager.CreateAsync(user, "Password#321");

                    if (result.Succeeded)
                    {
                        resultTwo = await userManager.AddToRoleAsync(user, UserRole.User.ToString());

                        //if (!resultTwo.Succeeded)
                        //    throw new Exception("Unable to add user " + user.UserName + " to IdentityRole '" + UserRole.User.ToString() + "'.");

                        if (resultTwo.Succeeded)
                        {
                            // Seed app users (both admins and regular users)
                            await SeedFitnessData(user, context, cnToken);
                        }
                        else
                            throw new Exception("Unable to add user " + user.UserName + " to IdentityRole '" + UserRole.User.ToString() + "'.");
                    }
                    else
                    {
                        string strError = "";
                        foreach (IdentityError error in result.Errors)
                            strError += error.Description + error.Code + "\n";

                        throw new Exception("Unable to create new user '" + user.UserName + "'.\n" + strError);
                    }
                }
            }
            await context.SaveChangesAsync(cnToken);
        }

        public static async Task SeedFitnessData(AppUser user, UnifiedContext context, CancellationToken cnToken)
        {
            UserProfile profile = new()
            {
                User = user,
                UserID = user.Id,
                FirstName = "Jane",
                LastName = "Doe",
                AddrStreet = "Main Street",
                AddrNumber = 1,
                AddrZip = "90510",
                AddrCity = "Beverly Hills",
                AddrCountry = "USA",
                DateOfBirth = new DateTime(1999, 2, 14),
                HeightCM = 169,
                WeightKG = 62f
            };

            WorkoutGoal goal = new()
            {
                User = user,
                UserID = user.Id,
                Type = GoalType.WeightLoss,
                TargetValue = 20,
                TargetDate = new DateTime(2026, 2, 14),
                IsCompleted = false
            };

            Statistics stats = new()
            {
                User = user,
                UserID = user.Id,
                PeriodStart = new DateTime(2026, 1, 1),
                PeriodEnd = new DateTime(2026, 1, 5),
                TotalWorkouts = 3,
                TotalDurationMin = 150,
                TotalCaloriesBurnt = 5400
            };

            WorkoutSchedule schedule = new()
            {
                User = user,
                UserID = user.Id,
                StartDate = new DateTime(2026, 1, 1),
                EndDate = new DateTime(2026, 4, 30),
                Notes = "Let's get in shape!"
            };

            WorkoutSession sessionOne = new()
            {
                Schedule = schedule,
                ScheduleID = schedule.ScheduleID,
                StartTime = new DateTime(2026, 1, 2, 9, 0, 0),
                EndTime = new DateTime(2026, 1, 2, 9, 45, 0),
                TotalCalories = 2100
            };

            Exercise exerOne = new()
            {
                Session = sessionOne,
                SessionID = sessionOne.SessionID,
                Type = ExerciseType.Cardio,
                Category = "Spinning",
                CaloriesBurnt = 2100
            };

            WorkoutSession sessionTwo = new()
            {
                Schedule = schedule,
                ScheduleID = schedule.ScheduleID,
                StartTime = new DateTime(2026, 1, 4, 9, 0, 0),
                EndTime = new DateTime(2026, 1, 4, 10, 0, 0),
                TotalCalories = 2100
            };

            Exercise exerTwo = new()
            {
                Session = sessionTwo,
                SessionID = sessionTwo.SessionID,
                Type = ExerciseType.Strength,
                Category = "Torso",
                Sets = 10,
                Reps = 15,
                WeightKG = 45,
                CaloriesBurnt = 1200
            };

            WorkoutSession sessionThree = new()
            {
                Schedule = schedule,
                ScheduleID = schedule.ScheduleID,
                StartTime = new DateTime(2026, 1, 5, 15, 30, 0),
                EndTime = new DateTime(2026, 1, 5, 16, 15, 0),
                TotalCalories = 2100
            };

            Exercise exerThree = new()
            {
                Session = sessionThree,
                SessionID = sessionThree.SessionID,
                Type = ExerciseType.Cardio,
                Category = "Spinning",
                CaloriesBurnt = 2100
            };

            await context.Set<UserProfile>().AddAsync(profile, cnToken);
            await context.Set<WorkoutGoal>().AddAsync(goal, cnToken);
            await context.Set<Statistics>().AddAsync(stats, cnToken);
            await context.Set<WorkoutSchedule>().AddAsync(schedule, cnToken);
            await context.Set<WorkoutSession>().AddAsync(sessionOne, cnToken);
            await context.Set<WorkoutSession>().AddAsync(sessionTwo, cnToken);
            await context.Set<WorkoutSession>().AddAsync(sessionThree, cnToken);
            await context.Set<Exercise>().AddAsync(exerOne, cnToken);
            await context.Set<Exercise>().AddAsync(exerTwo, cnToken);
            await context.Set<Exercise>().AddAsync(exerThree, cnToken);

            await context.SaveChangesAsync(cnToken);
        }

        public static async Task<bool> VerifyUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, AppUser user)
        {
            if (userManager != null && user != null && !String.IsNullOrEmpty(user.UserName))
                return await VerifyUserAsync(userManager, roleManager, user.UserName);
            else
                return false;
        }

        public static async Task<bool> VerifyUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, string userName)
        {
            if(userManager != null && !String.IsNullOrEmpty(userName))
            {
                AppUser? existingUser = await userManager.FindByNameAsync(userName);

                if (existingUser != null)
                {
                    // Search among all registered Identity roles, if available.
                    if (roleManager != null)
                    {
                        IdentityRole<Guid>[] listRoles = roleManager.Roles.ToArray();

                        foreach(IdentityRole<Guid> role in listRoles)
                        {
                            if(await userManager.IsInRoleAsync(existingUser, role.ToString()))
                            {
                                Console.WriteLine("UserOperations.VerifyUserAsync() --> User \"" + existingUser.UserName + "\" does exist in the role of '" + role.ToString() + "'.");
                                return true;
                            }
                        }

                        Console.WriteLine("UserOperations.VerifyUserAsync() --> User \"" + existingUser.UserName + "\" does exist, but has no registered Identity role.");
                        return true;
                    }
                    // No registered Identity roles - try this app's "standard roles" instead.
                    else
                    {
                        string[] arrRoles = Enum.GetNames<UserRole>();

                        foreach (string roleName in arrRoles)
                        {
                            if (await userManager.IsInRoleAsync(existingUser, roleName))
                            {
                                Console.WriteLine("UserOperations.VerifyUserAsync() --> User \"" + existingUser.UserName + "\" does exist in the role of '" + roleName + "'.");
                                return true;
                            }
                        }

                        Console.WriteLine("UserOperations.VerifyUserAsync() --> User \"" + existingUser.UserName + "\" does exist, but has no registered Identity role.");
                        return true;
                    }
                }
            }

            Console.WriteLine("UserOperations.VerifyUserAsync() --> User \"" + userName + "\" does NOT exist.");
            return false;
        }

        // Seed test data for admin users
        public static async Task<AppUser[]> GenerateTestAdminsAsync()
        {
            AppUser[] crew =
            [
                new AppUser
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
            ];

            return crew;
        }

        // Seed test data for regular users
        public static async Task<AppUser[]> GenerateTestUsersAsync()
        {
            AppUser[] users =
            [
               new AppUser
                {
                    Email = "guest@gmail.com",
                    NormalizedEmail = "GUEST@GMAIL.COM",
                    UserName = "Guest",
                    NormalizedUserName = "GUEST",
                    EmailConfirmed = true
                }
            ];

            return users;
        }
    }
}
