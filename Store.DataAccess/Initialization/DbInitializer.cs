using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Initialization
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationContext>();
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DbConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });

            services.AddIdentity<Users, Roles>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders().AddTokenProvider("StoreProvider", typeof(DataProtectorTokenProvider<Users>));

        }
    }
}
