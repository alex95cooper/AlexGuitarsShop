using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.EntityHandlers.AccountHandlers;

public class AccountsCreator : IAccountsCreator
{
    private const string ExistEmailErrorMessage = "User with this e-mail already exists";
    
        
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
            return ResponseCreator.GetInvalidResponse<User>(ExistEmailErrorMessage);
        }

        user = model.ToUser();
        await _userRepository.AddAsync(user)!;
        return ResponseCreator.GetValidResponse(user);
    }
    
}