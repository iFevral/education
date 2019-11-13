using Store.DataAccess.Entities;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Authors> GetAllAuthors();
        Authors GetAuthorById(int id);
        Authors GetAuthorByName(string name);
    }
}
