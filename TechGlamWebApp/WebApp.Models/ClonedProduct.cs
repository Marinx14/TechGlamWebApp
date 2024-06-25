using WebApp.Models;
using WebApp.enumeration;
/// <summary>
/// Represents a cloned product.
/// </summary>
public class ClonedProduct
{
    /// <summary>
    /// Gets or sets the unique identifier for the cloned product.
    /// </summary>
    public Guid ClonedProductID { get; set; }

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product ordered.
    /// </summary>
    public int QuantityOrdered { get; set; } = 0;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public WebAppEnum.Category Category { get; set; }

    /// <summary>
    /// Gets or sets the size of the product.
    /// </summary>
    public WebAppEnum.Size Size { get; set; }

    /// <summary>
    /// Gets or sets the color of the product.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the associated cart.
    /// </summary>
    public Guid AssociatedCartID { get; set; }

    /// <summary>
    /// Gets or sets the associated cart.
    /// </summary>
    public virtual Cart AssociatedCart { get; set; }

    // Uncomment these properties if you want to link orders as well.
    // public Guid AssociatedOrderID { get; set; }
    // public virtual Order AssociatedOrder { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClonedProduct"/> class.
    /// </summary>
    public ClonedProduct()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClonedProduct"/> class by cloning an existing product.
    /// </summary>
    /// <param name="product">The product to clone.</param>
    public ClonedProduct(Product product)
    {
        ClonedProductID = Guid.NewGuid();
        Name = product.Name;
        Image = product.Image;
        Description = product.Description;
        Price = product.Price;
        Category = product.Category;
        Size = product.Size;
        Color = product.Color;
    }
}
