    namespace WebApp.ViewModels

{
    public class CartViewModel
    {
        public Guid CartID { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public decimal TotalPrice { get; set; }
    }
}
