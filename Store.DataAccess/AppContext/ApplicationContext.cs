using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;

namespace Store.DataAccess.AppContext
{
    public partial class ApplicationContext : IdentityDbContext<Users,
                                                                Roles,
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
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<PrintingEditions> PrintingEditions { get; set; }
        public override DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserInRoles> UserInRoles { get; set; }
        public override DbSet<Users> Users { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DbConnection");
            }
        }

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

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasIndex(e => e.OrderId);

                entity.HasIndex(e => e.PrintingEditionId);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasIndex(e => e.PaymentId);
                entity.HasOne(x => x.User)
                      .WithMany(x => x.Orders)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasOne(x => x.User)
                      .WithMany(x => x.Sessions)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<UserInRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);
                entity.HasOne(x => x.User)
                      .WithMany(x => x.UserInRoles)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
                
            });

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>().HasNoKey();
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}