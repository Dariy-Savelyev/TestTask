using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    public async Task<Order> GetOrder()
    {
        var order = await context.Orders
            .Where(x => x.Quantity > 1)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        return order!;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await context.Orders
            .Where(x => Equals(x.User.Status, UserStatus.Active))
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
        
        return orders;
    }
}