using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Models;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class EFBaseRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ApplicationContext _dbContext;
        private DbSet<T> _dbSet;

        public EFBaseRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(FilterModel<T> filterModel)
        {
            //todo add itemsCount
            //todo use IQuer...
            var items = await _dbSet.Where(filterModel.Predicate).ToListAsync();

            items = filterModel.SortWay == 1
                ? items.OrderByDescending(x => x.GetType().GetProperty(filterModel.SortProperty).GetValue(x, null)).ToList()
                : items = items.OrderBy(x => x.GetType().GetProperty(filterModel.SortProperty).GetValue(x, null)).ToList();
        
            if(filterModel.Quantity > 0)
            {
                items = items.Skip(filterModel.StartIndex).Take(filterModel.Quantity).ToList();
            }

            return items;
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
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            _dbSet.Remove(item);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
