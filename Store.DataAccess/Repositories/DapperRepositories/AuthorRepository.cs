using Dapper;
using Microsoft.Data.SqlClient;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class AuthorRepository : DapperBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository()
        {
           
        }

        /*public override IEnumerable<Author> GetAll(FilterModel<Author> filterModel, out int counter)
        {
            counter = 10;
            Type myType = typeof(Author);
            string query = $"SELECT * FROM Authors as a LEFT JOIN AuthorInPrintingEdition as aib ON a.Id = aib.AuthorId INNER JOIN PrintingEditions as pe ON aib.PrintingEditionId = pe.Id";

            using (IDbConnection dbc = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EducationDB;Integrated Security=True"))
            {
                dbc.Open();
                var c = dbc.State;
                var a = dbc.Query<Author, AuthorInPrintingEdition, PrintingEdition, Author>(query, (author, aib, pe) =>
                {

                }).ToList();
                return a;
            }
        }*/
    }
}
