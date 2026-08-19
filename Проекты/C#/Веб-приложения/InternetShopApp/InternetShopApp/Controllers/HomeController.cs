using Microsoft.AspNetCore.Mvc;

namespace InternetShopApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}