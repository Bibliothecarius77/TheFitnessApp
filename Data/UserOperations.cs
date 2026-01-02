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
        public static async Task CreateDefaultRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = Enum.GetNames<UserRole>();

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        public static async Task SeedTestUsersAsync(UserManager<AppUser> userManager)
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

            foreach (AppUser user in crew)
            {
                if (!userManager.Users.Any(u => u.UserName == user.UserName))
                {
                    Console.WriteLine("UserOperations.SeedTestUsersAsync() --> Creating test user " + user.UserName);
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    await userManager.CreateAsync(user, "secret");
                    //Console.WriteLine("UserOperations.SeedTestUsersAsync() --> Adding user " + user.UserName + " to Identity role \"Admin\"");
                    //await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            AppUser guest = new AppUser
            {
                Email = "guest@gmail.com",
                NormalizedEmail = "GUEST@GMAIL.COM",
                UserName = "Guest",
                NormalizedUserName = "GUEST",
                EmailConfirmed = true
            };

            if (!userManager.Users.Any(u => u.UserName == guest.UserName))
            {
                Console.WriteLine("UserOperations.SeedTestUsersAsync() --> Creating test user " + guest.UserName);
                guest.SecurityStamp = Guid.NewGuid().ToString();
                await userManager.CreateAsync(guest, "password");
                Console.WriteLine("UserOperations.SeedTestUsersAsync() --> Adding user " + guest.UserName + " to Identity role \"User\"");
                await userManager.AddToRoleAsync(guest, "User");
            }
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
    }
}
