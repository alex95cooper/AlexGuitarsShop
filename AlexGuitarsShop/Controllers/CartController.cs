using AlexGuitarsShop.Domain.Interfaces.Account;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CartController : Controller
{
    private const string ThanksMessage = "Thank you for your purchase!";
    
    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IAccountsProvider _accountProvider;
    private readonly ICartItemValidator _cartItemValidator;
    private readonly IGuitarValidator _guitarValidator;
    private readonly Cart _cart;

    public CartController(ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, IGuitarsProvider guitarsProvider, IAccountsProvider accountProvider,
        ICartItemValidator cartItemValidator, IGuitarValidator guitarValidator, Cart cart)
    {
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _guitarsProvider = guitarsProvider;
        _accountProvider = accountProvider;
        _cartItemValidator = cartItemValidator;
        _guitarValidator = guitarValidator;
        _cart = cart;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string cartId = GetCartId();
        var result = await _cartItemsProvider.GetCartItemsAsync(cartId);
        _cart.Products = result.Data;
        return View(_cart);
    }

    [HttpGet]
    public async Task<IActionResult> AddToCart(int prodId)
    {
        if (!_guitarValidator.CheckIfGuitarExist(prodId).Result)
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidGuitarId;
            return View("Notification");
        }

        string cartId = GetCartId();
        var cartResult = await _cartItemsProvider.GetCartItemAsync(prodId, cartId);
        if (cartResult.Data == null)
        {
            await AddNewItemToCart(prodId);
        }
        else
        {
            await _cartItemsUpdater.IncrementAsync(prodId, cartId);
        }

        return RedirectToAction("Index", "Catalog");
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        string cartId = GetCartId();
        if (!await _cartItemValidator.CheckIfCartItemExist(id, cartId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.RemoveAsync(id, cartId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        string cartId = GetCartId();
        if (!await _cartItemValidator.CheckIfCartItemExist(id, cartId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.IncrementAsync(id, cartId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        string cartId = GetCartId();
        if (!await _cartItemValidator.CheckIfCartItemExist(id, cartId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.DecrementAsync(id, cartId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Order()
    {
        string cartId = GetCartId();
        await _cartItemsUpdater.OrderAsync(cartId);
        ViewBag.Message = ThanksMessage;
        return View("Notification");
    }

    private async Task AddNewItemToCart(int prodId)
    {
        string cartId = GetCartId();
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId);
        await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data, cartId);
    }

    private string GetCartId()
    {
        if (User.Identity is {IsAuthenticated: true})
        {
            string email = User.Identity.Name;
            return _accountProvider.GetCartId(email).Result.Data;
        }

        return _cart.Id;
    }
}