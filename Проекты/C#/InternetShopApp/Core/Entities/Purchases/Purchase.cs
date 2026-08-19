using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Goods;
using Core.Entities.PurchaseStatuses;
using Core.Entities.Users;

namespace Core.Entities.Purchases;

public class Purchase : Entity
{
    [DataType("ForeignKey")]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [DataType("Reference")]
    public virtual User User { get; set; }
    
    [DataType("ForeignKey")]
    [ForeignKey("Good")]
    public int GoodId { get; set; }

    [DataType("Reference")]
    public virtual Good Good { get; set; }
    
    [DataType("ForeignKey")]
    [ForeignKey("PurchaseStatus")]
    public int PurchaseStatusId { get; set; }

    [DataType("Reference")]
    public virtual PurchaseStatus PurchaseStatus { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    
    public DateTime DeliveryDate { get; set; }
}