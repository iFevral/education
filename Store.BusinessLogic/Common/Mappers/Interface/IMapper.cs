using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Common.Mappers.Interface
{
    public interface IMapper<TEntity, UModel> 
        where TEntity : class
        where UModel : BaseModel
    {
        public TEntity Map(UModel model, TEntity entity);
        public UModel Map(TEntity entity, UModel model);
    }
}