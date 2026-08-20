using Data.Enums.PurchaseStatuses;

namespace Core.Entities.PurchaseStatuses;

public class PurchaseStatusRepository : BaseRepository<PurchaseStatus>
{
    public PurchaseStatusRepository(DataContext context) : base(context) {}

    public PurchaseStatus? GetByEnumId(PurchaseStatusEnum enumId)
        => _context.PurchaseStatuses.FirstOrDefault(x => x.PurchaseStatusEnum == enumId);
}