using System;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T item);
        T FindById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
    }
}
