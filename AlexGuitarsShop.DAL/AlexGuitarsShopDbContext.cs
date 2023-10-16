using AlexGuitarsShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexGuitarsShop.DAL;

public class AlexGuitarsShopDbContext : DbContext
{
    public AlexGuitarsShopDbContext(DbContextOptions<AlexGuitarsShopDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Account { get; set; }
    public DbSet<Guitar> Guitar { get; set; }
    public DbSet<CartItem> CartItem { get; set; }
}