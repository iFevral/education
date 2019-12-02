using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Common.Mappers;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;

namespace Store.BusinessLogic.Initialization
{
    public static class ServicesInitializator
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            EFRepositoryInitializer.Initialize(services, configuration);

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IUserService, UserService>();

            //Mappers
            services.AddScoped<IMapper<Authors, AuthorModelItem>, AuthorMapper>();
            services.AddScoped<IMapper<Orders, OrderModelItem>, OrderMapper>();
            services.AddScoped<IMapper<PrintingEditions, PrintingEditionModelItem>, PrintingEditionMapper>();
            services.AddScoped<IMapper<Users, SignUpModel>, SignUpMapper>();
            services.AddScoped<IMapper<Users, UserModelItem>, UserMapper>();
        }
    }
}