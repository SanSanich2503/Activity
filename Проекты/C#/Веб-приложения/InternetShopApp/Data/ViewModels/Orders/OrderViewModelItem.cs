using Data.Enums.PurchaseStatuses;

namespace Data.ViewModels.Orders;

public class OrderViewModelItem : ViewModelItem
{
    public int Price { get; set; }
    
    public string? PurchaseDate { get; set; }
    
    public string? DeliveryDate { get; set; }
    
    public string? Status { get; set; }
    
    public PurchaseStatusEnum StatusEnum { get; set; }
}