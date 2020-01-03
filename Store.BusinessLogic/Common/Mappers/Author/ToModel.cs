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

            foreach (var item in entity.AuthorInPrintingEdition)
            {
                if (item == null)
                {
                    continue;
                }
                var printingEdition = new PrintingEditionModelItem();
                printingEdition.Id = item.PrintingEditionId;
                printingEdition.Title = item.PrintingEdition.Title;
                model.PrintingEditions.Add(printingEdition);
            }

            return model;
        }
    }
}
