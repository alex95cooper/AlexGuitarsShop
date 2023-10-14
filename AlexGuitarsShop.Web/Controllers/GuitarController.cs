using AlexGuitarsShop.Web.Domain.Interfaces.Guitar;
using AlexGuitarsShop.Web.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Web.Controllers;

public class GuitarController : Controller
{
    private readonly IGuitarsCreator _guitarsCreator;
    private readonly IGuitarsProvider _guitarsProvider;
    private readonly IGuitarsUpdater _guitarsUpdater;

    public GuitarController(IGuitarsCreator guitarsCreator,
        IGuitarsProvider guitarsProvider, IGuitarsUpdater guitarsUpdater)
    {
        _guitarsCreator = guitarsCreator;
        _guitarsProvider = guitarsProvider;
        _guitarsUpdater = guitarsUpdater;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var result = await _guitarsProvider.GetGuitarsByLimitAsync(pageNumber);

        if (result.IsSuccess)
        {
            if (result.Data.List.Count > 0)
            {
                return View(result.Data);
            }

            ViewBag.Message = Constants.ErrorMessages.CatalogEmpty;
        }
        else
        {
            ViewBag.Message = result.Error;
        }

        return View("Notification");
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
        var result = await _guitarsProvider.GetGuitarAsync(id);
        if (result.IsSuccess)
        {
            return View(result.Data);
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Add(GuitarViewModel model)
    {
        var result = await _guitarsCreator.AddGuitarAsync(model);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Update(GuitarViewModel model)
    {
        var result = await _guitarsUpdater.UpdateGuitarAsync(model);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }

    [HttpGet]
    [Authorize(Roles = Constants.Roles.AdminPlus)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _guitarsUpdater.DeleteGuitarAsync(id);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Message = result.Error;
        return View("Notification");
    }
}