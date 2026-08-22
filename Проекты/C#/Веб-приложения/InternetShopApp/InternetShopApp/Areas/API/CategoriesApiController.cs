using Data.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Areas.API;

[ApiController]
[Route("api/Categories/[action]")]
[Authorize(Roles = "Админ")]
public class CategoriesApiController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesApiController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    /// <summary>
    /// Получение категорий
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="title">Название</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetCategories(int? pageNumber = null, int? pageSize = null, string? title = null)
        => new OkObjectResult(_categoryService.BuildViewModelList(pageNumber ?? 1, pageSize ?? 10, title ?? "").Result);

    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="form">Форма для создания категории</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(CategoryForm form)
    {
        var result = _categoryService.Create(form).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Получение категории по Id
    /// </summary>
    /// <param name="id">Id категории</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetById(int id) => new OkObjectResult(_categoryService.BuildFormById(id));
    
    /// <summary>
    /// Редактирование категории
    /// </summary>
    /// <param name="form">Форма для редактирования категории</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Update(CategoryForm form)
    {
        var result = _categoryService.Update(form).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }

    /// <summary>
    /// Удаление категории
    /// </summary>
    /// <param name="id">Id категории</param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var result = _categoryService.Delete(id).Result;
        
        return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
    }
}