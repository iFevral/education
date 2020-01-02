using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Store.DataAccess.Repositories.Base
{
    public class DapperBaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly string _connectionString;
        public DapperBaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public virtual Task<bool> CreateAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> FindByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll(FilterModel<T> filterModel, out int counter)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> RemoveAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}
