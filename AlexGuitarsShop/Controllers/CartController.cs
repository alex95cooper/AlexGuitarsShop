using AlexGuitarsShop.DAL.Models;
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

    public CartController(ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, IGuitarsProvider guitarsProvider, IAccountsProvider accountProvider,
        ICartItemValidator cartItemValidator, IGuitarValidator guitarValidator)
    {
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _guitarsProvider = guitarsProvider;
        _accountProvider = accountProvider;
        _cartItemValidator = cartItemValidator;
        _guitarValidator = guitarValidator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int id = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        List<CartItem> cart = _cartItemsProvider.GetCartItemsAsync(id).Result.Data;
        return View(cart);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int id, int currentPage)
    {
        if (!_guitarValidator.CheckIfGuitarExist(id).Result)
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidGuitarId;
            return View("Notification");
        }

        if (GetCartItem(id) == null)
        {
            await AddNewItemToCart(id);
        }
        else
        {
            int accountId = _accountProvider.GetId(User.Identity!.Name).Result.Data;
            await _cartItemsUpdater.IncrementAsync(id, accountId);
        }

        return RedirectToAction("Index", "Guitar", new {pageNumber = currentPage});
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        int accountId = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        if (!await _cartItemValidator.CheckIfCartItemExist(id, accountId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.RemoveAsync(id, accountId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        int accountId = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        if (!await _cartItemValidator.CheckIfCartItemExist(id, accountId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.IncrementAsync(id, accountId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        int accountId = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        if (!await _cartItemValidator.CheckIfCartItemExist(id, accountId))
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidProductId;
            return View("Notification");
        }

        await _cartItemsUpdater.DecrementAsync(id, accountId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Order()
    {
        int id = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        await _cartItemsUpdater.OrderAsync(id);
        ViewBag.Message = ThanksMessage;
        return View("Notification");
    }

    private CartItem GetCartItem(int id)
    {
        int accountId = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        return _cartItemsProvider.GetCartItemAsync(id, accountId).Result.Data;
    }

    private async Task AddNewItemToCart(int prodId)
    {
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId);
        int id = _accountProvider.GetId(User.Identity!.Name).Result.Data;
        await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data, id);
    }
}