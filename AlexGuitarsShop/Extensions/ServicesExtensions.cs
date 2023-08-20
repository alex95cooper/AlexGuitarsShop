using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.DAL.Repositories;
using AlexGuitarsShop.Domain.Creators;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Providers;
using AlexGuitarsShop.Domain.Updaters;

namespace AlexGuitarsShop.Extensions;

public static class ServicesExtensions
{
    public static void InitializeRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IGuitarRepository, GuitarRepository>(_ =>
            new GuitarRepository(connectionString));
        services.AddTransient<IAccountRepository, AccountRepository>(_ =>
            new AccountRepository(connectionString));
        services.AddTransient<ICartItemRepository, CartItemRepository>(provider =>
            new CartItemRepository(connectionString,
                (provider ?? throw new ArgumentNullException(nameof(provider))).GetService<Cart>()));
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

    public static void InitializeAuthorizer(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IAuthorizer, ValidUserAuthorizer>();
    }
}