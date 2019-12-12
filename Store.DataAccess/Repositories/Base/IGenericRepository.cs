using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Store.DataAccess.Models.EFFilters;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Filter and get part of data
        /// </summary>
        /// <returns>Part of filtered instances</returns>
        public IEnumerable<T> GetAll(FilterModel<T> filterModel, out int counter);

        /// <summary>
        /// Find instance by predicate
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find instance by id async
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindByIdAsync(long id);

        /// <summary>
        /// Create instance async
        /// </summary>
        public Task<bool> CreateAsync(T item);

        /// <summary>
        /// Update instance async
        /// </summary>
        public Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Remove instance async
        /// </summary>
        public Task<bool> RemoveAsync(T item);
    }
}