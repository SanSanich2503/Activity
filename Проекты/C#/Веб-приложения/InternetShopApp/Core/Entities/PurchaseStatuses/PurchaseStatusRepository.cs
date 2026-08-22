using Data.Enums.PurchaseStatuses;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.PurchaseStatuses;

public class PurchaseStatusRepository : BaseRepository<PurchaseStatus>
{
    public PurchaseStatusRepository(DataContext context) : base(context) {}

    public  async Task<PurchaseStatus?> GetByEnumId(PurchaseStatusEnum enumId)
        => await _context.PurchaseStatuses.FirstOrDefaultAsync(x => x.PurchaseStatusEnum == enumId);
}