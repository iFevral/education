using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : EFBaseRepository<Authors>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public override IList<Authors> GetAll(Func<Authors, bool> predicate)
        {
            return _db.Authors.Where(predicate).ToList();
        }

        public override async Task<IList<Authors>> GetAllAsync()
        {
            return await _db.Authors.ToListAsync();
        }

        public override IList<Authors> Get(Func<Authors, bool> predicate, int startIndex, int quantity)
        {
            return _db.Authors.Where(predicate)
                                       .Skip(startIndex)
                                       .Take(quantity)
                                       .ToList();
        }

        public override async Task<IList<Authors>> GetAsync(int startIndex, int quantity)
        {
            return await _db.Authors.Skip(startIndex)
                                             .Take(quantity)
                                             .ToListAsync();
        }

        public void RemovePrintingEditions(int authorId)
        {
            _db.RemoveRange(_db.AuthorInBooks.Where(aib => aib.AuthorId == authorId));
            _db.SaveChanges();
        }
    }
}
