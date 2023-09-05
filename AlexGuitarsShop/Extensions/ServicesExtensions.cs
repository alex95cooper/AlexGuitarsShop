using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Repositories;
using AlexGuitarsShop.Domain.Creators;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Providers;
using AlexGuitarsShop.Domain.Updaters;
using AlexGuitarsShop.Domain.Validators;

namespace AlexGuitarsShop.Extensions;

public static class ServicesExtensions
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGuitarRepository, GuitarRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ICartItemRepository, CartItemRepository>();
    }

    public static void InitializeEntityHandlers(this IServiceCollection services)
    {
        services.AddTransient<IAccountsCreator, AccountsCreator>();
        services.AddTransient<IAccountsProvider, AccountsProvider>();
        services.AddTransient<IAccountsUpdater, AccountsUpdater>();
        services.AddTransient<ICartItemsCreator, CartItemsCreator>();
        services.AddTransient<ICartItemsProvider, CartItemsProvider>();
        services.AddTransient<ICartItemsUpdater, CartItemsUpdater>();
        services.AddTransient<IGuitarsCreator, GuitarsCreator>();
        services.AddTransient<IGuitarsProvider, GuitarsProvider>();
        services.AddTransient<IGuitarsUpdater, GuitarsUpdater>();
    }

    public static void InitializeValidators(this IServiceCollection services)
    {
        services.AddTransient<IAccountValidator, AccountValidator>();
        services.AddTransient<ICartItemValidator, CartItemValidator>();
        services.AddTransient<IGuitarValidator, GuitarValidator>();
    }
}