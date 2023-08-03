using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.ViewModels;

namespace AlexGuitarsShop.Domain.BLLClasses.Creators;

public class AccountsCreator : IAccountsCreator
{
    private const string ExistEmailErrorMessage = "User with this e-mail already exists";
    
        
    private readonly IUserRepository _userRepository;

    public AccountsCreator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult<User>> AddAccountAsync(RegisterViewModel model)
    {
        if (_userRepository == null) throw new ArgumentNullException(nameof(_userRepository));
        if (model == null) throw new ArgumentNullException(nameof(model));
        var user = await _userRepository.FindAsync(model.Email)!;
        if (user != null)
        {
            return ResultCreator.GetInvalidResult<User>(ExistEmailErrorMessage);
        }

        user = model.ToUser();
        await _userRepository.CreateAsync(user)!;
        return ResultCreator.GetValidResult(user);
    }
    
}