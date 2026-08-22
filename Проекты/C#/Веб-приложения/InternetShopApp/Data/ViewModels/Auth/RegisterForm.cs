using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Auth;

public class RegisterForm : LoginForm
{
    /// <summary>
    /// Пароль для подтверджения
    /// </summary>
    [Display(Name = "Пароль для подтверджения")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? ConfirmPassword { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    [Display(Name = "Номер телефона")]
    public string? Phone { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    [Display(Name = "Фамилия")]
    public string? Surname { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    [Display(Name = "Имя")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }
}