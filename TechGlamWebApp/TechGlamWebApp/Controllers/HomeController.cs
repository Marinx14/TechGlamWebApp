using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechGlamWebApp.Models;
using WebApp.Data;
using WebApp.Models;
using WebApp.CartService;
using WebApp.ProductServices;

namespace TechGlamWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _user;
        private readonly ProductServices _productServices; 
        private readonly CartServices _cartServices;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> user, AppDbContext dbContext, CartServices cartServices, UserManager<IdentityUser> userManager) 
        {
            _user = user;
            _logger = logger;
            _productServices = new ProductServices(dbContext);
            _cartServices = cartServices;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var totalProducts = await _productServices.GetProductsAsync(); 
            return View(totalProducts);
            
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/Cart")]
        public async Task<IActionResult> Cart()
        {
            return await Task.Run(() => View("~/Views/Cart/IndexCart.cshtml"));
        }

        [AllowAnonymous]
        public IActionResult Product()
        {
            return View("~/Views/Product/AddProduct.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}