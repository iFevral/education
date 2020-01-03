using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class AuthorInPrintingEditionRepository : DapperBaseRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository
    {
        public AuthorInPrintingEditionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> RemoveByPrintingEditionAsync(long printingEditionId)
        {
            var sql = new StringBuilder($@"DELETE FROM AuthorInPrintingEdition WHERE PrintingEditionId = @PrintingEditionId");
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                await databaseConnection.QueryAsync(sql.ToString(), new { PrintingEditionId = printingEditionId });
                return true;
            }
        }
    }
}
