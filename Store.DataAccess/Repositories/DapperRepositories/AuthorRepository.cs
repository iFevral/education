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

        public async Task<DataModel<Author>> GetAllAuthors(FilterModel<Author> filterModel)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM Authors
                                                  WHERE (isRemoved = @IsRemoved) 
	                                              ORDER BY {filterModel.SortProperty.ToString()} {(filterModel.IsAscending ? "ASC" : "DESC")}
		                                            OFFSET @Offset ROWS");
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	FETCH NEXT @Quantity ROWS ONLY"));
            }

            sql.Append(new StringBuilder($@") AS a
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON a.Id = aipe.AuthorId
                                            LEFT JOIN PrintingEditions AS pe ON aipe.PrintingEditionId = pe.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                  FROM Authors
                                                  WHERE (IsRemoved = @IsRemoved);");

            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString(),
                    new 
                    {
                        IsRemoved = 0,
                        Offset = filterModel.StartIndex,
                        Quantity = filterModel.Quantity
                    });

                var datamodel = new DataModel<Author>();
                datamodel.Items = new List<Author>();

                var authors = queryResult.Read<Author, AuthorInPrintingEdition, PrintingEdition, Author>(
                    (author, authorInPrintingEdition, printingEdition) =>
                    {
                        if(authorInPrintingEdition == null)
                        {
                            return author;
                        }

                        author.AuthorInPrintingEdition = new List<AuthorInPrintingEdition>();
                        
                        authorInPrintingEdition.PrintingEdition = printingEdition;
                        authorInPrintingEdition.PrintingEditionId = printingEdition.Id;

                        author.AuthorInPrintingEdition.Add(authorInPrintingEdition);

                        return author;
                    },
                    splitOn: "AuthorId, Id"
               );

                datamodel.Items = authors.GroupBy(author => new { author.Id, author.Name })
                    .Select(group => new Author
                    {
                        Id = group.Key.Id,
                        Name = group.Key.Name,
                        AuthorInPrintingEdition = group.Select(author => author.AuthorInPrintingEdition.FirstOrDefault()).ToList()
                    });

                datamodel.Counter = await queryResult.ReadFirstAsync<int>();

                return datamodel;
            }
        }
    }
}
