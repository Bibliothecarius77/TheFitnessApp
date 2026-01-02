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

using System.Security.Claims;
using TheFitnessApp.Models;

namespace TheFitnessApp.Data
{
    public interface IUserService
    {
        Task<AppUser?> GetCurrentUserAsync(ClaimsPrincipal principal);
    }
}
