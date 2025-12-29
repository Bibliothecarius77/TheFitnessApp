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
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GeneralContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(GeneralContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T? GetByID(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChangesAsync();
        }

        public async Task InsertAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var data = _dbSet.Find(id);

            if (data != null)
                _dbSet.Remove(data);

            _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbSet.FindAsync(id);

            if (data != null)
                _dbSet.Remove(data);

            await _context.SaveChangesAsync();
        }
    }
}
