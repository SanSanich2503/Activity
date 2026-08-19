using Data.Enums.PurchaseStatuses;

namespace Core.Entities.PurchaseStatuses;

public class PurchaseStatus : Entity
{
    public PurchaseStatusEnum PurchaseStatusEnum { get; set; }
}