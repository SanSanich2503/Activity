using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Goods;

public class GoodForm : Form
{
    [Display(Name = "Цена за шт.")]
    public int Price { get; set; }
    
    [Display(Name = "Количество")]
    public int Count { get; set; }
    
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