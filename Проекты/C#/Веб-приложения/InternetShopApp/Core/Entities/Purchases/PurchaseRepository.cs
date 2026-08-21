using Core.Entities.PurchaseStatuses;
using Data.Enums.PurchaseStatuses;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Purchases;

public class PurchaseRepository : BaseRepository<Purchase>
{
    public PurchaseRepository(DataContext context) : base(context) { }
    
    public new IQueryable<Purchase> GetAll()
        => _context.Purchases
            .Include(x => x.Good)
            .Include(x => x.PurchaseStatus)
            .AsNoTracking();
    
    public IQueryable<Purchase> GetPurchasesByGoodId(int goodId)
        => _context.Purchases
            .Where(p => p.GoodId == goodId)
            .AsNoTracking();
    
    public IQueryable<Purchase> GetPurchasesByUserId(int userId)
        => _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(x => x.Good)
            .Include(x => x.PurchaseStatus)
            .AsNoTracking();
    
    public IQueryable<Purchase> GetCart()
        => GetAll()
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum == PurchaseStatusEnum.Cart)
            .AsNoTracking();

    public IQueryable<Purchase> GetCartByUserId(int userId)
        => GetPurchasesByUserId(userId)
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum == PurchaseStatusEnum.Cart)
            .AsNoTracking();
    
    public IQueryable<Purchase> GetOrders()
        => GetAll()
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum != PurchaseStatusEnum.Cart)
            .OrderByDescending(x => x.PurchaseDate)
            .AsNoTracking();
    
    public IQueryable<Purchase> GetOrdersByUserId(int userId)
        => GetPurchasesByUserId(userId)
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum != PurchaseStatusEnum.Cart)
            .OrderBy(x => x.PurchaseStatus.PurchaseStatusEnum)
            .AsNoTracking();

    public void ChangeStatus(int id, PurchaseStatus purchaseStatus)
    {
        var purchase = GetById(id);
        if (purchase != null)
        {
            purchase.PurchaseStatus = purchaseStatus;
            _context.SaveChanges();
        }
    }
}