using Data.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginForm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginForm model) 
        => _authService.Login(model, ModelState).Result.Item1 ? RedirectToAction("Index", "Home") : View(model);
    
    [HttpGet]
    public IActionResult Register() => View(new RegisterForm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterForm model) 
        => _authService.Register(model, ModelState).Result.Item1? RedirectToAction("Index", "Home") : View(model);
    
    [HttpGet]
    public IActionResult Logout() => _authService.Logout().Result.Item1
        ? RedirectToAction("Login", "Auth")
        : Content("Произошла внутрення ошибка сервера");

    [HttpGet]
    public IActionResult AccessDenied() => Content("Доступ запрещен");
}