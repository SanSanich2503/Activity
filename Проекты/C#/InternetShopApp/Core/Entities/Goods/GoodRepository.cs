namespace Core.Entities.Goods;

public class GoodRepository : BaseRepository<Good>
{
    public GoodRepository(DataContext context) : base(context) {}
}