using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Goods;

public class GoodForm : Form
{
    /// <summary>
    /// Цена за шт.
    /// </summary>
    [Display(Name = "Цена за шт.")]
    public int Price { get; set; }
    
    /// <summary>
    /// Количество
    /// </summary>
    [Display(Name = "Количество")]
    public int Count { get; set; }
    
    /// <summary>
    /// Сколько дней доставляется
    /// </summary>
    [Display(Name = "Сколько дней доставляется")]
    public int DeliveryDays { get; set; }
    
    public GoodForm() {}

    public GoodForm(int id, string? title, string? description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}