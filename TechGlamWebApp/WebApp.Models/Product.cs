using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.enumeration;
using System;
using System.Drawing;

namespace WebApp.Models
{
    /// <summary>
    /// Represents a product in the web application.
    /// </summary>
    public abstract class Product
    {
       

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product available in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public WebAppEnum.Category Category { get; set; }

        /// <summary>
        /// Gets or sets the color of the product.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Gets or sets the size of the product.
        /// </summary>
        
        public WebAppEnum.SizeRingsBracelets Size { get; set; }
        
        
        public WebAppEnum.SizeWatches? SizeWatches;
        public string MetalType { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            ProductID = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class with specified details.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="image">The image URL of the product.</param>
        /// <param name="description">The description of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="category">The category of the product.</param>
        /// <param name="size">The size of the product.</param>
        /// <param name="color">The color of the product.</param>

        public Product(string name, string image, string description, decimal price, WebAppEnum.Category category,
                       string color, WebAppEnum.SizeRingsBracelets size)
            : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "The name cannot be null.");
            Image = image ?? throw new ArgumentNullException(nameof(image), "The image URL cannot be null.");
            Description = description ?? throw new ArgumentNullException(nameof(description), "The description cannot be null.");
            Price = price;
            Category = category;
            Color = color ?? throw new ArgumentNullException(nameof(color), "The color cannot be null.");
            Size = size;
        }

        /// <summary>
        /// Creates a cloned instance of the product.
        /// </summary>
        /// <returns>A cloned <see cref="ClonedProduct"/> instance.</returns>
        public ClonedProduct CreateClonedProduct()
        {
            return new ClonedProduct
            {
                Name = this.Name,
                Image = this.Image,
                Description = this.Description,
                Price = this.Price,
                Category = this.Category,
                Color = this.Color,
                Size = this.Size,
            };
        }

        
    }


}