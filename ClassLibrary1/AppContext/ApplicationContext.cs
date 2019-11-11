using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Store.DataAccess.Entities;

namespace Store.DataAccess.AppContext
{
    class ApplicationContext : IdentityDbContext<Users>
    {
        public DbSet<AuthorInBooks> AuthorInBooks { get;set;}
        public DbSet<Authors> Authors { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<PrintingEditions> PrintingEditions { get; set; }

        public ApplicationContext() : base("DB4Exam")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole>().ToTable("UserInRoles");
        }
    }
}
