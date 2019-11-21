using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PrintingEditionRepository : EFBaseRepository<PrintingEditions>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<IList<PrintingEditions>> GetAll()
        {
            return await _db.PrintingEditions
                                .Include(pe => pe.AuthorInBooks)
                                    .ThenInclude(aib => aib.Author).ToListAsync();
        }

        public override IList<PrintingEditions> GetAll(Func<PrintingEditions,bool> predicate)
        {
            return _db.PrintingEditions
                                .Include(pe => pe.AuthorInBooks)
                                    .ThenInclude(aib => aib.Author).Where(predicate).ToList();
        }
    }
}