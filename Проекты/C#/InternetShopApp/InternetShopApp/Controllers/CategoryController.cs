using Data.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

public class CategoryController : Controller
{
    private readonly CategoryService _categoryService;
    
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
        => View(_categoryService.BuildViewModelList(pageNumber, pageSize, name));

    public IActionResult Create()
    {
        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _categoryService.BuildForm());
    }

    [HttpPost]
    public IActionResult Create(CategoryForm form)
    {
        if (ModelState.IsValid)
        {
            _categoryService.Create(form);

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
    public IActionResult Update(CategoryForm form)
    {
        if (ModelState.IsValid)
        {
            _categoryService.Update(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _categoryService.BuildByForm(form));
    }

    public IActionResult Delete(int id)
    {
        _categoryService.Delete(id);

        return Content("OK");
    }
}