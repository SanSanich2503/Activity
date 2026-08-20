using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApp.Controllers;

[Authorize(Roles = "Админ, Покупатель")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}