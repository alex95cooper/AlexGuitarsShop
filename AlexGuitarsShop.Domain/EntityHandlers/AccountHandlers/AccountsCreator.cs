using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.AccountHandlers;

public class AccountsCreator : IAccountsCreator
{
    private readonly IUserRepository _userRepository;

    public AccountsCreator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResponse<User>> AddAccountAsync(RegisterViewModel model)
    {
        var user = await _userRepository!.GetUserByEmailAsync(model!.Email!)!;
        if (user != null)
        {
            return GetInvalidResponse();
        }

        user = model.ToUser();
        await _userRepository.AddAsync(user)!;
        return GetValidResponse(user);
    }

    private static IResponse<User> GetInvalidResponse()
    {
        return new Response<User>
        {
            StatusCode = StatusCode.ClientError,
            Description = "User with this e-mail already exists"
        };
    }

    private static IResponse<User> GetValidResponse(User user)
    {
        return new Response<User>
        {
            StatusCode = StatusCode.Ok,
            Description = "User with this e-mail already exists",
            Data = user
        };
    }
}