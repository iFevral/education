using AutoMapper;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.Presentation.Common
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Users, UserModelItem>();
            CreateMap<UserModelItem, Users>();
            CreateMap<SignUpData, Users>();
        }
    }
}
