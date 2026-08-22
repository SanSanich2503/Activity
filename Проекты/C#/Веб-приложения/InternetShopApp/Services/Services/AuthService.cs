using System.Security.Claims;
using Core;
using Core.Entities.Roles;
using Core.Entities.Users;
using Data.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Services.Services;

public class AuthService : BaseService
{
    private readonly HttpContext _httpContext;
    private readonly UserRepository _userRepository;
    private readonly RoleRepository _roleRepository;

    public AuthService(DataContext context, IHttpContextAccessor httpContextAccessor,
        UserRepository userRepository, RoleRepository roleRepository) : base(context)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _httpContext = httpContextAccessor.HttpContext ??  throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<(bool, string)> Login(LoginForm model, ModelStateDictionary? dictionary = null)
    {
        var message = "";
        
        try
        {
            if (ValidateLoginData(model))
            {
                var email = model.Email?.ToLower() ?? "";
                var password = DbInitializer.HashPassword(model.Password ?? "");
                var user = _userRepository.GetByEmailAndPassword(email, password);

                if (user != null)
                {
                    await Authenticate(user);
                    
                    return (true, "Вход в систему выполнен успешно");
                }

                message = "Пользовтель не найден";
                dictionary?.AddModelError("UserNotFound", message);

                return (false, message);
            }

            message = "Email или пароль не должны быть пустыми";
            dictionary?.AddModelError("EmptyEmailOrPassword", message);

            return (false, message);
        }
        catch (Exception e)
        {
            message = "Произошла внутренняя ошибка сервера";
            dictionary?.AddModelError("InnerError", message);

            return (false, message);
        }
    }

    public async Task<(bool, string)> Register(RegisterForm model, ModelStateDictionary? dictionary = null)
    {
        var message = "";
        
        try
        {
            if (ValidateLoginData(model))
            {
                var email = model.Email?.ToLower() ?? "";
                var existedUser = _userRepository.GetByEmail(email);

                if (existedUser == null)
                {
                    var name = model.Name?.Trim() ?? "";
                    var surname = model.Surname?.Trim() ?? "";
                    var patronymic = model.Patronymic?.Trim() ?? "";
                    var role = _roleRepository.GetAll().FirstOrDefault(x => x.Title == "Покупатель");

                    if (role != null)
                    {
                        var user = new User
                        {
                            UserGuid = Guid.NewGuid().ToString(),
                            Email = email,
                            Password = DbInitializer.HashPassword(model.Password ?? ""),
                            Surname = surname,
                            Name = name,
                            Patronymic = patronymic,
                            Role = role,
                            Title = $"{surname} {name} {patronymic}".Trim()
                        };
                        await _userRepository.Add(user);

                        await Authenticate(user);

                        return (true, "Пользователь успешно зарегистрирован в системе");
                    }

                    message = "Роль покупателя не сущестсвует";
                    dictionary?.AddModelError("RoleNotExists", "Роль покупателя не сущестсвует");

                    return (false, message);
                }

                message = "Такой пользователь уже существует";
                dictionary?.AddModelError("UserExists", "Такой пользователь уже существует");

                return (false, message);
            }

            message = "Email или пароль не должны быть пустыми";
            dictionary?.AddModelError("EmptyEmailOrPassword", message);

            return (false, message);
        }
        catch (Exception e)
        {
            message = "Произошла внутренняя ошибка сервера";
            dictionary?.AddModelError("InnerError", message);

            return (false, message);
        }
    }

    public async Task<(bool, string)> Logout()
    {
        try
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return (true, "Выход из системы успешно выполнен");
        }
        catch (Exception e)
        {
            return (false, "Произошла внутренняя ошибка сервера");
        }
    }

    public bool IsAdmin()
    {
        var userGuid = _httpContext.User.Identity?.Name ?? "";
        var user = _userRepository.GetCurrentUser(userGuid);
        
        return user?.Role.Title?.ToLower() == "админ";
    }

    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserGuid ?? ""),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Title ?? "Без роли")
        };
        
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    private bool ValidateLoginData(LoginForm model)
        => !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password);
}