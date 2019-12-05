using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Store.DataAccess.Models;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get number of not removed items 
        /// </summary>
        /// <returns></returns>
        public Task<int> GetNumberOfItems();

        /// <summary>
        /// Filter and get part of data
        /// </summary>
        /// <returns>Part of filtered instances</returns>
        public Task<IEnumerable<T>> GetAllAsync(FilterModel<T> filterModel);

        /// <summary>
        /// Find instance by predicate
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

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