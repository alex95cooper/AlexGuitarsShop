using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult AboutUs() => View();

    public IActionResult Contacts() => View();
}