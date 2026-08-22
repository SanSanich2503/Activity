using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

[Authorize(Roles = "Админ, Покупатель")]
public class OrdersController : Controller
{
    private readonly PurchaseService _purchaseService;
    
    public OrdersController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        => View(_purchaseService.GetOrders(pageNumber, pageSize).Result);

    public async Task<IActionResult> Cancel(int id)
    {
        await _purchaseService.Cancel(id);
        
        return Content("OK");
    }

    public async Task<IActionResult> Complete(int id)
    {
        await _purchaseService.Complete(id);
        
        return Content("OK");
    }
    
    public async Task<IActionResult> Return(int id)
    {
        await _purchaseService.Return(id);
        
        return Content("OK");
    }
}