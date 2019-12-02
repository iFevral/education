using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Filter and get all instances
        /// </summary>
        /// <returns>Filtered instances</returns>
        public IList<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all instances async
        /// </summary>
        /// <returns>All instances</returns>
        public Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Filter and get part of data
        /// </summary>
        /// <returns>Part of filtered instances</returns>
        public IList<T> Get(Expression<Func<T, bool>> predicate, int startIndex, int quantity, string sortBy = "Id");

        /// <summary>
        /// Get part of data async
        /// </summary>
        /// <returns>Part of instances</returns>
        public Task<IList<T>> GetAsync(int startIndex, int quantity);

        /// <summary>
        /// Find instance by predicate
        /// </summary>
        /// <returns>First found instance</returns>
        public T FindBy(Expression<Func<T, bool>> predicate);

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