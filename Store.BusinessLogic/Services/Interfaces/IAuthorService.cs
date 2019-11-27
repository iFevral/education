using Store.BusinessLogic.Models.Authors;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        public AuthorModel GetAll(string authorNameFilter, int startIndex, int quantity);
        public Task<AuthorModel> FindById(int id);
        public Task<AuthorModel> Create(AuthorModelItem authorModelItem);
        public Task<AuthorModel> Update(int id, AuthorModelItem authorModelItem);
        public Task<AuthorModel> Delete(int id);
    }
}
