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

namespace TheFitnessApp.Data
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        //T? GetByID(int id);
        Task<T?> GetByIDAsync(int id);
        //void Insert(T entity);
        Task InsertAsync(T entity);
        //void Delete(int id);
        Task DeleteAsync(int id);
        //void Update(T entity);
        Task UpdateAsync(T entity);
    }
}
