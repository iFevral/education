using System.Collections.Generic;

namespace Store.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Authors>
    {
        IEnumerable<Authors> GetAllAuthors();
        Authors GetAuthorById(int id);
        Authors GetAuthorByName(string name);
    }
}
