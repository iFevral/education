﻿using AutoMapper;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;

namespace Store.Presentation.Common
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Authors, AuthorModelItem>();
            CreateMap<AuthorModelItem, Authors>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<Orders, OrderModelItem>();
            CreateMap<OrderModelItem, Orders>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment.Id))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<OrderItems, OrderItemModelItem>();
            CreateMap<OrderItemModelItem, OrderItems>()
                .ForMember(dest => dest.PrintingEditionId, opt => opt.MapFrom(src => src.PrintingEdition.Id))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<Payments, PaymentModelItem>();
            CreateMap<PaymentModelItem, Payments>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<PrintingEditions, PrintingEditionModelItem>();
            CreateMap<PrintingEditionModelItem, PrintingEditions>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.ToString().Equals("0")));

            CreateMap<Users, UserModelItem>();
            CreateMap<UserModelItem, Users>();
            CreateMap<SignUpData, Users>();
            
        }
    }
}
