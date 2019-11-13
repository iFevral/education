using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories.Base
{
    public class EFBaseRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ApplicationContext _db;
        private DbSet<T> _dbSet;

        public EFBaseRepository(ApplicationContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _db.SaveChanges();
        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            _db.SaveChanges();
        }
    }
}
