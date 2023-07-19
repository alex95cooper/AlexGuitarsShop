using System.Security.Claims;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Service.Interfaces;

public interface IAccountService
{
    Task<IResponse<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<IResponse<ClaimsIdentity>> Login(LoginViewModel model);

    Task<IResponse<List<User>>> GetUsersOnly(int offset, int limit);
    
    Task<IResponse<List<User>>> GetAdmins(int offset, int limit);

    Task SetAdminRights(string email);

    Task RemoveAdminRights(string email);
    
    Task<IResponse<int>> GetUsersCount();
    
    Task<IResponse<int>> GetAdminsCount();
}