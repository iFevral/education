using AutoMapper;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.Presentation.Common
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Users, UserModelItem>();
            CreateMap<UserModelItem, Users>();
            CreateMap<SignUpData, Users>();
            
            CreateMap<Authors, AuthorModelItem>();
            CreateMap<AuthorModelItem, Authors>();

            CreateMap<PrintingEditions, PrintingEditionModelItem>();
            CreateMap<PrintingEditionModelItem, PrintingEditions>();
        }
    }
}
