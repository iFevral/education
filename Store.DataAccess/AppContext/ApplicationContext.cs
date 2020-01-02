using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

namespace Store.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>

    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<AuthorInPrintingEdition> AuthorInBooks { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PrintingEdition> PrintingEditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorInPrintingEdition>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.PrintingEditionId });

                entity.HasIndex(e => e.AuthorId);
                entity.HasOne(x => x.Author)
                      .WithMany(x => x.AuthorInPrintingEdition)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.PrintingEditionId);
                entity.HasOne(x => x.PrintingEdition)
                      .WithMany(x => x.AuthorInPrintingEditions)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.PaymentId);
                entity.HasOne(x => x.User)
                      .WithMany(x => x.Orders)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Status)
                    .HasConversion(x => (int)x, x => (Enums.Order.OrderStatus)x);
            });

            modelBuilder.Entity<PrintingEdition>(entity =>
            {
                entity.Property(e => e.Currency)
                    .HasConversion(x => (int)x, x => (Enums.PrintingEditions.Currency)x);

                entity.Property(e => e.Type)
                    .HasConversion(x => (int)x, x => (Enums.PrintingEditions.PrintingEditionType)x);
            });

            modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.OrderId);

            entity.HasIndex(e => e.PrintingEditionId);
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

            modelBuilder.Entity<IdentityRole<long>>(entity =>
            {
                entity.HasKey(e => e.Id );
            });

            modelBuilder.Entity<IdentityUserRole<long>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserLogin<long>>().HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<long>>().HasNoKey();
            modelBuilder.Entity<IdentityRoleClaim<long>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<long>>().HasNoKey();
        }
    }
}