using System.ComponentModel.DataAnnotations;
using Scrutor.AspNetCore;

namespace Data.ViewModels;

public abstract class Form
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Обязательное поле")]
    public string? Title { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
}