using Store.BusinessLogic.Models.Authors;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Mappers.AuthorInPrintingEdition
{
    public static partial class AuthorInPrintingEditionMapperExtension
    {
        public static IEnumerable<DataAccess.Entities.AuthorInPrintingEdition> MapToAuthorInPrintingEditionList(this IList<AuthorModelItem> items, long printingEditionId)
        {
            var authorInPrintingEditions = new List<DataAccess.Entities.AuthorInPrintingEdition>();

            foreach (var author in items)
            {
                var authorInBooks = new DataAccess.Entities.AuthorInPrintingEdition();
                authorInBooks.PrintingEditionId = printingEditionId;
                authorInBooks.AuthorId = author.Id;
                authorInPrintingEditions.Add(authorInBooks);
            }

            return authorInPrintingEditions;
        }
    }
}
