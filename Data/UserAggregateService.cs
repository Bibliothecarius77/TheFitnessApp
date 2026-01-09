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

using Microsoft.EntityFrameworkCore;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    public interface IUserAggregateService
    {
        Task<AppUser?> GetAsync(Guid userId);
    }

    public class UserAggregateService : IUserAggregateService
    {
        private readonly UnifiedContext _context;

        public UserAggregateService(UnifiedContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Goals)
                .Include(u => u.Statistics)
                .Include(u => u.Schedule)
                .ThenInclude(s => s.Sessions)
                .ThenInclude(s => s.Exercises)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}
