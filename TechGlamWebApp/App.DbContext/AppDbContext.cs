using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ClonedProduct> ClonedProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasKey(user => user.Id);

            builder.Entity<Product>()
                .HasKey(product => product.ProductID);

            builder.Entity<ClonedProduct>()
                .HasKey(clonedProduct => clonedProduct.ClonedProductID);

            builder.Entity<Cart>()
                .HasKey(cart => cart.IdCart);

            builder.Entity<Cart>()
                .HasMany(cart => cart.ClonedProducts)
                .WithOne(clonedProduct => clonedProduct.AssociatedCart)
                .HasForeignKey(clonedProduct => clonedProduct.AssociatedCartID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}