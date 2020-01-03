using Store.BusinessLogic.Models.Authors;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Mappers.AuthorInPrintingEdition
{
    public static partial class AuthorInPrintingEditionMapperExtension
    {
        public static IEnumerable<DataAccess.Entities.AuthorInPrintingEdition> MapToAuthorInPrintingEditionList(this IList<AuthorModelItem> items, long printingEditionId)
        {
            var list = new List<DataAccess.Entities.AuthorInPrintingEdition>();

            foreach (var author in items)
            {
                var authorInPrintingEdition = new DataAccess.Entities.AuthorInPrintingEdition();
                
                authorInPrintingEdition.Id = 0;
                authorInPrintingEdition.PrintingEditionId = printingEditionId;
                authorInPrintingEdition.AuthorId = author.Id;

                list.Add(authorInPrintingEdition);
            }

            return list;
        }
    }
}
