using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexGuitarsShop.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AlexGuitarsShopDbContext _db;

    public AccountRepository(AlexGuitarsShopDbContext db)
    {
        _db = db;
    }

    public async Task<Account> FindAsync(string email)
    {
        return await _db.Account.FirstOrDefaultAsync(account => account.Email == email);
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await _db.Account.CountAsync(account => account.Role == Role.User);
    }

    public async Task<int> GetAdminsCountAsync()
    {
        return await _db.Account.CountAsync(account => account.Role == Role.Admin);
    }

    public async Task<List<Account>> GetUsersAsync(int offset, int limit)
    {
        return await _db.Account
            .Where(account => account.Role == Role.User)
            .Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<List<Account>> GetAdminsAsync(int offset, int limit)
    {
        return await _db.Account
            .Where(account => account.Role == Role.Admin)
            .Skip(offset).Take(limit).ToListAsync();
    }

    public async Task CreateAsync(Account account)
    {
        await _db.Account.AddAsync(account);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(string email, int role)
    {
        Account account = await FindAsync(email);
        if (account != null)
        {
            account.Role = (Role)role;
            _db.Account.Update(account);
            await _db.SaveChangesAsync();
        }
    }
}