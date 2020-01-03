using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Common.Mappers.Author
{
    public static partial class AuthorMapperExtension
    {
        public static AuthorModelItem MapToModel(this DataAccess.Entities.Author entity)
        {
            var model = new AuthorModelItem();

            model.Id = entity.Id;
            model.Name = entity.Name;

            foreach (var authorInBook in entity.AuthorInPrintingEdition)
            {
                var printingEdition = new PrintingEditionModelItem();
                printingEdition.Id = authorInBook.PrintingEditionId;
                printingEdition.Title = authorInBook.PrintingEdition.Title;
                model.PrintingEditions.Add(printingEdition);
            }

            return model;
        }
    }
}
