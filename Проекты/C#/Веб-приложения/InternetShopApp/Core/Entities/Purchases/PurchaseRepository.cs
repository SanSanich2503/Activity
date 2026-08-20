using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Purchases;

public class PurchaseRepository : BaseRepository<Purchase>
{
    public PurchaseRepository(DataContext context) : base(context) { }
    
    public List<Purchase> GetPurchasesByGoodId(int goodId)
        => _context.Purchases
            .Where(p => p.GoodId == goodId)
            .ToList();
    
    public List<Purchase> GetPurchasesByUserId(int userId)
        => _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(x => x.PurchaseStatus)
            .ToList();
}