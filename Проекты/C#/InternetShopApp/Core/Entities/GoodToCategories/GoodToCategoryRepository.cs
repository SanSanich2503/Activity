namespace Core.Entities.GoodToCategories;

public class GoodToCategoryRepository : BaseRepository<GoodToCategory>
{
    public GoodToCategoryRepository(DataContext context) : base(context) {}
}