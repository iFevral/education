using Dapper;
using Microsoft.Data.SqlClient;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Store.DataAccess.Repositories.Base
{
    public class DapperBaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public Task<bool> CreateAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> FindByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll(FilterModel<T> filterModel, out int counter)
        {
            counter = 10;
            Type myType = typeof(T);
            using (IDbConnection dbc = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EducationDB;Integrated Security=True"))
            {
                dbc.Open();
                var c = dbc.State;
                var a = dbc.Query<T>($"SELECT * FROM Authors").ToList();
                return a;
            }
        }

        public Task<bool> RemoveAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}
