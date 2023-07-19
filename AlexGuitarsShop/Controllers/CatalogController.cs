using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Domain.ViewModels;
using AlexGuitarsShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CatalogController : Controller
{
    const int Limit = 10;

    private readonly IGuitarService _guitarService;
    private readonly ICartItemRepository _cartRepository;
    private readonly Cart _cart;

    private int _pageCount;
    private int _offset;

    public CatalogController(IGuitarService guitarService, ICartItemRepository cartRepository, Cart cart)
    {
        _guitarService = guitarService;
        _cartRepository = cartRepository;
        _cart = cart;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        _offset = (pageNumber - 1) * Limit;
        int count = (await _guitarService.GetCount()).Data;
        _pageCount = count % Limit == 0 ? count / Limit : count / Limit + 1;
        var response = await _guitarService.GetGuitarsByLimit(_offset, Limit);
        CatalogViewModel catalog = new CatalogViewModel
        {
            Guitars = response.Data, PageCount = _pageCount, CurrentPage = pageNumber
        };
        return View(catalog);
    }

    public async Task<IActionResult> AddOrUpdate(int id)
    {
        if (id == 0)
        {
            return View(new GuitarViewModel {Id = 0});
        }

        var response = await _guitarService.GetGuitar(id);
        return View(response.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate(GuitarViewModel model)
    {
        if (model.Avatar != null)
        {
            using var binaryReader = new BinaryReader(model.Avatar.OpenReadStream());
            model.Image = binaryReader.ReadBytes((int) model.Avatar.Length);
        }
        else
        {
            var response = await _guitarService.GetGuitar(model.Id);
            model.Image = response.Data.Image;
        }

        if (model.Id == 0)
        {
            await _guitarService.AddGuitar(model);
        }
        else
        {
            await _guitarService.UpdateGuitar(model);
        }

        return RedirectToAction("Index");
    }

    public async Task<RedirectToActionResult> AddToCart(int prodId)
    {
        var response = await _guitarService.GetGuitar(prodId);
        Guitar product = _guitarService.ToGuitar(response.Data);
        CartItem item = new CartItem {CartId = _cart.Id, Product = product, Quantity = 1};
        if (product.IsDeleted == 0 && !_cart.Products.Contains(item))
        {
            _cart.Products.Add(item);
            await _cartRepository.Add(item);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _guitarService.DeleteGuitar(id);
        return RedirectToAction("Index");
    }
}