using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartItemService _service;
    private readonly Cart _cart;

    public CartController(ICartItemService service, Cart cart)
    {
        _service = service;
        _cart = cart;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _service.GetCartItems();
        if (response.StatusCode == Domain.Enums.StatusCode.OK)
        {
            _cart.Products = response.Data;
            return View(_cart);
        }

        ViewBag.Message = "Cart is empty";
        return View("Notification");
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        await _service.Remove(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        await _service.Increment(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        await _service.Decrement(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Order(int id)
    {
        await _service.Order();
        ViewBag.Message = "Thank you for your purchase!";
        return View("Notification");
    }
}