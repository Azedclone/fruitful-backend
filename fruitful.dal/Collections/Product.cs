namespace fruitful.dal.Collections;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [BsonElement("cost_price")]
    [Range(0.01, double.MaxValue)]
    public decimal CostPrice { get; set; } // The price at which the product is bought

    [BsonElement("sell_price")]
    [Range(0.01, double.MaxValue)]
    public decimal SellPrice { get; set; } // The price at which the product is sold

    [BsonElement("quantity_in_stock")]
    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }

    [BsonElement("category_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }

    [BsonElement("image")] [Url] public string? Image { get; set; }

    [BsonElement("created_at")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [BsonElement("expired_at")] public DateTime ExpiredAt { get; set; }
}