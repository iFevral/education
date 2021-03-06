﻿using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEdition>
    {
        public Task<DataModel<PrintingEdition>> GetAllPrintingEditions(PrintingEditionFilterModel filterModel);
    }
}