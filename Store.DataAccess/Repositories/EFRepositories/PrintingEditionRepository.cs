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
    public class PrintingEditionRepository : EFBaseRepository<PrintingEditions>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public override IList<PrintingEditions> GetAll(Func<PrintingEditions,bool> predicate)
        {
            return _db.PrintingEditions.Where(predicate).ToList();
        }

        public override async Task<IList<PrintingEditions>> GetAllAsync()
        {
            return await _db.PrintingEditions.ToListAsync();
        }

        public override IList<PrintingEditions> Get(Func<PrintingEditions, bool> predicate, int startIndex, int quantity)
        {
            return _db.PrintingEditions.Where(predicate)
                                       .Skip(startIndex)
                                       .Take(quantity)
                                       .ToList();
        }

        public override async Task<IList<PrintingEditions>> GetAsync(int startIndex, int quantity)
        {
            return await _db.PrintingEditions.Skip(startIndex)
                                             .Take(quantity)
                                             .ToListAsync();
        }

        public void RemoveAuthors(int printingEditionId)
        {
            _db.RemoveRange(_db.AuthorInBooks.Where(aib => aib.PrintingEditionId == printingEditionId));
            _db.SaveChanges();
        }

    }
}