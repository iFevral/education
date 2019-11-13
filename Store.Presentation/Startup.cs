using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;

namespace Store.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ExamDB"),
                    assembly => assembly.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });

            services.AddIdentity<Users, Roles>()
                .AddEntityFrameworkStores<ApplicationContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationContext context, UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
