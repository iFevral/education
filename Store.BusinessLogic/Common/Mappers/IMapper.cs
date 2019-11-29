using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Common.Mappers.User
{
    public interface IMapper<T, U> 
        where T : class
        where U : BaseModel
    {
        public T Map(U src, T dest);
        public U Map(T src, U dest);
    }
}