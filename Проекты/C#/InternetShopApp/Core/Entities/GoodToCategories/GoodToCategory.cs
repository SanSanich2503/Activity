using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Categories;
using Core.Entities.Goods;

namespace Core.Entities.GoodToCategories;

public class GoodToCategory : Entity
{
    [DataType("ForeignKey")]
    [ForeignKey("Good")]
    public int GoodId { get; set; }

    [DataType("Reference")]
    public virtual Good Good { get; set; }
    
    [DataType("ForeignKey")]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    [DataType("Reference")]
    public virtual Category Category { get; set; }
}