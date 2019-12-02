using System.Threading.Tasks;
using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Interface;

namespace Store.BusinessLogic
{
    public class AuthorService : IAuthorService
    {
        private IMapper<Authors, AuthorModelItem> _mapper;
        private IAuthorRepository _authorRepository;

        public AuthorService(IMapper<Authors, AuthorModelItem> mapper,
                             IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorModel> GetAll(AuthorFilter authorFilter, int startIndex = -1, int quantity = -1)
        {
            var authorModel = new AuthorModel();
            IList<Authors> authors;
            
            if (startIndex != -1 && quantity != -1)
            {
                authors = _authorRepository.Get(authorFilter.Predicate, startIndex, quantity);
            }
            else
            {
                authors = _authorRepository.GetAll(authorFilter.Predicate);
            }

            if (authors.Count == 0)
            {
                authorModel.Errors.Add($"Author not found");
                return authorModel;
            }

            foreach (var author in authors)
            {
                var a = new AuthorModelItem();
                a = _mapper.Map(author, a);
                authorModel.Authors.Add(a);
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> FindByIdAsync(int id)
        {
            var authorModel = new AuthorModelItem();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            authorModel = _mapper.Map(author, authorModel);
            return authorModel;
        }

        public async Task<AuthorModelItem> CreateAsync(AuthorModelItem authorItem)
        {
            var author = new Authors();
            author = _mapper.Map(authorItem, author);
            var result = await _authorRepository.CreateAsync(author);
            var authorModel = new AuthorModelItem();
            if(!result)
            {
                authorModel.Errors.Add("Creating author error");
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> UpdateAsync(int id, AuthorModelItem authorItem)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            var authorModel = new AuthorModelItem();
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            author = _mapper.Map(authorItem, author);
            var result = await _authorRepository.UpdateAsync(author);
            if (!result)
            {
                authorModel.Errors.Add("Updating author error");
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> DeleteAsync(int id)
        {
            var authorModel = new AuthorModelItem();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            author.isRemoved = true;
            var result = await _authorRepository.UpdateAsync(author);
            if (!result)
            {
                authorModel.Errors.Add("Deleting author error");
            }

            return authorModel;
        }
    }
}
