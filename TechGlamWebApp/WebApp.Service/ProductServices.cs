using WebApp.Data;

namespace WebApp.Service;

public class ProductServices(AppDbContext dbContext)
{
    public async Task<string?> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}