using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Filters;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> GetAllAsync(AuthorFilterModel authorFilter);
        public Task<AuthorModelItem> FindByIdAsync(int id);
        public Task<BaseModel> CreateAsync(AuthorModelItem authorModelItem);
        public Task<BaseModel> UpdateAsync(AuthorModelItem authorModelItem);
        public Task<BaseModel> DeleteAsync(int id);
    }
}
