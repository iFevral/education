using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;
using Store.DataAccess.Extensions.Sorting;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class EFBaseRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected readonly ApplicationContext _dbContext;
        protected DbSet<T> _dbSet;

        public EFBaseRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<int> GetNumberOfItems()
        {
            var counter = await _dbSet.Where(x => !x.isRemoved).CountAsync();
            return counter;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(FilterModel<T> filterModel)
        {
            var items = _dbSet.Where(filterModel.Predicate)
                               .AsEnumerable()
                               .SortBy(filterModel.SortProperty.ToString(), filterModel.IsAscending);

            if(filterModel.Quantity > 0)
            {
                items =  items.Skip(filterModel.StartIndex).Take(filterModel.Quantity);
            }

            return items.ToList();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var item = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            return item;
        }

        public virtual async Task<T> FindByIdAsync(long id)
        {
            var item = await _dbSet.FindAsync(id);
            return item;
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
