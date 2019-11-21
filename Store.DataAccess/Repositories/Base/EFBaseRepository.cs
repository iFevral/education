using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class EFBaseRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ApplicationContext _db;
        private DbSet<T> _dbSet;

        public EFBaseRepository(ApplicationContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual IList<T> GetAll(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                         .Where(predicate).ToList();
        }

        public virtual async Task<IList<T>> Get(int from, int quantity)
        {
            return await _dbSet.AsNoTracking()
                               .Skip(from)
                               .Take(quantity).ToListAsync();
        }

        public virtual IList<T> Get(Func<T, bool> predicate, int from, int quantity)
        {
            return _dbSet.AsNoTracking()
                         .Skip(from)
                         .Take(quantity)
                         .Where(predicate).ToList();
        }

        public virtual IList<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                         .Where(predicate).ToList();
        }

        public virtual async Task<T> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public virtual async Task Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public virtual async Task Remove(int id)
        {
            _dbSet.Remove(await FindById(id));
            await _db.SaveChangesAsync();
        }
    }
}
