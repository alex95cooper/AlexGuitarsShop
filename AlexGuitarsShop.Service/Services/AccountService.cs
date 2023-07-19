using System.Security.Claims;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Enums;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.Responses;
using AlexGuitarsShop.Domain.ViewModels;
using AlexGuitarsShop.Service.Interfaces;

namespace AlexGuitarsShop.Service.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        var user = await _userRepository.GetUserByEmail(model.Email);
        if (user != null)
        {
            return new Response<ClaimsIdentity>
            {
                Description = "User with this e-mail already exists"
            };
        }

        user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Role = Role.User,
            Password = PasswordHasher.HashPassword(model.Password),
        };

        await _userRepository.Add(user);
        var result = Authenticate(user);

        return new Response<ClaimsIdentity>()
        {
            Data = result,
            Description = "Object added",
            StatusCode = StatusCode.OK
        };
    }

    public async Task<IResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        var user = await _userRepository.GetUserByEmail(model.Email);
        if (user == null)
        {
            return new Response<ClaimsIdentity>
            {
                Description = "User is not found"
            };
        }

        if (user.Password != PasswordHasher.HashPassword(model.Password))
        {
            return new Response<ClaimsIdentity>
            {
                StatusCode = StatusCode.InternalServerError,
                Description = "Invalid password or login"
            };
        }

        var result = Authenticate(user);

        return new Response<ClaimsIdentity>()
        {
            Data = result,
            StatusCode = StatusCode.OK
        };
    }

    public async Task<IResponse<List<User>>> GetUsersOnly(int offset, int limit)
    {
        var userList = await _userRepository.GetUsersByLimit(offset, limit);
        if (userList.Count == 0)
        {
            return new Response<List<User>>
            {
                Description = "No users"
            };
        }

        return new Response<List<User>>
        {
            Data = userList,
            StatusCode = StatusCode.OK
        };
    }

    public async Task<IResponse<List<User>>> GetAdmins(int offset, int limit)
    {
        var userList = await _userRepository.GetAdminsByLimit(offset, limit);
        if (userList.Count == 0)
        {
            return new Response<List<User>>
            {
                Description = "No admins"
            };
        }

        return new Response<List<User>>
        {
            Data = userList,
            StatusCode = StatusCode.OK
        };
    }

    public async Task<IResponse<int>> GetUsersCount()
    {
        int count = await _userRepository.GetUsersCount();
        return new Response<int>
        {
            Data = count,
            StatusCode = StatusCode.OK
        };
    }

    public async Task<IResponse<int>> GetAdminsCount()
    {
        int count = await _userRepository.GetAdminsCount();
        return new Response<int>
        {
            Data = count,
            StatusCode = StatusCode.OK
        };
    }

    public async Task SetAdminRights(string email)
    {
        await _userRepository.SetAdminRights(email);
    }

    public async Task RemoveAdminRights(string email)
    {
        await _userRepository.RemoveAdminRights(email);
    }

    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}