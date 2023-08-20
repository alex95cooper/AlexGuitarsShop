using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Extensions;
using AlexGuitarsShop.ViewModels;
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

    public CatalogController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarsUpdater guitarsUpdater,
        ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
        ICartItemsUpdater cartItemsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        int offset = (pageNumber - 1) * Limit;
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        List<Guitar> list = (await _guitarsProvider.GetGuitarsByLimitAsync(offset, Limit)).Data;
        ListViewModel<Guitar> model = new ListViewModel<Guitar>
        {
            List = list, Title = Title.Catalog,
            TotalCount = count, CurrentPage = pageNumber
        };

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult Add()
    {
        return View(new GuitarViewModel());
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _guitarsProvider.GetGuitarAsync(id);
        GuitarViewModel model = result.Data.ToGuitarViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Add(GuitarViewModel model)
    {
        model = model ?? throw new ArgumentNullException(nameof(model));
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsCreator.AddGuitarAsync(model.ToGuitar());
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Update(GuitarViewModel model)
    {
        model = model ?? throw new ArgumentNullException(nameof(model));
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsUpdater.UpdateGuitarAsync(model.ToGuitar());
        return RedirectToAction("Index");
    }

    public async Task<RedirectToActionResult> AddToCart(int prodId)
    {
        var cartResult = await _cartItemsProvider.GetCartItemAsync(prodId);
        if (cartResult.Data == null)
        {
            await AddNewItemToCart(prodId);
        }
        else
        {
            await _cartItemsUpdater.IncrementAsync(prodId);
        }

        return RedirectToAction("Index");
    }

    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Delete(int id)
    {
        await _guitarsUpdater.DeleteGuitarAsync(id);
        return RedirectToAction("Index");
    }

    private async Task AddNewItemToCart(int prodId)
    {
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId);
        await _cartItemsCreator.AddNewCartItemAsync(guitarResult.Data);
    }
}