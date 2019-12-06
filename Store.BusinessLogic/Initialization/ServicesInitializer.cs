using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Helpers;
using Store.BusinessLogic.Helpers.Interface;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Initialization;

namespace Store.BusinessLogic.Initialization
{
    public static class ServicesInitializator
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            EFRepositoryInitializer.Initialize(services, configuration);

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
        }
    }
}