/*using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationContext _db;

        public AuthorRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<Authors> GetAllAuthors()
        {
            return _db.Authors.AsEnumerable();
        }

        public Authors GetAuthorById(int id)
        {
            return _db.Authors.Find(id);
        }

        public Authors GetAuthorByName(string name)
        {
            return _db.Authors.FirstOrDefault(a => a.Name.Equals(name));
        }

        public void Create(Authors author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }
        public void Update(Authors author)
        {
            _db.Entry(author).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Authors author = _db.Authors.Find(id);
            if (author != null)
            {
                _db.Authors.Remove(author);
                _db.SaveChanges();
            }
        }
    }
}
*/