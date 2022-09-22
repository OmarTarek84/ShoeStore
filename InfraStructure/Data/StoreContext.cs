
using Core.Entities;
using Core.Entities.Cart;
using Core.Entities.Identity;
using Core.Entities.Orders;
using Core.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace InfraStructure.Data
{
    public class StoreContext: IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public StoreContext(DbContextOptions<StoreContext> options, IHttpContextAccessor contextAccessor): base(options)
        {
            _contextAccessor = contextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("Users", "security");

            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            if (_contextAccessor.HttpContext is not null)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                AddedEntities.ForEach(E =>
                {
                    //var type = E.Entity.GetType();
                    //if (type == typeof(AppUser) || type == typeof(IdentityUserRole<string>)) return;
                    if (E.Entity is BaseEntity entity)
                    {
                        E.Property("CreateDate").CurrentValue = DateTime.Now;
                        E.Property("CreatedBy").CurrentValue = userId;
                        E.Property("UpdateDate").CurrentValue = DateTime.Now;
                        E.Property("UpdatedBy").CurrentValue = userId;
                    }
                });

                var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

                EditedEntities.ForEach(E =>
                {
                    //var type = E.Entity.GetType();
                    //if (type == typeof(AppUser)) return;
                    if (E.Entity is BaseEntity entity)
                    {
                        E.Property("CreateDate").IsModified = false;
                        E.Property("CreatedBy").IsModified = false;
                        E.Property("UpdateDate").CurrentValue = DateTime.Now;
                        E.Property("UpdatedBy").CurrentValue = userId;
                    }
                });
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
