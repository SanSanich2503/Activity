using Data.ViewModels.Goods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Areas.API;

[ApiController]
[Route("api/Goods/[action]")]
[Authorize(Roles = "Админ, Покупатель")]
public class GoodsApiController : ControllerBase
{
    private readonly GoodService _goodService;

    public GoodsApiController(GoodService goodService)
    {
        _goodService = goodService;
    }
    
    /// <summary>
    /// Получение товаров
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="title">Название</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetGoods(int? pageNumber = null, int? pageSize = null, string? title = null)
        => new OkObjectResult(_goodService.BuildViewModelList(pageNumber ?? 1, pageSize ?? 10, title ?? "").Result);

    /// <summary>
    /// Создание товара
    /// </summary>
    /// <param name="form">Форма для создания товара</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(GoodForm form)
    {
        var result = _goodService.Create(form).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Получение товара по Id
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetById(int id) => new OkObjectResult(_goodService.BuildFormById(id));
    
    /// <summary>
    /// Редактирование товара
    /// </summary>
    /// <param name="form">Форма для редактирования товара</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Update(GoodForm form)
    {
        var result = _goodService.Update(form).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var result = _goodService.Delete(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }
    
    /// <summary>
    /// Добавление товара в корзину
    /// </summary>
    /// <param name="id">Id товара</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var result = _goodService.AddToCart(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }
}