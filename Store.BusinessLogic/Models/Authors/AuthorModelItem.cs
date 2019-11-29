using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModelItem : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}