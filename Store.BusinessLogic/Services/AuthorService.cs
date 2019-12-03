﻿using System.Threading.Tasks;
using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Common;

namespace Store.BusinessLogic
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper<Authors, AuthorModelItem> _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IMapper<Authors, AuthorModelItem> mapper,
                             IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorModel> GetAll(AuthorFilter authorFilter)
        {
            var authorModel = new AuthorModel();
            IEnumerable<Authors> authors;
            
            if (authorFilter.Quantity != 0)
            {
                authors = await _authorRepository.GetAsync(authorFilter.Predicate,
                                                           authorFilter.StartIndex,
                                                           authorFilter.Quantity, 
                                                           authorFilter.SortProperty, 
                                                           authorFilter.SortWay);
            }
            else
            {
                authors = await _authorRepository.GetAllAsync(authorFilter.Predicate,
                                                              authorFilter.SortProperty,
                                                              authorFilter.SortWay);
            }

            if (authors == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
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
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
                return authorModel;
            }

            authorModel = _mapper.Map(author, authorModel);
            return authorModel;
        }

        public async Task<AuthorModelItem> CreateAsync(AuthorModelItem authorModel)
        {
            var author = new Authors();
            author = _mapper.Map(authorModel, author);
            var result = await _authorRepository.CreateAsync(author);
            if(!result)
            {
                authorModel.Errors.Add(Constants.Errors.CreateAuthorError);
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> UpdateAsync(int id, AuthorModelItem authorModel)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add(Constants.Errors.NotFoundAuthorError);
                return authorModel;
            }

            author = _mapper.Map(authorModel, author);
            var result = await _authorRepository.UpdateAsync(author);
            if (!result)
            {
                authorModel.Errors.Add(Constants.Errors.UpdateAuthorError);
            }

            return authorModel;
        }

        public async Task<AuthorModelItem> DeleteAsync(int id)
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
