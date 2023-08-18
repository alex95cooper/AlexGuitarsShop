using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;
using AlexGuitarsShop.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CatalogController : Controller
{
    private const int Limit = 10;

    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly Paginator _paginator;

    public CatalogController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarsUpdater guitarsUpdater,
        ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater, Cart cart)
    {
        _guitarsCreator = guitarsCreator ?? throw new ArgumentNullException(nameof(guitarsCreator));
        _guitarsProvider = guitarsProvider ?? throw new ArgumentNullException(nameof(guitarsProvider));
        _guitarsUpdater = guitarsUpdater ?? throw new ArgumentNullException(nameof(guitarsUpdater));
        _cartItemsCreator = cartItemsCreator ?? throw new ArgumentNullException(nameof(cartItemsCreator));
        _cartItemsProvider = cartItemsProvider ?? throw new ArgumentNullException(nameof(cartItemsProvider));
        _cartItemsUpdater = cartItemsUpdater ?? throw new ArgumentNullException(nameof(cartItemsUpdater));
        _paginator = new Paginator(Limit);
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var countResult = await _guitarsProvider.GetCountAsync()!;
        _paginator.SetPaginationValues(pageNumber, countResult);
        var result = await _guitarsProvider.GetGuitarsByLimitAsync(_paginator.OffSet, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        return View(_paginator.GetPaginatedList(
            result.Data, Title.Catalog, pageNumber));
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public IActionResult Add()
    {
        return View(new GuitarViewModel());
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _guitarsProvider.GetGuitarAsync(id)!;
        result = result ?? throw new ArgumentNullException(nameof(id));
        return View(result.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> Add(GuitarViewModel model)
    {
        model = model ?? throw new ArgumentNullException(nameof(model));
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsCreator.AddGuitarAsync(model)!;
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> Update(GuitarViewModel model)
    {
        model = model ?? throw new ArgumentNullException(nameof(model));
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsUpdater.UpdateGuitarAsync(model)!;
        return RedirectToAction("Index");
    }


    public async Task<RedirectToActionResult> AddToCart(int prodId)
    {
        var cartResult = await _cartItemsProvider.GetCartItemAsync(prodId)!;
        cartResult = cartResult ?? throw new ArgumentNullException(nameof(cartResult));
        if (cartResult.Data == null)
        {
            await AddNewItemToCart(prodId);
        }
        else
        {
            await _cartItemsUpdater.IncrementAsync(prodId)!;
        }

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _guitarsUpdater.DeleteGuitarAsync(id)!;
        return RedirectToAction("Index");
    }

    private async Task AddNewItemToCart(int prodId)
    {
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId)!;
        guitarResult = guitarResult ?? throw new ArgumentNullException(nameof(guitarResult));
        await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data);
    }
}