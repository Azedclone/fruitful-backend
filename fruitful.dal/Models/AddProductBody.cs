using System.ComponentModel.DataAnnotations;

namespace fruitful.dal.Models;

public class AddProductBody
{
    [Required] [StringLength(100)] public string Name { get; set; }


    [Range(0.01, double.MaxValue)] public decimal CostPrice { get; set; } // The price at which the product is bought


    [Range(0.01, double.MaxValue)] public decimal SellPrice { get; set; } // The price at which the product is sold


    [Range(0, int.MaxValue)] public int QuantityInStock { get; set; }


    public string CategoryId { get; set; }

    [Url] public string? Image { get; set; }

    public static DateTime CreatedAt { get; } = DateTime.Now;

    public DateTime ExpiredAt { get; set; }
}