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
            model.Currency = entity.Currency.ToString();
            model.Type = entity.Type.ToString();

            foreach(var author in entity.AuthorInBooks)
            {
                model.Authors.Add(new AuthorModelItem
                {
                    Id = author.AuthorId,
                    Name = author.Author.Name
                });
            }

            return model;
        }
    }
}