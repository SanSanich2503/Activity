using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Goods;

public class GoodRepository : BaseRepository<Good>
{
    public GoodRepository(DataContext context) : base(context) {}

    public new IQueryable<Good> GetAll()
        => _context.Goods.Where(x => x.Count > 0).AsNoTracking();
}