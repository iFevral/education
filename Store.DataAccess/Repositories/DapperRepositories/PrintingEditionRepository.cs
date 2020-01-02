using Dapper;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class PrintingEditionRepository : DapperBaseRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<ListModel<PrintingEdition>> GetAllPrintingEditions(PrintingEditionFilterModel filterModel)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM PrintingEditions
	                                              WHERE (Price BETWEEN { filterModel.MinPrice } AND { filterModel.MaxPrice }) AND
                                                        (IsRemoved = 0) AND 
			                                            (Type IN ({ string.Join(",", filterModel.Types) })) AND
			                                            (Title LIKE '%{ filterModel.SearchQuery }%' OR 
			                                             Id IN (SELECT PrintingEditionId
					                                            FROM AuthorInPrintingEdition
					                                            WHERE AuthorId IN (SELECT Id
								  		                                           FROM Authors
										                                           WHERE Name LIKE '%{ filterModel.SearchQuery }%')))
		                                          ORDER BY { filterModel.SortProperty.ToString() } { (filterModel.IsAscending ? "ASC" : "DESC") }
			                                            OFFSET { filterModel.StartIndex } ROWS");
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	    FETCH NEXT { filterModel.Quantity } ROWS ONLY"));
            }

            sql.Append(new StringBuilder($@") AS pe
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON pe.Id = aipe.PrintingEditionId
                                            LEFT JOIN Authors AS a ON aipe.AuthorId = a.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                FROM (SELECT *
	                                                  FROM PrintingEditions
	                                                  WHERE (Price BETWEEN { filterModel.MinPrice } AND { filterModel.MaxPrice }) AND
                                                            (IsRemoved = 0) AND 
			                                                (Type IN ({ string.Join(",", filterModel.Types) })) AND
			                                                (Title LIKE '%{ filterModel.SearchQuery }%' OR 
			                                                 Id IN (SELECT PrintingEditionId
					                                                FROM AuthorInPrintingEdition
					                                                WHERE AuthorId IN (SELECT Id
								  		                                               FROM Authors
										                                               WHERE Name LIKE '%{ filterModel.SearchQuery }%')))) AS pe;");
        
            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString());

                var list = new ListModel<PrintingEdition>();
                list.Items = new List<PrintingEdition>();

                var authors = queryResult.Read<PrintingEdition, AuthorInPrintingEdition, Author, PrintingEdition>(
                    (printingEdition, authorInPrintingEdition, author) =>
                    {
                        if (list.Items.Count() == 0 || list.Items.Last().Id != printingEdition.Id)
                        {
                            list.Items.AsList().Add(printingEdition);
                        }

                        if (list.Items.Last().AuthorInPrintingEditions == null)
                        {
                            list.Items.Last().AuthorInPrintingEditions = new List<AuthorInPrintingEdition>();
                        }

                        if (authorInPrintingEdition == null)
                        {
                            return printingEdition;
                        }

                        authorInPrintingEdition.Author = author;
                        authorInPrintingEdition.AuthorId = author.Id;

                        list.Items.Last().AuthorInPrintingEditions.Add(authorInPrintingEdition);

                        return printingEdition;
                    },
                    splitOn: "AuthorId, Id"
               );

                list.Counter = await queryResult.ReadFirstAsync<int>();

                return list;
            }
        }

        public override async Task<PrintingEdition> FindByIdAsync(long id)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM PrintingEditions
	                                              WHERE (Id = {id}) AND (IsRemoved = 0)) AS pe
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON pe.Id = aipe.PrintingEditionId
                                            LEFT JOIN Authors AS a ON aipe.AuthorId = a.Id;");


            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                PrintingEdition item = null;
                var queryResult = await databaseConnection.QueryAsync<PrintingEdition, AuthorInPrintingEdition, Author, PrintingEdition>(sql.ToString(),
                    map: (printingEdition, authorInPrintingEdition, author) =>
                    {
                        if (item == null)
                        {
                            item = printingEdition;
                        }

                        if (item.AuthorInPrintingEditions == null)
                        {
                            item.AuthorInPrintingEditions = new List<AuthorInPrintingEdition>();
                        }

                        if (authorInPrintingEdition == null)
                        {
                            return printingEdition;
                        }

                        authorInPrintingEdition.Author = author;
                        authorInPrintingEdition.AuthorId = author.Id;

                        item.AuthorInPrintingEditions.Add(authorInPrintingEdition);

                        return printingEdition;
                    },
                    splitOn: "AuthorId, Id"
                    );

                return item;
            }
        }
    }
}
