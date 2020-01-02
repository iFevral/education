﻿using System;
using System.Collections.Generic;
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
            entity.Currency = model.Currency;
            entity.Type =  model.Type;
            entity.Image = model.Image;
            entity.AuthorInPrintingEditions = new List<AuthorInPrintingEdition>();
            foreach(var author in model.Authors)
            {
                var authorId = author.Id;
                var authorInBooks = new AuthorInPrintingEdition();
                authorInBooks.AuthorId = authorId;
                entity.AuthorInPrintingEditions.Add(authorInBooks);
            }

            return entity;
        }
    }
}