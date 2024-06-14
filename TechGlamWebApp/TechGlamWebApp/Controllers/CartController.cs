using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CartController : Controller
    {
        private readonly CartServices _cartServices;
        private readonly UserManager<User> _userManager;

        public CartController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _cartServices = new CartServices(dbContext);
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/CartIndex")]
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var cart = await _cartServices.GetCartAsync(userId);
            cart.ClonedProducts = await _cartServices.GetClonedProductsAsync(cart);
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
            var user = await _userManager.GetUserAsync(User);
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

        [HttpPost]
        [Route("RemoveProductFromCart")]
        public async Task<IActionResult> RemoveProductFromCart(Guid id)
        {
            if (id == Guid.Empty) { return View(); }
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            await _cartServices.RemoveProductFromCart(id, userId);

            return RedirectToAction("CartIndex", "Cart");
        }

        [HttpPost]
        [Route("EmptyCart")]
        public async Task<IActionResult> EmptyCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            await _cartServices.EmptyCart(userId);
            return RedirectToAction("CartIndex", "Cart");
        }

        [HttpGet]
        [Route("/SubmitCartProducts")]
        public async Task<IActionResult> SubmitProducts()
        {
            return View();
        }
        
        [HttpGet]
        [Route("/SubmissionError")]
        public async Task<IActionResult> SubmissionError() 
        {
            return View(); 
        }
        
        [HttpPost]
        [Route("/SubmitCartProducts")]
        public async Task<IActionResult> SubmitCartProducts(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            if (await _cartServices.SubmitProducts(userId)) 
            { 
                return RedirectToAction("SubmitProducts"); 
            }
            else 
            { 
                return View("SubmissionError"); 
            }
        }

        // The following commented methods are suggestions for frontend handling
        // for adding and removing products directly from the view:
        // [HttpPost]
        // [Route("/EmptyCart")]
        // public IActionResult EmptyCart()
        // {
        //     _CartServices.EmptyCart();
        //     return RedirectToAction("IndexList", "List"); // Redirect to the home page after emptying the cart
        // }

        // [HttpPost]
        // [Route("/SubmitProducts")]
        // public IActionResult SubmitProducts(List<Product> productsToSubmit)
        // {
        //     _CartServices.SubmitProducts(productsToSubmit);
        //     return RedirectToAction("Index", "Home");
        // }

        // [HttpPost]
        // [Route("list/addproducttolist")]
        // public IActionResult AddProductToList(Product newProduct)
        // {
        //     _CartServices.AddProductToList(newProduct);
        //     return RedirectToAction("Index", "Home");
        // }

        // [HttpPost]
        // [Route("list/removeproductfromlist")]
        // public IActionResult RemoveProductFromList(Guid productId)
        // {
        //     _CartServices.RemoveProductFromList(productId);
        //     return RedirectToAction("Index");
        // }

        // [HttpPost]
        // [Route("list/totalproductquantity")]
        // public IActionResult TotalProductQuantity()
        // {
        //     int totalProductQuantity = _CartServices.TotalProductQuantity();
        //     return View(totalProductQuantity);
        // }

        // [HttpPost]
        // [Route("list/calculatetotalprice")]
        // public IActionResult CalculateTotalPrice()
        // {
        //     decimal totalPrice = _CartServices.CalculateTotalPrice();
        //     return View(totalPrice);
        // }
    }
}
