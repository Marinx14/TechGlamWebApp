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
            User? User = null;
            User = await _UserManager.GetUserAsync(User);
            var UserId = User?.Id;
            var cart = await _cartServices.GetCartAsync(UserId);
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
            var User = await _UserManager.GetUserAsync(User);
            var UserId = User?.Id;
            object product = await _cartServices.GetProductByIdAsync(id);
            if (product != null)
            {
                await _cartServices.AddProductToCart(UserId, product, quantity);
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
            var User = await _UserManager.GetUserAsync(User);
            var UserId = User?.Id;
            await _cartServices.RemoveProductFromCart(id, UserId);

            return RedirectToAction("CartIndex", "Cart");
        }

        [HttpPost]
        [Route("EmptyCart")]
        public async Task<IActionResult> EmptyCart()
        {
            var User = await _UserManager.GetUserAsync(User);
            var UserId = User?.Id;
            await _cartServices.EmptyCart(UserId);
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
            var User = await _UserManager.GetUserAsync(User);
            var UserId = User?.Id;
            if (await _cartServices.SubmitProducts(UserId)) 
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
