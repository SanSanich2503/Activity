using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Areas.API;

[ApiController]
[Route("api/Orders/[action]")]
[Authorize(Roles = "Админ, Покупатель")]
public class OrdersApiController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public OrdersApiController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    
    /// <summary>
    /// Получение заказов
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetOrders(int? pageNumber = null, int? pageSize = null)
        => new OkObjectResult(_purchaseService.GetOrders(pageNumber ?? 1, pageSize ?? 10).Result);

    /// <summary>
    /// Отмена заказа
    /// </summary>
    /// <param name="id">Id заказа</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Cancel(int id)
    {
        var result = _purchaseService.Cancel(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Завершение заказа
    /// </summary>
    /// <param name="id">Id заказа</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var result = _purchaseService.Complete(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Возврат заказа
    /// </summary>
    /// <param name="id">Id заказа</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Return(int id)
    {
        var result = _purchaseService.Return(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }
}