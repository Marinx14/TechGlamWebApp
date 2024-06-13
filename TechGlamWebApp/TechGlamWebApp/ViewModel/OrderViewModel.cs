using System;

namespace WebApp.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderID { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}