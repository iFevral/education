using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Initialization
{
    public static class EFRepositoryInitializer
    {
        public static void Initialize(IServiceCollection services,
                                      IConfiguration configuration)
        {
            DbInitializer.Initialize(services, configuration);

            services.AddScoped<DataSeeder>();
            
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, Repositories.DapperRepositories.OrderRepository>();
            services.AddScoped<IAuthorRepository, Repositories.DapperRepositories.AuthorRepository>();
            services.AddScoped<IPrintingEditionRepository, Repositories.DapperRepositories.PrintingEditionRepository>();
            services.AddScoped<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
