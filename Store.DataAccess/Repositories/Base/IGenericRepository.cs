using System.Threading.Tasks;
using System.Collections.Generic;
using Store.DataAccess.Models.Filters;

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
        /// Find instance by id async
        /// </summary>
        /// <returns>First found instance</returns>
        public Task<T> FindByIdAsync(long id);

        /// <summary>
        /// Create instance async
        /// </summary>
        public Task<bool> CreateAsync(T item);

        /// <summary>
        /// Create instances async
        /// </summary>
        public Task<bool> CreateListAsync(IEnumerable<T> items);
        
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