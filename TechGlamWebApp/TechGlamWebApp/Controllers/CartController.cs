using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.CartService;

namespace TechGlamWebApp.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CartController : Controller
    {
        private readonly CartServices _cartServices;
        private readonly UserManager<User> _UserManager;

        public CartController(AppDbContext dbContext, UserManager<User> UserManager)
        {
            _cartServices = new CartServices(dbContext);
            _UserManager = UserManager;
        }

        [HttpGet]
        [Route("/CartIndex")]
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var user = await _UserManager.GetUserAsync(User);
            var userId = user?.Id;
            var cart = await _cartServices.GetCartAsync(userId);
            /*cart.ClonedProducts = await _cartServices.GetClonedProductsAsync(cart);*/
            return View(cart);
        }

        [HttpGet]
        [Route("/GetCartProducts")]
        public async Task<ActionResult<List<Product>>> GetCartProductsAsync(string id)
        {
            var products = await _cartServices.GetCartAsync(id);
            return View(products);
        }

        [HttpGet]
        [Route("AddProductToCart")]
        [Authorize]
        public async Task<IActionResult> AddProductToCart()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("AddProductToCart")]
        [Authorize]
        public async Task<IActionResult> AddProductToCart(Guid id, int quantity = 1)
        {
            var user = await _UserManager.GetUserAsync(User);
            var userId = user?.Id;
            var product = await _cartServices.GetProductByIdAsync(id);
            if (product != null)
            {
                await _cartServices.AddProductToCart(userId, product, quantity);
            }
            return RedirectToAction("CartIndex");
        }
        
        [HttpGet]
        [Route("RemoveProductFromCart")]
        public async Task<IActionResult> RemoveProductFromCart()
        {
            return View();
        }

    }
}
/*[HttpPost]
[Route("RemoveProductFromCart")]
public Task<IActionResult> RemoveProductFromCart(Guid id)*/
