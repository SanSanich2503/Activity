namespace Core.Entities.Goods;

public class GoodRepository : BaseRepository<Good>
{
    public GoodRepository(DataContext context) : base(context) {}

    public new List<Good> GetAll()
        => _context.Goods.Where(x => x.Count > 0).ToList();
}