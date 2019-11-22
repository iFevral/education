using AutoMapper;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;

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
            CreateMap<AuthorModelItem, Authors>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<PrintingEditions, PrintingEditionModelItem>();

            CreateMap<PrintingEditionModelItem, PrintingEditions>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0"))); 
        }
    }
}
