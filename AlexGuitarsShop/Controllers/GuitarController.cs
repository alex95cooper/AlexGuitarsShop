using AlexGuitarsShop.DAL.Models;
using AlexGuitarsShop.Domain;
using AlexGuitarsShop.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Domain.Validators;
using AlexGuitarsShop.Extensions;
using AlexGuitarsShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Controllers;

public class GuitarController : Controller
{
    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;
    private readonly IGuitarValidator _guitarValidator;

    public GuitarController(IGuitarsCreator guitarsCreator,
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
        int count = (await _guitarsProvider.GetCountAsync()).Data;
        if (!PageValidator.CheckIfPageIsValid(pageNumber, Paginator.Limit, count))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidPage;
           return View("Notification");
        }
        
        int offset = Paginator.GetOffset(pageNumber);
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
        if (model == null || !_guitarValidator.CheckIfGuitarIsValid(model.ToGuitar()))
        {
           ViewBag.Message = Constants.ErrorMessages.InvalidGuitar;
           return View("Notification");
        }
        
        model.Image = model.Avatar == null ? model.Image : model.Avatar.ToByteArray();
        await _guitarsCreator.AddGuitarAsync(model.ToGuitar());
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Update(GuitarViewModel model)
    {
        if (model == null || !await _guitarValidator.CheckIfGuitarUpdateIsValid(model.ToGuitar()))
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