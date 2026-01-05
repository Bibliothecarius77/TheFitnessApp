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
    public interface IRepository<T> where T : class
    {
        // CRUD - Create
        void Insert(T entity);
        Task InsertAsync(T entity);

        // CRUD - Read
        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        T? GetByID(int id);
        Task<T?> GetByIDAsync(int id);

        // CRUD - Update
        void Update(T entity);
        Task UpdateAsync(T entity);

        // CRUD - Deleete
        void Delete(int id);
        Task DeleteAsync(int id);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UnifiedContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(UnifiedContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

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

        public void Delete(int id)
        {
            var data = _dbSet.Find(id);

            if (data != null)
                _dbSet.Remove(data);

            _context.SaveChanges();
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
