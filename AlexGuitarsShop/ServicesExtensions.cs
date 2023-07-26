using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.DAL.Repositories;
using AlexGuitarsShop.Domain.EntityHandlers.AccountHandlers;
using AlexGuitarsShop.Domain.EntityHandlers.CartItemsHandlers;
using AlexGuitarsShop.Domain.EntityHandlers.GuitarsHandlers;
using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;

namespace AlexGuitarsShop;

public static class ServicesExtensions
{
    public static void InitializeRepositories(this IServiceCollection services, string connectionString)
    {
        if (services == null) return;
        services.AddTransient<IRepository<Guitar>, GuitarRepository>(_ =>
            new GuitarRepository(connectionString));
        services.AddTransient<IUserRepository, UserRepository>(_ =>
            new UserRepository(connectionString));
        services.AddTransient<ICartItemRepository, CartItemRepository>(provider =>
            new CartItemRepository(connectionString, provider!.GetService<Cart>()));
    }

    public static void InitializeEntityHandlers(this IServiceCollection services)
    {
        if (services == null) return;
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
}