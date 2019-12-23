using System.Threading.Tasks;
using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Author;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Common.Mappers.Filter;

namespace Store.BusinessLogic
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorModel> GetAllAsync(AuthorFilterModel authorFilterModel)
        {
            var authorModel = new AuthorModel();
            var filterModel = authorFilterModel.MapToEFFilterModel();

            var authors = _authorRepository.GetAll(filterModel, out int counter);

            if (authors == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorsError);
                return authorModel;
            }

            authorModel.Counter = counter;
            foreach (var author in authors)
            {
                var authorModelItem = new AuthorModelItem();
                authorModel.Items.Add(author.MapToModel());
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> FindByIdAsync(int id)
        {
            var authorModel = new AuthorModelItem();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
                return authorModel;
            }

            authorModel = author.MapToModel();
            return authorModel;
        } 

        public async Task<BaseModel> CreateAsync(AuthorModelItem authorModel)
        {
            var author = new Author();
            author = authorModel.MapToEntity(author);
            var result = await _authorRepository.CreateAsync(author);

            if(!result)
            {
                authorModel.Errors.Add(Constants.Errors.CreateAuthorError);
            }

            return authorModel;
        }

        public async Task<BaseModel> UpdateAsync(AuthorModelItem authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(authorModel.Id);
            if (author == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
                return authorModel;
            }

            author = authorModel.MapToEntity(author);
            var result = await _authorRepository.UpdateAsync(author);
            if (!result)
            {
                authorModel.Errors.Add(Constants.Errors.UpdateAuthorError);
            }

            return authorModel;
        }

        public async Task<BaseModel> DeleteAsync(int id)
        {
            var authorModel = new AuthorModelItem();

            var author = await _authorRepository.FindByIdAsync(id);

            if (author == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
                return authorModel;
            }

            author.isRemoved = true;

            var result = await _authorRepository.UpdateAsync(author);

            if (!result)
            {
                authorModel.Errors.Add(Constants.Errors.DeleteAuthorError);
            }

            return authorModel;
        }
    }
}
