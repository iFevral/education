using Store.BusinessLogic.Models.Authors;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> GetAll(AuthorFilter authorFilter);
        public Task<AuthorModelItem> FindByIdAsync(int id);
        public Task<AuthorModelItem> CreateAsync(AuthorModelItem authorModelItem);
        public Task<AuthorModelItem> UpdateAsync(int id, AuthorModelItem authorModelItem);
        public Task<AuthorModelItem> DeleteAsync(int id);
    }
}
