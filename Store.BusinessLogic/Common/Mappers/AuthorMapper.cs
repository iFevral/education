using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Common.Mappers
{
    public class AuthorMapper : IMapper<Authors, AuthorModelItem>
    {
        public Authors Map(AuthorModelItem model, Authors entity)
        {
            entity.Name = string.IsNullOrWhiteSpace(model.Name)
                ? entity.Name
                : model.Name;

            return entity;
        }

        public AuthorModelItem Map(Authors entity, AuthorModelItem model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;

            foreach (var pe in entity.AuthorInBooks)
            {
                model.PrintingEditions.Add(new PrintingEditionModelItem
                {
                    Id = pe.PrintingEditionId,
                    Title = pe.PrintingEdition.Title
                });
            }

            return model;
        }
    }
}
