using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Filter and get all instances
        /// </summary>
        /// <returns>Filtered instances</returns>
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, string sortProperty, string sortWay);

        /// <summary>
        /// Filter and get part of data
        /// </summary>
        /// <returns>Part of filtered instances</returns>
        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int startIndex, int quantity, string sortProperty, string sortWay);

        /// <summary>
        /// Find instance by predicate
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find instance by id async
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindByIdAsync(int id);

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