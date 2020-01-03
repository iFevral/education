using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Repositories.DapperRepositories;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Initialization
{
    public static class DapperRepositoriesInitializer
    {
        public static void Initialize(IServiceCollection services,
                                      IConfiguration configuration)
        {

            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddScoped<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
