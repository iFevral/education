using System;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;
using Store.BusinessLogic.Models.Authors;

namespace Store.BusinessLogic.Common.Mappers
{
    public class PrintingEditionMapper : IMapper<PrintingEditions, PrintingEditionModelItem>
    {
        public PrintingEditions Map(PrintingEditionModelItem model, PrintingEditions entity)
        {
            entity.Title = string.IsNullOrWhiteSpace(model.Title)
                ? entity.Title
                : model.Title;

            entity.Description = string.IsNullOrWhiteSpace(model.Description)
                ? entity.Description
                : model.Description;

            entity.Price = model.Price == 0
                ? entity.Price
                : model.Price;

            entity.Currency = string.IsNullOrWhiteSpace(model.Currency)
                ? entity.Currency
                : (Enums.PrintingEditions.Currencies)Enum.Parse(typeof(Enums.PrintingEditions.Currencies), model.Currency);

            entity.Type = string.IsNullOrWhiteSpace(model.Type)
                ? entity.Type
                : (Enums.PrintingEditions.Types)Enum.Parse(typeof(Enums.PrintingEditions.Types), model.Type);

            return entity;
        }

        public PrintingEditionModelItem Map(PrintingEditions entity, PrintingEditionModelItem model)
        {
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