using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechGlamWebApp.Models;
using WebApp.Data;
using WebApp.Models;
using TechGlamWebApp.Services; 

namespace TechGlamWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _user;
        private readonly ProductServices _productService;
        private readonly CartServices _cartServices;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> user, AppDbContext dbContext)
        {
            _user = user;
            _logger = logger;
            _productService = new ProductServices(dbContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var totalProducts = await _productService.GetProductsAsync();
            return View(totalProducts);

        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/Carrello")]
               public async Task<IActionResult> Cart()
        {
            return View("~/Views/Carrello/IndexCart.cshtml");
        }

        [AllowAnonymous]
        public IActionResult Product()
        {
            return View("~/Views/Prodotto/AddProduct.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}