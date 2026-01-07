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
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add database connection with options, including seeding data
            builder.Services.AddDbContext<UnifiedContext>(options =>
            {
                options.UseSqlServer(connectionString);

                options.UseSeeding((context, _) =>
                {
                    // Create ASP.NET Identity Roles
                    UserOperations.CreateDefaultRolesAsync((UnifiedContext)context, CancellationToken.None).Wait();

                    // Seed app users (both admins and regular users) plus some fitness data
                    UserOperations.SeedTestUsersAsync((UnifiedContext)context, CancellationToken.None).Wait();
                });

                options.UseAsyncSeeding(async (context, _hasSchema, _cancellationToken) =>
                {
                    // Create ASP.NET Identity Roles
                    await UserOperations.CreateDefaultRolesAsync((UnifiedContext)context, _cancellationToken);

                    // Seed app users (both admins and regular users)
                    await UserOperations.SeedTestUsersAsync((UnifiedContext)context, _cancellationToken);
                });
            });

            // Middleware to help detect and diagnose errors with Entity Framework Core migrations (can be removed)
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<UnifiedContext>();

            // Register services
            builder.Services.AddScoped<IUserAggregateService, UserAggregateService>();

            // Endast MVC-controllers och views (ingen Identity UI, ingen scaffoldad CSS)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();

                // Automatically apply any pending database migrations
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<UnifiedContext>();
                await context.Database.MigrateAsync();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            // ROUTING
            // Steg 1 – Index är startsidan
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Steg 2 – Welcome (dashboard)
            app.MapControllerRoute(
                name: "welcome",
                pattern: "welcome",
                defaults: new { controller = "Home", action = "Welcome" });

            // Ingen Razor Pages här – du jobbar bara med Views
            app.Run();
        }
    }
}
