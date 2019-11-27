using AutoMapper;
using System;
using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class AuthorService : IAuthorService
    {
        private IMapper _mapper;
        private IAuthorRepository _authorRepository;

        public AuthorService(ApplicationContext db,
                                IMapper mapper)
        {
            _authorRepository = new AuthorRepository(db);
            _mapper = mapper;
        }

        public AuthorModel GetAll(string authorNameFilter, int startIndex = -1, int quantity = -1)
        {
            var authorModel = new AuthorModel();
            IList<Authors> authors;
            
            Func<Authors, bool> predicate = (a => (authorNameFilter == null || a.Name.Contains(authorNameFilter)));
            if (startIndex != -1 && quantity != -1)
                authors = _authorRepository.Get(predicate, startIndex, quantity);
            else
                authors = _authorRepository.GetAll(predicate);

            if (authors.Count == 0)
            {
                authorModel.Errors.Add($"Author not found");
                return authorModel;
            }

            foreach (var author in authors)
            {
                var a = _mapper.Map<AuthorModelItem>(author);
                foreach (var printingEdition in author.AuthorInBooks)
                {
                    var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition.PrintingEdition);
                    a.PrintingEditions.Add(pe);
                }

                authorModel.Authors.Add(a);
            }

            return authorModel;
        }

        public async Task<AuthorModel> FindById(int id)
        {
            var authorModel = new AuthorModel();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            var a = _mapper.Map<AuthorModelItem>(author);
            foreach (var printingEdition in author.AuthorInBooks)
            {
                var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition.PrintingEdition);
                a.PrintingEditions.Add(pe);
            }

            authorModel.Authors.Add(a);

            return authorModel;
        }

        public async Task<AuthorModel> Create(AuthorModelItem authorItem)
        {
            var authorModel = new AuthorModel();
            var author = _mapper.Map<Authors>(authorItem);

            author.AuthorInBooks = new List<AuthorInBooks>();
            foreach (var printingEdition in authorItem.PrintingEditions)
                author.AuthorInBooks.Add(new AuthorInBooks { PrintingEditionId = printingEdition.Id });

            await _authorRepository.CreateAsync(author);
            return authorModel;
        }

        public async Task<AuthorModel> Update(int id, AuthorModelItem authorItem)
        {
            var authorModel = new AuthorModel();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            _mapper.Map<AuthorModelItem, Authors>(authorItem, author);
            _authorRepository.RemovePrintingEditions(author.Id);

            foreach (var printingEditionItem in authorItem.PrintingEditions)
            {
                if (author.AuthorInBooks == null)
                    author.AuthorInBooks = new List<AuthorInBooks>();

                author.AuthorInBooks.Add(new AuthorInBooks { PrintingEditionId = printingEditionItem.Id });
            }
            await _authorRepository.UpdateAsync(author);
            return authorModel;
        }

        public async Task<AuthorModel> Delete(int id)
        {
            var authorModel = new AuthorModel();
            var author = await _authorRepository.FindByIdAsync(id);
            if (author == null)
            {
                authorModel.Errors.Add("Author not found");
                return authorModel;
            }

            await _authorRepository.RemoveAsync(author);

            return authorModel;
        }
    }
}
