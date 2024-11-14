using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class UserService(ApplicationDbContext context) : IUserService
{
    public async Task<User> GetUser()
    {
        var user = await context.Users
            .Select(x => new
            {
                User = x,
                TotalProductValue = x.Orders
                    .Where(y => y.Status == OrderStatus.Delivered && y.CreatedAt.Year == 2003)
                    .Sum(y => y.Price * y.Quantity)
            })
            .OrderByDescending(x => x.TotalProductValue)
            .Select(x => x.User)
            .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = await context.Users
            .Where(x => x.Orders.Any(y => y.Status == OrderStatus.Paid && y.CreatedAt.Year == 2010))
            .ToListAsync();

        return users;
    }
}