using WebApp.enumeration;

namespace WebApp.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Category { get; set; }
        public string? Color { get; set; }
        public WebAppEnum.SizeRingsBracelets? SizeRingsBracelets { get; set; }
        public WebAppEnum.SizeWatches? SizeWatches { get; set; } // Nullable for watches only
        public string? MetalType { get; set; }
    }
}
