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

namespace TheFitnessApp.Data
{
    // 📌 Generic repository interface med Guid som ID
    public interface IRepository<T> where T : class
    {
        // CREATE
        void Insert(T entity);
        Task InsertAsync(T entity);

        // READ
        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        T? GetByID(Guid id);
        Task<T?> GetByIDAsync(Guid id);

        // UPDATE
        void Update(T entity);
        Task UpdateAsync(T entity);

        // DELETE
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }

    // 📌 Generic repository implementation
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UnifiedContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(UnifiedContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // CREATE
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // READ
        public IEnumerable<T> Get()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T? GetByID(Guid id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIDAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // UPDATE
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);

            _context.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
