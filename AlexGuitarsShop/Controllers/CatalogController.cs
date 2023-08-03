using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Extensions;
using AlexGuitarsShop.Domain.Interfaces.CartItem;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.ViewModels;
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
        if (_guitarsProvider == null) throw new ArgumentNullException(nameof(_guitarsProvider));
        _offset = (pageNumber - 1) * Limit;
        var countResult = await _guitarsProvider.GetCountAsync()!;
        int count = (countResult ?? throw new ArgumentNullException(nameof(countResult))).Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var result = await _guitarsProvider.GetGuitarsByLimitAsync(_offset, Limit)!;
        result = result ?? throw new ArgumentNullException(nameof(result));
        ListViewModel<Guitar> catalog = new(result.Data, Title.Catalog, _pageCount, pageNumber);
        return View(catalog);
    }


    public IActionResult Add()
    {
        return View(new GuitarViewModel());
    }

    public async Task<IActionResult> Update(int id)
    {
        if (_guitarsProvider == null) throw new ArgumentNullException(nameof(_guitarsProvider));
        var result = await _guitarsProvider.GetGuitarAsync(id)!;
        result = result ?? throw new ArgumentNullException(nameof(id));
        return View(result.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(GuitarViewModel model)
    {
        if (_guitarsCreator == null) throw new ArgumentNullException(nameof(_guitarsCreator));
        model = model ?? throw new ArgumentNullException(nameof(model));
        if (model.Avatar != null)
        {
            using var binaryReader = new BinaryReader(model.Avatar.OpenReadStream());
            model.Image = binaryReader.ReadBytes((int) model.Avatar.Length);
        }

        await _guitarsCreator.AddGuitarAsync(model)!;
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(GuitarViewModel model)
    {
        if (_guitarsProvider == null) throw new ArgumentNullException(nameof(_guitarsProvider));
        if (_guitarsUpdater == null) throw new ArgumentNullException(nameof(_guitarsUpdater));
        model = model ?? throw new ArgumentNullException(nameof(model));
        if (model.Avatar != null)
        {
            using var binaryReader = new BinaryReader(model.Avatar.OpenReadStream());
            model.Image = binaryReader.ReadBytes((int) model.Avatar.Length);
        }
        else
        {
            var result = await _guitarsProvider.GetGuitarAsync(model.Id)!;
            result = result ?? throw new ArgumentNullException(nameof(result));
            model.Image = (result.Data
                           ?? throw new ArgumentNullException(nameof(result.Data))).Image;
        }

        await _guitarsUpdater.UpdateGuitarAsync(model)!;
        return RedirectToAction("Index");
    }


    public async Task<RedirectToActionResult> AddToCart(int prodId)
    {
        if (_cartItemsProvider == null) throw new ArgumentNullException(nameof(_cartItemsProvider));
        if (_cartItemsUpdater == null) throw new ArgumentNullException(nameof(_cartItemsUpdater));
        var cartResult = await _cartItemsProvider.GetCartItemAsync(prodId)!;
        cartResult = cartResult ?? throw new ArgumentNullException(nameof(cartResult));
        if (cartResult.Data == null)
        {
            await AddNewItemTCart(prodId);
        }
        else
        {
            await _cartItemsUpdater.IncrementAsync(prodId)!;
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (_guitarsUpdater == null) throw new ArgumentNullException(nameof(_guitarsUpdater));
        await _guitarsUpdater.DeleteGuitarAsync(id)!;
        return RedirectToAction("Index");
    }

    private async Task AddNewItemTCart(int prodId)
    {
        if (_cartItemsCreator == null) throw new ArgumentNullException(nameof(_cartItemsCreator));
        if (_guitarsProvider == null) throw new ArgumentNullException(nameof(_guitarsProvider));
        var guitarResult = await _guitarsProvider.GetGuitarAsync(prodId)!;
        guitarResult = guitarResult ?? throw new ArgumentNullException(nameof(guitarResult));
        Guitar product = guitarResult.Data.ToGuitar();
        product = product ?? throw new ArgumentNullException(nameof(product));
        if (_cart == null) throw new ArgumentNullException(nameof(_cart));
        CartItem item = new CartItem {Quantity = 1, Product = product};
        if (_cart.Products == null || !_cart.Products.Contains(item))
        {
            if (product.IsDeleted == 0)
            {
                _cart.Products = new List<CartItem>();
                await _cartItemsCreator.AddCartItemAsync(item)!;
            }
        }
    }
}