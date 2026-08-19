namespace Core.Entities.Categories;

public class CategoryRepository : BaseRepository<Category>
{
    public CategoryRepository(DataContext context) : base(context) {}
}