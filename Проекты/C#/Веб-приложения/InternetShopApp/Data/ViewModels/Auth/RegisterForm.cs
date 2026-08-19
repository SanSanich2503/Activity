using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Auth;

public class RegisterForm : LoginForm
{
    [Display(Name = "Потвердить пароль")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? ConfirmPassword { get; set; }
    
    [Display(Name = "Номер телефона")]
    public string? Phone { get; set; }
    
    [Display(Name = "Фамилия")]
    public string? Surname { get; set; }
    
    [Display(Name = "Имя")]
    public string? Name { get; set; }
    
    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }
}