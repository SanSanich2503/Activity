namespace Data.ViewModels.Categories;

public class CategoryForm : Form
{
    public CategoryForm() {}

    public CategoryForm(int id, string? title, string? description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}