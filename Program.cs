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
using TheFitnessApp.Data;

namespace TheFitnessApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Databas (behövs för backend-teamet, men påverkar inte din frontend)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Endast MVC-controllers och views (ingen Identity UI, ingen scaffoldad CSS)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

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
