using Store.BusinessLogic.Models.Authors;

namespace Store.BusinessLogic.Common.Mappers.Author
{
    public static partial class AuthorMapperExtension
    {
        public static DataAccess.Entities.Author MapToEntity(this AuthorModelItem model, DataAccess.Entities.Author entity)
        {
            entity.Name = model.Name;
            return entity;
        }
    }
}
