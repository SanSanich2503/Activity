using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

public class OrderController : Controller
{
    private readonly PurchaseService _purchaseService;
    
    public OrderController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    
    public IActionResult Index() => View();

    public IActionResult Cancel(int id)
    {
        _purchaseService.Cancel(id);
        
        return Content("OK");
    }

    public IActionResult Complete(int id)
    {
        _purchaseService.Complete(id);
        
        return Content("OK");
    }
    
    public IActionResult Return(int id)
    {
        _purchaseService.Return(id);
        
        return Content("OK");
    }
}