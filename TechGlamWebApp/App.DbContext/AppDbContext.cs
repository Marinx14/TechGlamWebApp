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
        public DbSet<Cart> CartProduct { get; set; }
        
        public DbSet<ClonedProduct> CartDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasKey(User => User.Id);

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