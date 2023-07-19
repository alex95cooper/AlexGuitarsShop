using AlexGuitarsShop.Domain.Models;

namespace AlexGuitarsShop.DAL.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string email);

    Task<List<User>> GetUsersByLimit(int offset, int limit);

    Task<List<User>> GetAdminsByLimit(int offset, int limit);

    Task<int> GetUsersCount();

    Task<int> GetAdminsCount();

    Task SetAdminRights(string email);

    Task RemoveAdminRights(string email);
}