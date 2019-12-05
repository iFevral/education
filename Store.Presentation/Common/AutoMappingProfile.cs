using AutoMapper;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;

namespace Store.Presentation.Common
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Author, AuthorModelItem>();
            CreateMap<AuthorModelItem, Author>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<Order, OrderModelItem>();
            CreateMap<OrderModelItem, Order>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<OrderItem, OrderItemModelItem>();
            CreateMap<OrderItemModelItem, OrderItem>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<Payment, PaymentModelItem>();
            CreateMap<PaymentModelItem, Payment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<PrintingEdition, PrintingEditionModelItem>();
            CreateMap<PrintingEditionModelItem, PrintingEdition>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<User, UserModelItem>();
            CreateMap<UserModelItem, User>();
            CreateMap<SignUpModel, User>();
            
        }
    }
}
