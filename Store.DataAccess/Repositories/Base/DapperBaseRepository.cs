using Dapper.Contrib.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Store.DataAccess.Repositories.Base
{
    public class DapperBaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly string _connectionString;
        public DapperBaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public virtual async Task<long> CreateAsync(T item)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                int id = await databaseConnection.InsertAsync<T>(item);
                return id;
            }
        }

        public async Task<bool> CreateListAsync(IEnumerable<T> items)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                await databaseConnection.InsertAsync(items);
                return true;
            }
        }
    
        public virtual async Task<T> FindByIdAsync(long id)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var item = await databaseConnection.GetAsync<T>(id);
                return item;
            }
        }

        public virtual IEnumerable<T> GetAll(FilterModel<T> filterModel, out int counter)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var items = databaseConnection.GetAll<T>();
                counter = items.Count();
                return items;
            }
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var result = await databaseConnection.DeleteAsync<T>(item);
                return result;
            }
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var result = await databaseConnection.UpdateAsync<T>(item);
                return result;
            }
        }
    }
}
