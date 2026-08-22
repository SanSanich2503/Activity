using Data.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Areas.API;

[ApiController]
[Route("api/Auth/[action]")]
public class AuthApiController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthApiController(AuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>
    /// Вход в систему
    /// </summary>
    /// <param name="form">Форма для входа в систему</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(LoginForm form)
    {
        var result = _authService.Login(form).Result;
        
        return result.Item1 ? new OkObjectResult(result.Item2) : new BadRequestObjectResult(result.Item2);
    }
    
    /// <summary>
    /// Регистрация в системе
    /// </summary>
    /// <param name="form">Форма для регистрации</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Register(RegisterForm form)
    {
        var result = _authService.Register(form).Result;
        
        return result.Item1 ? new OkObjectResult(result.Item2) : new BadRequestObjectResult(result.Item2);
    }
    
    /// <summary>
    /// Выход из системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Logout()
    {
        var result = _authService.Logout().Result;
        
        return result.Item1 ? new OkObjectResult(result.Item2) : new BadRequestObjectResult(result.Item2);
    }
}