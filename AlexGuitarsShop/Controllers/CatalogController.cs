using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CatalogController : Controller
{
    const int Limit = 10;

    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly ICartItemsCreator _cartItemsCreator;
    private readonly ICartItemsProvider _cartItemsProvider;
    private readonly ICartItemsUpdater _cartItemsUpdater;
    private readonly Cart _cart;

    private int _pageCount;
    private int _offset;

    public CatalogController(IGuitarsCreator guitarsCreator, 
        IGuitarsProvider guitarsProvider, IGuitarsUpdater guitarsUpdater,
        ICartItemsCreator cartItemsCreator, ICartItemsProvider cartItemsProvider,
    ICartItemsUpdater cartItemsUpdater, Cart cart)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _cartItemsCreator = cartItemsCreator;
        _cartItemsProvider = cartItemsProvider;
        _cartItemsUpdater = cartItemsUpdater;
        _cart = cart;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        _offset = (pageNumber - 1) * Limit;
        int count = ((await _guitarsProvider!.GetCountAsync()!)!).Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var response = await _guitarsProvider.GetGuitarsByLimitAsync(_offset, Limit)!;
        CatalogViewModel catalog = new CatalogViewModel
        {
            Guitars = response!.Data, PageCount = _pageCount, CurrentPage = pageNumber
        };
        return View(catalog);
    }

    public async Task<IActionResult> AddOrUpdate(int id)
    {
        if (id == 0)
        {
            return View(new GuitarViewModel {Id = 0});
        }

        var response = await _guitarsProvider!.GetGuitarAsync(id)!;
        return View(response!.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate(GuitarViewModel model)
    {
        if (model!.Avatar != null)
        {
            using var binaryReader = new BinaryReader(model.Avatar.OpenReadStream());
            model.Image = binaryReader.ReadBytes((int) model.Avatar.Length);
        }
        else
        {
            var response = await _guitarsProvider!.GetGuitarAsync(model.Id)!;
            model.Image = response!.Data!.Image;
        }

        if (model.Id == 0)
        {
            await _guitarsCreator!.AddGuitarAsync(model)!;
        }
        else
        {
            await _guitarsUpdater!.UpdateGuitarAsync(model)!;
        }

        return RedirectToAction("Index");
    }

    public async Task<RedirectToActionResult> AddToCart(int prodId)
    {
        if (_guitarsProvider == null || _cartItemsProvider ==null) return RedirectToAction("Index");
        var guitarResponse = await _guitarsProvider.GetGuitarAsync(prodId)!;
        Guitar product = guitarResponse!.Data.ToGuitar();
        var cartResponse = await _cartItemsProvider.GetCartItemAsync(prodId)!;
        if (cartResponse!.StatusCode == Domain.StatusCode.Ok)
        {
            await _cartItemsUpdater!.IncrementAsync(prodId)!;
        }
        else
        {
            CartItem item = new CartItem {Product = product, Quantity = 1};
            if (product!.IsDeleted == 0 && !_cart!.Products!.Contains(item))
            {
                _cart.Products.Add(item);
                await _cartItemsCreator!.AddCartItemAsync(item)!;
            }
        }
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _guitarsUpdater!.DeleteGuitarAsync(id)!;
        return RedirectToAction("Index");
    }
}