using System.Security.Claims;
using System.Security.Cryptography;
using Core;
using Core.Entities.Users;
using Data.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Services.Services;

public class AuthService : BaseService
{
    private readonly HttpContext _httpContext;
    private readonly UserRepository _userRepository;

    public AuthService(DataContext context, HttpContextAccessor httpContextAccessor, UserRepository userRepository) : base(context)
    {
        _userRepository = userRepository;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<bool> Login(LoginForm model, ModelStateDictionary dictionary)
    {
        try
        {
            if (ValidateLoginData(model))
            {
                var email = model.Email?.ToLower();
                var password = HashPassword(model.Password);
                var user = _userRepository.GetAll().FirstOrDefault(x => x.Email?.ToLower() == email && x.Password == password);

                if (user != null)
                {
                    await Authenticate(user);
                    
                    return true;
                }
                
                dictionary.AddModelError("UserNotFound", "Пользовтель не найден");

                return false;
            }

            dictionary.AddModelError("EmptyEmailOrPassword", "Email или пароль не должны быть пустыми");

            return false;
        }
        catch (Exception e)
        {
            dictionary.AddModelError("InnerError", "Произошла внутренняя ошибка сервера");

            return false;
        }
    }

    public async Task<bool> Register(RegisterForm model, ModelStateDictionary dictionary)
    {
        try
        {
            if (ValidateLoginData(model))
            {
                var email = model.Email?.ToLower();
                var existedUser = _userRepository.GetAll().FirstOrDefault(x => x.Email?.ToLower() == email);

                if (existedUser == null)
                {
                    var name = model.Name?.Trim() ?? "";
                    var surname = model.Surname?.Trim() ?? "";
                    var patronymic = model.Patronymic?.Trim() ?? "";
                    
                    var user = new User
                    {
                        UserGuid =  Guid.NewGuid().ToString(),
                        Email = model.Email,
                        Password = HashPassword(model.Password),
                        Surname = surname,
                        Name = name,
                        Patronymic =  patronymic,
                        Title = $"{surname} {name} {patronymic}".Trim()
                    };
                    
                    await Authenticate(user);
                    
                    return true;
                }
                
                dictionary.AddModelError("UserExists", "Такой пользователь уже существует");

                return false;
            }

            dictionary.AddModelError("EmptyEmailOrPassword", "Email или пароль не должны быть пустыми");

            return false;
        }
        catch (Exception e)
        {
            dictionary.AddModelError("InnerError", "Произошла внутренняя ошибка сервера");

            return false;
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return new RedirectToActionResult("Login", "Auth", null);
    }

    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name ?? user.Email ?? "Безымянный"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Title ?? "Без роли")
        };
        
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    private string HashPassword(string? password)
        => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: RandomNumberGenerator.GetBytes(128 / 8),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

    private bool ValidateLoginData(LoginForm model)
        => !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password);
}