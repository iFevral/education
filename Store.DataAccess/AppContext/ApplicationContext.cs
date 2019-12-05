using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

namespace Store.DataAccess.AppContext
{
    public partial class ApplicationContext : IdentityDbContext<User,
                                                                Role,
                                                                string,
                                                                IdentityUserClaim<string>,
                                                                UserInRoles,
                                                                IdentityUserLogin<string>,
                                                                IdentityRoleClaim<string>,
                                                                IdentityUserToken<string>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthorInBooks> AuthorInBooks { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PrintingEdition> PrintingEditions { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserInRoles> UserInRoles { get; set; }
        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorInBooks>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.PrintingEditionId });

                entity.HasIndex(e => e.AuthorId);
                entity.HasOne(x => x.Author)
                      .WithMany(x => x.AuthorInBooks)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.PrintingEditionId);
                entity.HasOne(x => x.PrintingEdition)
                      .WithMany(x => x.AuthorInBooks)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.PaymentId);
                entity.HasOne(x => x.User)
                      .WithMany(x => x.Orders)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Status)
                    .HasConversion(x => (int) x, x => (Enums.Orders.Statuses) x);
            });

            modelBuilder.Entity<PrintingEdition>(entity =>
            {
                entity.Property(e => e.Currency)
                    .HasConversion(x => (int)x, x => (Enums.PrintingEditions.Currencies)x);

                entity.Property(e => e.Type)
                    .HasConversion(x => (int)x, x => (Enums.PrintingEditions.Types)x);
            });

                modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.OrderId);

                entity.HasIndex(e => e.PrintingEditionId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
                
            });

            modelBuilder.Entity<UserInRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);
                entity.HasOne(x => x.User)
                      .WithMany(x => x.UserInRoles)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>().HasNoKey();
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        }
    }
}