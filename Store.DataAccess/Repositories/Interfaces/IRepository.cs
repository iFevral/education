using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Repositories
{
    public interface  IRepository<T> 
        where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(int id);

    }
}
