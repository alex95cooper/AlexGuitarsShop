using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CartSessionController : Controller
{
    private const string ThanksMessage = "Thank you for your purchase!";

    private readonly ICartItemsSessionCreator _cartItemsCreator;
    private readonly ICartItemsSessionProvider _cartItemsProvider;
    private readonly ICartItemsSessionUpdater _cartItemsUpdater;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarValidator _guitarValidator;

    public CartSessionController(ICartItemsSessionCreator cartItemsCreator, ICartItemsSessionProvider cartItemsProvider,
        ICartItemsSessionUpdater cartItemsUpdater, IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator)
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
        List<CartItem> cartSession = _cartItemsProvider.GetCart();
        return View("/Views/Cart/Index.cshtml", cartSession);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int id, int currentPage)
    {
        if (!_guitarValidator.CheckIfGuitarExist(id).Result)
        {
            ViewBag.Message = Constants.ErrorMessages.InvalidGuitarId;
            return View("Notification");
        }

        if (_cartItemsProvider.GetCartItem(id) == null)
        {
            await AddNewItemToCart(id);
        }
        else
        {
            List<CartItem> cart = _cartItemsProvider.GetCart();
            _cartItemsUpdater.Increment(id, cart);
        }

        return RedirectToAction("Index", "Guitar", new {pageNumber = currentPage});
    }

    [HttpGet]
    public IActionResult Remove(int id)
    {
        List<CartItem> cart = _cartItemsProvider.GetCart();
        _cartItemsUpdater.Remove(id, cart);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Increment(int id)
    {
        List<CartItem> cart = _cartItemsProvider.GetCart();
        _cartItemsUpdater.Increment(id, cart);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Decrement(int id)
    {
        List<CartItem> cart = _cartItemsProvider.GetCart();
        _cartItemsUpdater.Decrement(id, cart);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Order()
    {
        _cartItemsUpdater.Order();
        ViewBag.Message = ThanksMessage;
        return View("Notification");
    }

    private async Task AddNewItemToCart(int prodId)
    {
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId);
        List<CartItem> cart = _cartItemsProvider.GetCart();
        _cartItemsCreator.AddCartItem(guitarResult.Data, cart);
    }
}