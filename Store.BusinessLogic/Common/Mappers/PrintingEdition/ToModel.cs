using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Common.Mappers.PrintingEdition
{
    public static partial class PrintingEditionMapperExtension
    {
        public static PrintingEditionModelItem MapToModel(this DataAccess.Entities.PrintingEdition entity)
        {
            var model = new PrintingEditionModelItem();

            model.Id = entity.Id;
            model.Title = entity.Title;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Currency = entity.Currency;
            model.Type = entity.Type;

            foreach(var item in entity.AuthorInBooks)
            {
                var author = new AuthorModelItem();
                author.Id = item.AuthorId;
                author.Name = item.Author.Name;
                
                model.Authors.Add(author);
            }

            return model;
        }
    }
}