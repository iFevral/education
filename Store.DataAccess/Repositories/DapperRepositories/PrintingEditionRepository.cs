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

        public async Task<DataModel<PrintingEdition>> GetAllPrintingEditions(PrintingEditionFilterModel filterModel)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM PrintingEditions
	                                              WHERE (Price BETWEEN @MinPrice AND @MaxPrice) AND
                                                        (IsRemoved = @IsRemoved) AND 
			                                            (Type IN @Types) AND
			                                            (Title LIKE CONCAT('%',@SearchQuery,'%') OR 
			                                             Id IN (SELECT PrintingEditionId
					                                            FROM AuthorInPrintingEdition
					                                            WHERE AuthorId IN (SELECT Id
								  		                                           FROM Authors
										                                           WHERE Name LIKE CONCAT('%',@SearchQuery,'%'))))
		                                          ORDER BY {filterModel.SortProperty.ToString()} { (filterModel.IsAscending ? "ASC" : "DESC") }
			                                            OFFSET @Offset ROWS");
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	    FETCH NEXT @Quantity ROWS ONLY"));
            }

            sql.Append(new StringBuilder($@") AS pe
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON pe.Id = aipe.PrintingEditionId
                                            LEFT JOIN Authors AS a ON aipe.AuthorId = a.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                FROM (SELECT *
	                                                  FROM PrintingEditions
	                                                  WHERE (Price BETWEEN @MinPrice AND @MaxPrice) AND
                                                            (IsRemoved = @IsRemoved) AND 
			                                                (Type IN @Types) AND
			                                                (Title LIKE CONCAT('%',@SearchQuery,'%') OR 
			                                                 Id IN (SELECT PrintingEditionId
					                                                FROM AuthorInPrintingEdition
					                                                WHERE AuthorId IN (SELECT Id
								  		                                               FROM Authors
										                                               WHERE Name LIKE CONCAT('%',@SearchQuery,'%'))))) AS pe;");
        
            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    MinPrice = filterModel.MinPrice,
                    MaxPrice = filterModel.MaxPrice,
                    IsRemoved = 0,
                    Types = filterModel.Types.ToArray(),
                    SearchQuery = filterModel.SearchQuery,
                    Offset = filterModel.StartIndex,
                    Quantity = filterModel.Quantity
                };

                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString(), parameters);
                    

                var dataModel = new DataModel<PrintingEdition>();

                var printingEditions = queryResult.Read<PrintingEdition, AuthorInPrintingEdition, Author, PrintingEdition>(
                    (printingEdition, authorInPrintingEdition, author) =>
                    {
                        printingEdition.AuthorInPrintingEditions = new List<AuthorInPrintingEdition>();

                        if (authorInPrintingEdition == null)
                        {
                            return printingEdition;
                        }

                        authorInPrintingEdition.Author = author;
                        authorInPrintingEdition.AuthorId = author.Id;

                        printingEdition.AuthorInPrintingEditions.Add(authorInPrintingEdition);

                        return printingEdition;
                    },
                    splitOn: "AuthorId, Id"
               );


                dataModel.Items = printingEditions.GroupBy(printingEdition =>
                    new
                    {
                        printingEdition.Id,
                        printingEdition.Title,
                        printingEdition.Image,
                        printingEdition.Price,
                        printingEdition.Type,
                        printingEdition.Description,
                        printingEdition.Currency,
                        printingEdition.Date
                    }
                )
                    .Select(group => new PrintingEdition
                    {
                        Id = group.Key.Id,
                        Date = group.Key.Date,
                        Type = group.Key.Type,
                        Image = group.Key.Image,
                        Title = group.Key.Title,
                        Price = group.Key.Price,
                        Currency = group.Key.Currency,
                        Description = group.Key.Description,
                        AuthorInPrintingEditions = group.Select(printingEdition => printingEdition.AuthorInPrintingEditions.FirstOrDefault()).ToList()
                    });

                dataModel.Counter = await queryResult.ReadFirstAsync<int>();

                return dataModel;
            }
        }

        public override async Task<PrintingEdition> FindByIdAsync(long id)
        {
            var sql = new StringBuilder($@" SELECT *
                                            FROM (SELECT *
	                                              FROM PrintingEditions
	                                              WHERE (Id = @Id) AND (IsRemoved = 0)) AS pe
                                            LEFT JOIN AuthorInPrintingEdition AS aipe ON pe.Id = aipe.PrintingEditionId
                                            LEFT JOIN Authors AS a ON aipe.AuthorId = a.Id;");


            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                PrintingEdition item = null;
                var queryResult = await databaseConnection.QueryAsync<PrintingEdition, AuthorInPrintingEdition, Author, PrintingEdition>(sql.ToString(),
                    param: new {Id = id},
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
