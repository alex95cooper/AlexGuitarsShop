using AlexGuitarsShop.Common.Models;
using AlexGuitarsShop.Web.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Web.Controllers;

public class CartController : Controller
{
    private const string ThanksMessage = "Thank you for your purchase!";

    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarValidator _guitarValidator;

    public CartController(ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator)
    {
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _guitarsProvider = guitarsProvider;
        _guitarValidator = guitarValidator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<CartItem> cart = _cartItemsProvider.GetCartAsync().Result.Data;
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

        if (!_cartItemsProvider.GetCartItemAsync(id).Result.IsSuccess)
        {
            var guitarResult = await _guitarsProvider.GetGuitarAsync(id);
            await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data);
        }
        else
        {
            await _cartItemsUpdater.IncrementAsync(id);
        }

        return RedirectToAction("Index", "Guitar", new {pageNumber = currentPage});
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await _cartItemsUpdater.RemoveAsync(id);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        var result = await _cartItemsUpdater.IncrementAsync(id);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        var result = await _cartItemsUpdater.DecrementAsync(id);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Order()
    {
        await _cartItemsUpdater.OrderAsync();
        ViewBag.Message = ThanksMessage;
        return View("Notification");
    }
}