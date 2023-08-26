using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Extensions;
using AlexGuitarsShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class CatalogController : Controller
{
    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly IGuitarValidator _guitarValidator;

    public CatalogController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarValidator guitarValidator, IGuitarsUpdater guitarsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
        _guitarValidator = guitarValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        if (!await _guitarValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidPage;
           return View("Notification");
        }
        
        int offset = Paginator.GetOffset(pageNumber);
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        List<Guitar> list = (await _guitarsProvider.GetGuitarsByLimitAsync(offset, Paginator.Limit)).Data;
        PaginatedListViewModel<Guitar> model = list.ToPaginatedList(Title.Catalog, count, pageNumber);
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public IActionResult Add()
    {
        return View(new GuitarViewModel());
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Update(int id)
    {
        if (!await _guitarValidator.CheckIfGuitarExist(id))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidGuitarId;
           return View("Notification");
        }
        
        var result = await _guitarsProvider.GetGuitarAsync(id);
        GuitarViewModel model = result.Data.ToGuitarViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Add(GuitarViewModel model)
    {
        if (!_guitarValidator.CheckIfGuitarIsValid(model.ToGuitar()))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidGuitar;
           return View("Notification");
        }
        
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
        if (!await _guitarValidator.CheckIfGuitarExist(model.Id) 
            || !_guitarValidator.CheckIfGuitarIsValid(model.ToGuitar()))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidGuitar;
           return View("Notification");
        }
        
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsUpdater.UpdateGuitarAsync(model.ToGuitar());
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _guitarValidator.CheckIfGuitarExist(id))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidGuitarId;
           return View("Notification");
        }
        
        await _guitarsUpdater.DeleteGuitarAsync(id);
        return RedirectToAction("Index");
    }
}