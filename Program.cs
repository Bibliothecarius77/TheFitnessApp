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
using TheFitnessApp.Data;
using TheFitnessApp.Models;

namespace TheFitnessApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add database connection.
            //builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDbContext<GeneralContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<UnifiedContext>(options => options.UseSqlServer(connectionString));

            // Middleware to help detect and diagnose errors with Entity Framework Core migrations. (Can be removed.)
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<UnifiedContext>();
            //builder.Services.AddDefaultIdentity<AppUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<UnifiedContext>();
            //builder.Services.AddDefaultIdentity<AppUser>()
            //    .AddEntityFrameworkStores<UnifiedContext>()
            //    .AddDefaultTokenProviders()
            //    .AddDefaultUI();
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<UnifiedContext>();
            //builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<UnifiedContext>();
            //builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<UnifiedContext>();

            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                // Create default Identity roles ('User' and 'Admin') if they don't already exist
                UserOperations.CreateDefaultRolesAsync(roleManager).Wait();

                // Create test users (the app makers plus a guest) if they don't already exist
                UserOperations.SeedTestUsersAsync(userManager).Wait();

                // Verify that at least one user now exists.
                //UserOperations.VerifyUserAsync(userManager, roleManager, "Jacob").Wait();
            }

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
