namespace Core.Entities.Goods;

public class Good : Entity
{
    public int Price { get; set; }
    
    public int Count { get; set; }
    
    public int DeliveryDays { get; set; }
}