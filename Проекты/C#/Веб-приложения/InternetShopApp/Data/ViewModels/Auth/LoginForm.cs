using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Auth;

public class LoginForm
{
    /// <summary>
    /// Email
    /// </summary>
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Не указан Email")]
    public string? Email { get; set; }
         
    /// <summary>
    /// Пароль
    /// </summary>
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}