using Dapper;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class AuthorRepository : DapperBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<ListModel<Author>> GetAllAuthors(FilterModel<Author> filterModel)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM Authors
                                                  WHERE (isRemoved = 0) 
	                                              ORDER BY { filterModel.SortProperty.ToString() } { (filterModel.IsAscending ? "ASC" : "DESC") }
		                                            OFFSET { filterModel.StartIndex } ROWS");
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	FETCH NEXT { filterModel.Quantity } ROWS ONLY"));
            }

            sql.Append(new StringBuilder($@") AS a
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON a.Id = aipe.AuthorId
                                            LEFT JOIN PrintingEditions AS pe ON aipe.PrintingEditionId = pe.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                  FROM Authors
                                                  WHERE (IsRemoved = 0);");

            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString());

                var list = new ListModel<Author>();
                list.Items = new List<Author>();

                var authors = queryResult.Read<Author, AuthorInPrintingEdition, PrintingEdition, Author>(
                    (author, authorInPrintingEdition, printingEdition) =>
                    {
                        if(list.Items.Count() == 0 || list.Items.Last().Id != author.Id)
                        {
                            list.Items.AsList().Add(author);
                        }
                        

                        if(list.Items.Last().AuthorInPrintingEdition == null)
                        {
                            list.Items.Last().AuthorInPrintingEdition = new List<AuthorInPrintingEdition>();
                        }

                        if(authorInPrintingEdition == null)
                        {
                            return author;
                        }

                        authorInPrintingEdition.PrintingEdition = printingEdition;
                        authorInPrintingEdition.PrintingEditionId = printingEdition.Id;

                        list.Items.Last().AuthorInPrintingEdition.Add(authorInPrintingEdition);

                        return author;
                    },
                    splitOn: "AuthorId, Id"
               );

                list.Counter = await queryResult.ReadFirstAsync<int>();

                return list;
            }
        }
    }
}
