using AlexGuitarsShop.DAL.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);

    Task<List<User>> GetUsersByLimitAsync(int offset, int limit);

    Task<List<User>> GetAdminsByLimitAsync(int offset, int limit);

    Task<int> GetUsersCountAsync();

    Task<int> GetAdminsCountAsync();

    Task SetAdminRightsAsync(string email);

    Task RemoveAdminRightsAsync(string email);
}