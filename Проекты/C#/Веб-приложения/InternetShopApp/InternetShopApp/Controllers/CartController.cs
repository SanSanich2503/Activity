using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

public class CartController : Controller
{
    private readonly PurchaseService _purchaseService;
    
    public CartController(PurchaseService purchaseService)
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