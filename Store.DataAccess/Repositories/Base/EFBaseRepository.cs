using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
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

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, string sortProperty, string sortWay)
        {
            var set = await _dbSet.Where(predicate).ToListAsync();

            if (sortWay == "DESC")
            {
                return set.OrderByDescending(x => x.GetType().GetProperty(sortProperty).GetValue(x, null));
            }

            return set.OrderBy(x => x.GetType().GetProperty(sortProperty).GetValue(x, null));
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int startIndex, int quantity, string sortProperty, string sortWay)
        {

            var set = await _dbSet.Where(predicate).ToListAsync();

            if(sortWay == "DESC")
            {
                return set.OrderByDescending(x => x.GetType().GetProperty(sortProperty).GetValue(x, null))
                          .Skip(startIndex)
                          .Take(quantity);
            }

            return set.OrderBy(x => x.GetType().GetProperty(sortProperty).GetValue(x, null))
                      .Skip(startIndex)
                      .Take(quantity);
        }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
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
