using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using System.Linq.Expressions;

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

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> predicate, int startIndex, int quantity, string sortBy)
        {

            var set = _dbSet.Where(predicate)
                         .Skip(startIndex)
                         .Take(quantity)
                         .ToList();

            return set.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null)).ToList();
        }

        public virtual async Task<IList<T>> GetAsync(int startIndex, int quantity)
        {
            return await _dbSet.Skip(startIndex)
                               .Take(quantity).ToListAsync();
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> CreateAsync(T item)
        {
            _dbSet.Add(item);
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            _dbSet.Remove(item);
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }
    }
}
