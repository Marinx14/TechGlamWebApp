using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    /// <summary>
    /// The AppDbContext class represents the Entity Framework database context for the application.
    /// It extends IdentityDbContext to include ASP.NET Core Identity features.
    /// </summary>
    public class AppDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the AppDbContext class using specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Users table.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Products table.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Carts table.
        /// </summary>
        public DbSet<Cart> Carts { get; set; }

        /// <summary>
        /// Gets or sets the ClonedProducts table, representing products added to carts.
        /// </summary>
        public DbSet<ClonedProduct> ClonedProducts { get; set; }

        /// <summary>
        /// Configures the model and relationships using Fluent API.
        /// </summary>
        /// <param name="builder">ModelBuilder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure User entity primary key
            builder.Entity<User>()
                .HasKey(user => user.Id);

            // Configure Product entity primary key
            builder.Entity<Product>()
                .HasKey(product => product.ProductID);

            // Configure ClonedProduct entity primary key
            builder.Entity<ClonedProduct>()
                .HasKey(clonedProduct => clonedProduct.ClonedProductID);

            // Configure Cart entity primary key
            builder.Entity<Cart>()
                .HasKey(cart => cart.IdCart);

            // Configure relationship between Cart and ClonedProducts
            builder.Entity<Cart>()
                .HasMany(cart => cart.clonedProduct)
                .WithOne(clonedProduct => clonedProduct.AssociatedCart)
                .HasForeignKey(clonedProduct => clonedProduct.AssociatedCartID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
