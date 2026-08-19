namespace Data.ViewModels.Goods;

public class GoodForm : Form
{
    public GoodForm() {}

    public GoodForm(int id, string? title, string? description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}