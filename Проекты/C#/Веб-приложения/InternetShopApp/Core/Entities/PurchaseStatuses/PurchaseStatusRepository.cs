namespace Core.Entities.PurchaseStatuses;

public class PurchaseStatusRepository : BaseRepository<PurchaseStatus>
{
    public PurchaseStatusRepository(DataContext context) : base(context) {}
}