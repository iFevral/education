using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> FindById(int id);
        public Task<IList<T>> GetAll();
        public IList<T> GetAll(Func<T, bool> predicate);
        public Task<IList<T>> Get(int from,int quantity);
        public IList<T> Get(Func<T, bool> predicate, int from, int quantity);
        public IList<T> Find(Func<T, bool> predicate);
        public Task Create(T item);
        public Task Update(T item);
        public Task Remove(int id);
    }
}