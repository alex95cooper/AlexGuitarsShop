using AlexGuitarsShop.Web.Domain.Creators;
using AlexGuitarsShop.Web.Domain.Interfaces.Account;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.Providers;
using AlexGuitarsShop.Web.Domain.Updaters;
using AlexGuitarsShop.Web.Domain.Validators;

namespace AlexGuitarsShop.Web.Extensions;

public static class ServicesExtensions
{
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
        services.AddTransient<IGuitarValidator, GuitarValidator>();
    }
}