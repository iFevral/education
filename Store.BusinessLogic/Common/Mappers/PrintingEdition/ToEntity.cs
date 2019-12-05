using System;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Common.Mappers.PrintingEdition
{
    public static partial class PrintingEditionMapperExtension
    {
        public static DataAccess.Entities.PrintingEdition MapToEntity(this PrintingEditionModelItem model, DataAccess.Entities.PrintingEdition entity)
        {
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Currency = (Enums.PrintingEditions.Currencies)Enum.Parse(typeof(Enums.PrintingEditions.Currencies), model.Currency);
            entity.Type = (Enums.PrintingEditions.Types)Enum.Parse(typeof(Enums.PrintingEditions.Types), model.Type);
            
            foreach(var author in model.Authors)
            {
                var authorId = author.Id;
                var authorInBooks = new AuthorInBooks();
                authorInBooks.AuthorId = authorId;
                entity.AuthorInBooks.Add(authorInBooks);
            }

            return entity;
        }
    }
}