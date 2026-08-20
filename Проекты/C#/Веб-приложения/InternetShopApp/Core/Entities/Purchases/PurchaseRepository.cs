using Core.Entities.PurchaseStatuses;
using Data.Enums.PurchaseStatuses;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Purchases;

public class PurchaseRepository : BaseRepository<Purchase>
{
    public PurchaseRepository(DataContext context) : base(context) { }
    
    public new List<Purchase> GetAll()
        => _context.Purchases
            .Include(x => x.Good)
            .Include(x => x.PurchaseStatus)
            .ToList();
    
    public List<Purchase> GetPurchasesByGoodId(int goodId)
        => _context.Purchases
            .Where(p => p.GoodId == goodId)
            .ToList();
    
    public List<Purchase> GetPurchasesByUserId(int userId)
        => _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(x => x.Good)
            .Include(x => x.PurchaseStatus)
            .ToList();
    
    public List<Purchase> GetCart()
        => GetAll()
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum == PurchaseStatusEnum.Cart)
            .ToList();

    public List<Purchase> GetCartByUserId(int userId)
        => GetPurchasesByUserId(userId)
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum == PurchaseStatusEnum.Cart)
            .ToList();
    
    public List<Purchase> GetOrders()
        => GetAll()
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum != PurchaseStatusEnum.Cart)
            .OrderByDescending(x => x.PurchaseDate)
            .ToList();
    
    public List<Purchase> GetOrdersByUserId(int userId)
        => GetPurchasesByUserId(userId)
            .Where(x => x.PurchaseStatus.PurchaseStatusEnum != PurchaseStatusEnum.Cart)
            .OrderBy(x => x.PurchaseStatus.PurchaseStatusEnum)
            .ToList();

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