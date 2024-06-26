using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.CartService;

public class CartServices
{
    public AppDbContext _dbContext { get; set; }
    public List<ClonedProduct> _prodottiClonati { get; set; }
    public CartServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _prodottiClonati = new List<ClonedProduct>();

    }

    public async Task<Cart> GetCartAsync(string IdUser)
    {
        var cart = await _dbContext.CartProduct.FirstOrDefaultAsync(d => d.IdUserAssociated == IdUser);
        if (cart == null)
        {
            cart = new Cart()
            {
                IdCart = Guid.NewGuid(),
                IdUserAssociated = IdUser,
                ClonedProducts = new List<ClonedProduct>()
            };
            await _dbContext.CartProduct.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
        }
        return cart;
    }

    public async Task<Product?> GetProductByIdAsync(Guid idProduct)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == idProduct);
    }

    public async Task RemoveProductFromCart(string userId)
    {
        //Prendere ID del carrello associato all'utente per rimuovere
        var cart = await this.GetCartAsync(userId);
        var cartDetails = await _dbContext.CartDetails.ToListAsync();
        cartDetails = cartDetails.Where(p => p.AssociatedCartID.Equals(cart.IdCart)).ToList();
        foreach (var item in cartDetails) 
        {
            _dbContext.CartDetails.RemoveRange(item);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task EmptyCart(string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddProductToCart(string idUser, Product productToAddToCart, int quantity)
    {
        var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductID == productToAddToCart.ProductID);
        var clonedProduct = new ClonedProduct(existingProduct);
        var detailsCart = await _dbContext.CartDetails.ToListAsync();
        var cart = await this.GetCartAsync(idUser);
        detailsCart = detailsCart.Where(p => p.AssociatedCartID.Equals(cart.IdCart)).ToList();
        var esistente = detailsCart.FirstOrDefault(pc => pc.Name.Equals(ClonedProduct.NameOf) && pc.Size.Equals(clonedProduct.Size)
            && pc.AssociatedCartID.Equals(cart.IdCart));

        if (cart != null)
        {
            if (esistente != null)
            {
                esistente.QuantityOrdered += quantity;
                _dbContext.Entry(esistente).State = EntityState.Modified;
            }
            else
            {
                clonedProduct.AssociatedCartID = cart.IdCart;
                clonedProduct.QuantityOrdered = quantity;
                _dbContext.Entry(clonedProduct).State = EntityState.Added;
            }
        }
        await _dbContext.SaveChangesAsync();
    }


    public async Task<Product> GetProductIdAsync(Guid idProduct)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == idProduct);
    }
    public async Task<bool> SendProduct(string idUtente)
    {
        var cart = await GetCartAsync(idUtente);
        var cartDetails = await _dbContext.CartDetails.ToListAsync();
        cartDetails = cartDetails.Where(p => p.AssociatedCartID.Equals(cart.IdCart)).ToList();
        if (cart != null && cartDetails.Any())
        {
            foreach (var product in cart.ClonedProducts)
            {
                var prodottoNelDb = await GetProductIdAsync(product.ClonedProductID);
                if (prodottoNelDb != null)
                {
                    if (prodottoNelDb.Quantity < product.QuantityOrdered)
                    {
                        return false;
                    }
                    else { 
                        prodottoNelDb.Quantity -= product.QuantityOrdered;
                        _dbContext.Products.Update(prodottoNelDb);
                        _dbContext.CartDetails.Remove(product);
                        cart.ClonedProducts.Remove(product);
                        _dbContext.CartProduct.Update(cart);

                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            await RemoveProductFromCart(idUtente);
            return true;
        }
        return false;
    }
}