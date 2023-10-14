using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexGuitarsShop.DAL.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AlexGuitarsShopDbContext _db;

    public CartItemRepository(AlexGuitarsShopDbContext db)
    {
        _db = db;
    }

    public async Task<CartItem> FindAsync(int id, int accountId)
    {
        return await _db.CartItem
            .Where(x => x.AccountId == accountId)
            .Include(x => x.Product)
            .Where(cartItem => cartItem.Product.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetProductQuantityAsync(int id, int accountId)
    {
        CartItem item = await FindAsync(id, accountId);
        return item?.Quantity ?? 0;
    }

    public async Task<List<CartItem>> GetAllAsync(int accountId)
    {
        return await _db.CartItem
            .Where(cartItem => cartItem.AccountId == accountId)
            .Include(cartItem => cartItem.Product)
            .Where(cartItem => cartItem.Product.IsDeleted == 0).ToListAsync();
    }

    public async Task CreateAsync(CartItem item, int accountId)
    {
        item.AccountId = accountId;
        await _db.CartItem.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateQuantityAsync(int id, int accountId, int quantity)
    {
        CartItem item = await FindAsync(id, accountId);
        if (item != null)
        {
            item.Quantity = quantity;
            _db.CartItem.Update(item);
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id, int accountId)
    {
        CartItem item = await FindAsync(id, accountId);
        _db.CartItem.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAllAsync(int accountId)
    {
        List<CartItem> items = await GetAllAsync(accountId);
        _db.CartItem.RemoveRange(items);
        await _db.SaveChangesAsync();
    }
}