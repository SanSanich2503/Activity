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
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        => View(_purchaseService.GetCart(pageNumber, pageSize).Result);
    
    public async Task<IActionResult> Delete(int id)
    {
        await _purchaseService.Delete(id);

        return Content("OK");
    }
}