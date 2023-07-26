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
        var response = await _cartItemsProvider!.GetCartItemsAsync()!;
        if (response is {StatusCode: Domain.StatusCode.Ok})
        {
            _cart!.Products = response.Data;
            return View(_cart);
        }

        ViewBag.Message = "Cart is empty";
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        await _cartItemsUpdater!.RemoveAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        await _cartItemsUpdater!.IncrementAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        await _cartItemsUpdater!.DecrementAsync(id)!;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Order(int id)
    {
        await _cartItemsUpdater!.OrderAsync()!;
        ViewBag.Message = "Thank you for your purchase!";
        return View("Notification");
    }
}