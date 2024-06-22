using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using WebApp.Data;
using System.Linq;
using WebApp.enumeration;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Image = p.Image,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                Category = p.Category.ToString(),
                Color = p.Color,
                SizeRingsBracelets = (p.Category == WebAppEnum.Category.Rings || p.Category == WebAppEnum.Category.Bracelets) ? p.SizeRingsBracelets : (WebAppEnum.SizeRingsBracelets?)null,
                SizeWatches = (p.Category == WebAppEnum.Category.Watches) ? p.SizeWatches : (WebAppEnum.SizeWatches?)null,
                MetalType = p.MetalType
            }).ToList(); // Ensure IQueryable is converted to List

            return View(products);
        }

        public IActionResult Details(Guid id)
        {
            var product = _context.Products.Where(p => p.ProductID == id).Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Image = p.Image,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                Category = p.Category.ToString(),
                Color = p.Color,
                SizeRingsBracelets = (p.Category == WebAppEnum.Category.Rings || p.Category == WebAppEnum.Category.Bracelets) ? p.SizeRingsBracelets : (WebAppEnum.SizeRingsBracelets?)null,
                SizeWatches = (p.Category == WebAppEnum.Category.Watches) ? p.SizeWatches : (WebAppEnum.SizeWatches?)null,
                MetalType = p.MetalType
            }).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
