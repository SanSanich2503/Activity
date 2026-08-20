using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

[Authorize(Roles = "Админ, Покупатель")]
public class CartsController : Controller
{
    private readonly PurchaseService _purchaseService;
    
    public CartsController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    
    public IActionResult Index() => View();
    
    public IActionResult Delete(int id)
    {
        _purchaseService.Delete(id);

        return Content("OK");
    }
}