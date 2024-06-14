using WebApp.Data;
using WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Select(o => new OrderViewModel
                {
                    OrderID = o.OrderID,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount
                })
                .ToListAsync();
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Where(o => o.OrderID == id)
                .Select(o => new OrderViewModel
                {
                    OrderID = o.OrderID,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount
                })
                .FirstOrDefaultAsync();
        }
    }

    public interface IOrderService
    {
    }
}
