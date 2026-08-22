using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Areas.API;

[ApiController]
[Route("api/Carts/[action]")]
[Authorize(Roles = "Админ, Покупатель")]
public class CartsApiController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public CartsApiController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    
    /// <summary>
    /// Получение корзины
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetCart(int? pageNumber = null, int? pageSize = null)
        => new OkObjectResult(_purchaseService.GetCart(pageNumber ?? 1, pageSize ?? 10).Result);

    /// <summary>
    /// Удален товара из корзины
    /// </summary>
    /// <param name="id">Id покупки</param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var result = _purchaseService.Delete(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }
}