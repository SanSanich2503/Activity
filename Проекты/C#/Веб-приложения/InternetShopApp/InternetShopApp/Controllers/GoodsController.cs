using Data.ViewModels.Goods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

[Authorize(Roles = "Админ, Покупатель")]
public class GoodsController : Controller
{
    private readonly GoodService _goodService;
    
    public GoodsController(GoodService goodService)
    {
        _goodService = goodService;
    }
    
    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
        => View(_goodService.BuildViewModelList(pageNumber, pageSize, name).Result);

    public IActionResult Create()
    {
        ViewBag.ActionName = "Создание";
        ViewBag.MethodName = "Create";

        return View("CreateUpdate", _goodService.BuildForm());
    }

    [HttpPost]
    public async Task<IActionResult> Create(GoodForm form)
    {
        if (ModelState.IsValid)
        {
            await _goodService.Create(form);

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
    public async Task<IActionResult> Update(GoodForm form)
    {
        if (ModelState.IsValid)
        {
            await _goodService.Update(form);

            return RedirectToAction("Index");
        }

        ViewBag.ActionName = "Редактирование";
        ViewBag.MethodName = "Update";

        return View("CreateUpdate", _goodService.BuildByForm(form));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _goodService.Delete(id);

        return Content("OK");
    }

    public async Task<IActionResult> AddToCart(int id)
    {
        await _goodService.AddToCart(id);
        
        return Content("OK");
    }
}