using WebApp.Data;

namespace WebApp.CartService;

public class CartServices
{
    public CartServices(AppDbContext dbContext)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> GetCartAsync(string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveProductFromCart(Guid id, string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task EmptyCart(string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddProductToCart(object userId, object product, int quantity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SubmitProducts(string? userId)
    {
        throw new NotImplementedException();
    }
}