using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface IUserRepository
{
    Task<User> FindAsync(string email);
    
    Task<int> GetUsersCountAsync();

    Task<int> GetAdminsCountAsync();
    
    Task<List<User>> GetUsersAsync(int offset, int limit);

    Task<List<User>> GetAdminsAsync(int offset, int limit);

    Task CreateAsync(User user);

    Task UpdateAsync(string email, int role);
}