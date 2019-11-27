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

        public virtual IList<T> GetAll(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IList<T> Get(Func<T, bool> predicate, int startIndex, int quantity)
        {
            return _dbSet.Where(predicate)
                         .Skip(startIndex)
                         .Take(quantity).ToList();
        }

        public virtual async Task<IList<T>> GetAsync(int startIndex, int quantity)
        {
            return await _dbSet.Skip(startIndex)
                               .Take(quantity).ToListAsync();
        }

        public virtual T FindBy(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(T item)
        {
            _dbSet.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
