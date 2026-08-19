namespace Core.Entities.Purchases;

public class PurchaseRepository : BaseRepository<Purchase>
{
    public PurchaseRepository(DataContext context) : base(context) { }
}