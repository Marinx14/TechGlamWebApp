using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.enumeration;

namespace WebApp.ProductServices
{
    public class ProductServices
    {
        private readonly AppDbContext _dbContext;

        public ProductServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to get the list of products
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<List<Product>> FilterProducts(WebAppEnum.Category? category, WebAppEnum.Size? size)
        {
            var filteredProducts = await _dbContext.Products.ToListAsync();
            if (category != null)
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category).ToList();
            }
            if (size != null)
            {
                filteredProducts = filteredProducts.Where(p => p.Size == size).ToList();
            }
            return filteredProducts;
        }

        public async Task<List<Product>> SearchProductsByName(string searchName)
        {
            var list = await _dbContext.Products.ToListAsync();

            if (!string.IsNullOrEmpty(searchName))
            {
                list = list.Where(p => p.Name.ToLower().Contains(searchName.ToLower())).ToList();
            }

            return list;
        }

        public async Task DeleteProduct(Product productToDelete, int quantity)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductID == productToDelete.ProductID);
            if (productToDelete != null && productToDelete.Quantity - quantity <= 0)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
            else if (productToDelete != null) // Add null check here
            {
                productToDelete.Quantity -= quantity;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p =>
                p.Name == product.Name &&
                p.Description == product.Description &&
                p.Price == product.Price &&
                p.Category == product.Category &&
                p.Size == product.Size &&
                p.Color == product.Color &&
                p.MetalType == product.MetalType);

            if (existingProduct == null)
            {
                await _dbContext.Products.AddAsync(product);
            }
            else
            {
                existingProduct.Quantity += product.Quantity;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductID == updatedProduct.ProductID);

            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Image = updatedProduct.Image;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Size = updatedProduct.Size;
                existingProduct.Color = updatedProduct.Color;
                existingProduct.MetalType = updatedProduct.MetalType;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("The product to update was not found in the database.");
            }
        }

        public bool CheckFields(string name, string description, string price, string quantity, string color, string size, string category)
        {
            // Check if the fields are empty or null
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(price.ToString()) || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(color)
                || string.IsNullOrEmpty(this.VerifySize(size)) || string.IsNullOrEmpty(this.VerifyCategory(category)))
            {
                return false;
            }

            return true;
        }

        public string? VerifySize(string size)
        {
            switch (size)
            {
                case nameof(WebAppEnum.Size.S):
                case nameof(WebAppEnum.Size.M):
                case nameof(WebAppEnum.Size.L):
                case nameof(WebAppEnum.Size.XL):
                    // The string matches a value from the Size enum
                    return size;
                default:
                    // The string does not match any value from the Size enum
                    return null;
            }
        }

        public string? VerifyCategory(string category)
        {
            switch (category)
            {
                case nameof(WebAppEnum.Category.Rings):
                case nameof(WebAppEnum.Category.Bracelets):
                case nameof(WebAppEnum.Category.Watches):
                    // The string matches a value from the Category enum
                    return category;
                default:
                    // The string does not match any value from the Category enum
                    return null;
            }
        }
    }
}