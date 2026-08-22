using Data.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

[Authorize(Roles = "Админ")]
public class CategoriesController : Controller
{
    private readonly CategoryService _categoryService;
    
    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
        => View(_categoryService.BuildViewModelList(pageNumber, pageSize, name).Result);

    public IActionResult Create()
    {
        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _categoryService.BuildForm());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryForm form)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.Create(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _categoryService.BuildByForm(form));
    }

    public IActionResult Update(int id)
    {
        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _categoryService.BuildFormById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryForm form)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.Update(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _categoryService.BuildByForm(form));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);

        return Content("OK");
    }
}