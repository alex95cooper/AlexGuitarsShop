using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface IAccountRepository
{
    Task<Account> FindAsync(string email);
    
    Task<int> GetUsersCountAsync();

    Task<int> GetAdminsCountAsync();
    
    Task<List<Account>> GetUsersAsync(int offset, int limit);

    Task<List<Account>> GetAdminsAsync(int offset, int limit);

    Task CreateAsync(Account account);

    Task UpdateAsync(string email, int role);
}