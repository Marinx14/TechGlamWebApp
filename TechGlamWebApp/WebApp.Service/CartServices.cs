using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Service
{
    public class CartServices
    {
        public AppDbContext _dbContext { get; set; }
        public List<ClonedProduct> _clonedProducts { get; set; }

        public CartServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _clonedProducts = new List<ClonedProduct>();
        }

        public async Task<Cart> GetCartAsync(string userId)
        {
            var cart = await _dbContext.CartProducts.FirstOrDefaultAsync(d => d.AssociatedUserId == userId);
            if (cart == null)
            {
                cart = new Cart()
                {
                    CartId = Guid.NewGuid(),
                    AssociatedUserId = userId,
                    ClonedProducts = new List<ClonedProduct>()
                };
                await _dbContext.CartProducts.AddAsync(cart);
                await _dbContext.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddProductToCart(string userId, Product productToAdd, int quantity)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == productToAdd.ProductId);
            var clonedProduct = new ClonedProduct(existingProduct);
            var cartDetails = await _dbContext.CartDetails.ToListAsync();
            var cart = await this.GetCartAsync(userId);
            cartDetails = cartDetails.Where(p => p.AssociatedCartId.Equals(cart.CartId)).ToList();
            var existing = cartDetails.FirstOrDefault(pc => pc.Name.Equals(clonedProduct.Name) && pc.Size.Equals(clonedProduct.Size)
            && pc.AssociatedCartId.Equals(cart.CartId));

            if (cart != null)
            {
                if (existing != null)
                {
                    existing.QuantityOrdered += quantity;
                    _dbContext.Entry(existing).State = EntityState.Modified;
                }
                else
                {
                    clonedProduct.AssociatedCartId = cart.CartId;
                    clonedProduct.QuantityOrdered = quantity;
                    _dbContext.Entry(clonedProduct).State = EntityState.Added;
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductFromCart(Guid clonedProductId, string userId)
        {
            var cartDetails = await _dbContext.CartDetails.ToListAsync();
            var product = _dbContext.CartDetails.FirstOrDefault(p => p.ClonedProductId == clonedProductId);
            if (product != null)
            {
                 _dbContext.CartDetails.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EmptyCart(string userId)
        {
            var cart = await this.GetCartAsync(userId);
            var cartDetails = await _dbContext.CartDetails.ToListAsync();
            cartDetails = cartDetails.Where(p => p.AssociatedCartId.Equals(cart.CartId)).ToList();
            foreach (var item in cartDetails) 
            {
                _dbContext.CartDetails.RemoveRange(item);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<bool> SubmitProducts(string userId)
        {
            var cart = await GetCartAsync(userId);
            var cartDetails = await _dbContext.CartDetails.ToListAsync();
            cartDetails = cartDetails.Where(p => p.AssociatedCartId.Equals(cart.CartId)).ToList();
            if (cart != null && cartDetails.Any())
            {
                foreach (var product in cart.ClonedProducts)
                {
                    var productInDb = await GetProductByIdAsync(product.ClonedProductId);
                    if (productInDb != null)
                    {
                        if (productInDb.Quantity < product.QuantityOrdered)
                        {
                            return false;
                        }
                        else
                        {
                            productInDb.Quantity -= product.QuantityOrdered;
                            _dbContext.Products.Update(productInDb);
                            _dbContext.CartDetails.Remove(product);
                            cart.ClonedProducts.Remove(product);
                            _dbContext.CartProducts.Update(cart);

                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                await EmptyCart(userId);
                return true;
            }
            return false;
        }

        public async Task<List<ClonedProduct>> GetClonedProductsAsync(Cart cart)
        {
            var result = await _dbContext.CartDetails.ToListAsync();
            result = result.Where(p => p.AssociatedCartId.Equals(cart.CartId)).DistinctBy(t => t.Name).ToList();
            return result;
        }
    }
}
