using Data.ViewModels.Goods;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

public class GoodController : Controller
{
    private readonly GoodService _goodService;
    
    public GoodController(GoodService goodService)
    {
        _goodService = goodService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
        => View(_goodService.BuildViewModelList(pageNumber, pageSize, name));

    public IActionResult Create()
    {
        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _goodService.BuildForm());
    }

    [HttpPost]
    public IActionResult Create(GoodForm form)
    {
        if (ModelState.IsValid)
        {
            _goodService.Create(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _goodService.BuildByForm(form));
    }

    public IActionResult Update(int id)
    {
        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _goodService.BuildFormById(id));
    }

    [HttpPost]
    public IActionResult Update(GoodForm form)
    {
        if (ModelState.IsValid)
        {
            _goodService.Update(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _goodService.BuildByForm(form));
    }

    public IActionResult Delete(int id)
    {
        _goodService.Delete(id);

        return Content("OK");
    }
}