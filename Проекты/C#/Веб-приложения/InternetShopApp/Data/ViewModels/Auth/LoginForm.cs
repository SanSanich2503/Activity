using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Auth;

public class LoginForm
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Не указан Email")]
    public string? Email { get; set; }
         
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}