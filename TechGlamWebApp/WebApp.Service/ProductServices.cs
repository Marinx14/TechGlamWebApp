using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.enumeration;

namespace WebApp.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to get the list of products
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<List<Product>> FilterProducts(WebAppEnum.Category? category, WebAppEnum.SizeRingsBracelets? sizeRingsBracelets, WebAppEnum.SizeWatches? sizeWatches)
        {
            var filteredProducts = await _dbContext.Products.ToListAsync();
            if (category != null)
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category).ToList();
            }
            if (sizeRingsBracelets != null)
            {
                filteredProducts = filteredProducts.Where(p => p.SizeRingsBracelets == sizeRingsBracelets).ToList();
            }
            if (sizeWatches != null)
            {
                filteredProducts = filteredProducts.Where(p => p.SizeWatches == sizeWatches).ToList();
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
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == productToDelete.ProductId);
            if (productToDelete != null && productToDelete.Quantity - quantity <= 0)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
            else
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
                p.SizeRingsBracelets == product.SizeRingsBracelets &&
                p.SizeWatches == product.SizeWatches &&
                p.Color == product.Color);

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
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Image = updatedProduct.Image;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.SizeRingsBracelets = updatedProduct.SizeRingsBracelets;
                existingProduct.SizeWatches = updatedProduct.SizeWatches;
                existingProduct.Color = updatedProduct.Color;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("The product to update was not found in the database.");
            }
        }

        public bool CheckFields(string name, string description, string price, string quantity, string color, string sizeRingsBracelets, string sizeWatches, string category)
        {
            // Check if the fields are empty or null
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(price.ToString()) || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(color)
                || string.IsNullOrEmpty(this.VerifySizeRingsBracelets(sizeRingsBracelets)) || string.IsNullOrEmpty(this.VerifySizeWatches(sizeWatches)) || string.IsNullOrEmpty(this.VerifyCategory(category)))
            {
                return false;
            }

            return true;
        }

        public string VerifySizeRingsBracelets(string size)
        {
            switch (size)
            {
                case nameof(WebAppEnum.SizeRingsBracelets.S):
                case nameof(WebAppEnum.SizeRingsBracelets.M):
                case nameof(WebAppEnum.SizeRingsBracelets.L):
                case nameof(WebAppEnum.SizeRingsBracelets.XL):
                    // The string matches a value from the SizeRingsBracelets enum
                    return size;
                default:
                    // The string does not match any value from the SizeRingsBracelets enum
                    return null;
            }
        }

        public string VerifySizeWatches(string size)
        {
            switch (size)
            {
                case nameof(WebAppEnum.SizeWatches.mm40):
                case nameof(WebAppEnum.SizeWatches.mm44):
                case nameof(WebAppEnum.SizeWatches.mm45):
                    // The string matches a value from the SizeWatches enum
                    return size;
                default:
                    // The string does not match any value from the SizeWatches enum
                    return null;
            }
        }

        public string VerifyCategory(string category)
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
