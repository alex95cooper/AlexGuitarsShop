using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly Cart _cart;

    public CartController(ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, Cart cart)
    {
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _cart = cart;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (_cartItemsProvider == null) throw new ArgumentNullException(nameof(_cartItemsProvider));
        if (_cart == null) throw new ArgumentNullException(nameof(_cart));
        var result = await _cartItemsProvider.GetCartItemsAsync()!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        _cart.Products = result.Data;
        return View(_cart);
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        if (_cartItemsUpdater == null) throw new ArgumentNullException(nameof(_cartItemsUpdater));
        await _cartItemsUpdater.RemoveAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        if (_cartItemsUpdater == null) throw new ArgumentNullException(nameof(_cartItemsUpdater));
        await _cartItemsUpdater.IncrementAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        if (_cartItemsUpdater == null) throw new ArgumentNullException(nameof(_cartItemsUpdater));
        await _cartItemsUpdater.DecrementAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Order(int id)
    {
        if (_cartItemsUpdater == null) throw new ArgumentNullException(nameof(_cartItemsUpdater));
        await _cartItemsUpdater.OrderAsync()!;
        ViewBag.Message = "Thank you for your purchase!";
        return View("Notification");
    }
}