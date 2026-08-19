using System.ComponentModel.DataAnnotations;
using Scrutor.AspNetCore;

namespace Data.ViewModels;

public abstract class Form
{
    public int Id { get; set; }
    
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string? Title { get; set; }
    
    [Display(Name = "Описание")]
    public string? Description { get; set; }
}